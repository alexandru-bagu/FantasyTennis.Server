using System;

namespace FTServer.Contracts
{
    public class DependsOnAttribute : Attribute
    {
        public Type[] Dependencies { get; }

        public DependsOnAttribute(params Type[] dependencies)
        {
            Dependencies = dependencies;
        }
    }
}
