using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface ITextDataStore : IReadOnlyDictionary<string, string>
    {
    }
}
