using System.Collections.Generic;

namespace FastReslectionForHabrahabr.Interfaces
{
    using StrKeyValuePair = KeyValuePair<string, string>;

    public interface IRawStringParser
    {
        IEnumerable<StrKeyValuePair> ParseWithLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";");
        IEnumerable<StrKeyValuePair> ParseWithoutLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";");
    }
}