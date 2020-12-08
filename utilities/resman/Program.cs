using FTServer.Contracts.Resources;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace resman
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 2 || (args.Length == 2 && args[0] != "-a")) { Help(); }
            var type = args[0];
            var input = args[1];
            if (!File.Exists(input)) { Console.Error.Write($"File '{input}' not found."); Environment.Exit(1); }
            var host = CreateHostBuilder(args).Build();
            var services = host.Services;
            var resMan = services.GetService<IResourceManager>();
            var crypto = services.GetService<IResourceCryptographyService>();
            if (type == "-a")
            {
                var autoOutput = resMan.ConvertPath(input);
                if (resMan.IsResourceEncrypted(input))
                    Decrypt(input, autoOutput, crypto);
                else
                    Encrypt(input, autoOutput, crypto);
            }
            else if (type == "-d")
            {
                var output = args[2];
                Decrypt(input, output, crypto);
            }
            else if (type == "-e")
            {
                var output = args[2];
                Encrypt(input, output, crypto);
            }
            else
            {
                Help();
            }
            return 0;
        }

        private static void Decrypt(string input, string output, IResourceCryptographyService crypto)
        {
            using (var outputFs = new FileStream(output, FileMode.Create))
            using (var inputFs = new FileStream(input, FileMode.Open))
            using (var cryptoFs = crypto.Read(inputFs))
            {
                cryptoFs.CopyTo(outputFs);
            }
        }

        private static void Encrypt(string input, string output, IResourceCryptographyService crypto)
        {
            using (var outputFs = new FileStream(output, FileMode.Create))
            using (var inputFs = new FileStream(input, FileMode.Open))
            using (var cryptoFs = crypto.Write(outputFs))
            {
                inputFs.CopyTo(cryptoFs);
            }
        }

        private static void Help()
        {
            Console.WriteLine("Fantasy Tennis - Resource manager");
            Console.WriteLine("Usage:");
            Console.WriteLine("\tresman -d <input> <output>\t- decrypt <input> into <output>");
            Console.WriteLine("\tresman -e <input> <output>\t- encrypt <input> into <output>");
            Console.WriteLine("\tresman -a <input>\t- decrypt/encrypt auto");
            Environment.Exit(1);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseCore()
                .UseResources();
    }
}
