using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Network;
using FTServer.Core.MemoryManagement;
using FTServer.Network.Message;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FTServer.Network.Services
{
    public class NetworkMessageFactory : INetworkMessageFactory
    {
        private ReadOnlyDictionary<ushort, NetworkMessageGenerator> _networkMessageGenerators;

        public NetworkMessageFactory()
        {
            _networkMessageGenerators = new ReadOnlyDictionary<ushort, NetworkMessageGenerator>(new Dictionary<ushort, NetworkMessageGenerator>());
            var currentDomain = AppDomain.CurrentDomain;
            foreach (var assembly in currentDomain.GetAssemblies())
                generateNetworkMessageGenerators(assembly);
            currentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        private void generateNetworkMessageGenerators(Assembly assembly)
        {
            var inetMsgType = typeof(INetworkMessage);
            foreach (var type in assembly.GetTypes())
            {
                if (type != inetMsgType && inetMsgType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                {
                    var attr = type.GetCustomAttribute<NetworkMessageAttribute>();
                    if (attr != null)
                    {
                        lock (this)
                        {
                            if (_networkMessageGenerators.TryGetValue(attr.MessageId, out NetworkMessageGenerator generator))
                            {
                                throw new Exception($"NetworkMessage conflict for {attr.MessageId} with type {generator.Type.FullName}");
                            }

                            generator = new NetworkMessageGenerator(type);

                            var rwDict = _networkMessageGenerators.ToDictionary(p => p.Key, p => p.Value);
                            rwDict.Add(attr.MessageId, generator);
                            _networkMessageGenerators = new ReadOnlyDictionary<ushort, NetworkMessageGenerator>(rwDict);
                        }
                    }
                }
            }
        }

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            generateNetworkMessageGenerators(args.LoadedAssembly);
        }

        public unsafe INetworkMessage Create(byte[] buffer, int offset, int size)
        {
            fixed (byte* ptr = buffer)
            {
                var header = *(NetworkMessageHeader*)ptr;

                INetworkMessage message;
                if (_networkMessageGenerators.TryGetValue(header.MessageId, out NetworkMessageGenerator generator))
                {
                    message = generator.Generator();
                }
                else
                {
                    message = new UnknownNetworkMessage();
                }
                message.Deserialize(new UnmanagedMemory(new IntPtr(ptr + offset), size));
                return message;
            }
        }

        private class NetworkMessageGenerator
        {
            public Type Type { get; }
            public Func<INetworkMessage> Generator { get; }
            public NetworkMessageGenerator(Type type)
            {
                Type = type;

                // Get constructor
                ConstructorInfo ctor = null;
                try
                {
                    ctor = type.GetConstructor(new Type[0]);
                }
                catch
                {
                    throw new Exception($"{type.FullName} must have a parameterless constructor.");
                }

                // Make a NewExpression that calls the ctor with the args we just created
                NewExpression newExp = Expression.New(ctor);

                // Create a lambda with the New expression as body and our param object[] as arg
                LambdaExpression lambda = Expression.Lambda(typeof(Func<INetworkMessage>), newExp);

                // Compile it
                Generator = (Func<INetworkMessage>)lambda.Compile();
            }
        }
    }
}
