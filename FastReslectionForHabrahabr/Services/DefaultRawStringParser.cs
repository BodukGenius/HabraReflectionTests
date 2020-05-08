using FastReslectionForHabrahabr.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastReslectionForHabrahabr.Services
{
    using StrKeyValuePair = KeyValuePair<string, string>;
    public class DefaultRawStringParser : IRawStringParser
    {
        private static readonly string _unrecognizedKey = "Unrecognized";

        public IEnumerable<StrKeyValuePair> ParseWithLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
            => rawData?.Split(pairDelimiter)
            .Select(x => x.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Length == 2 ? new StrKeyValuePair(key : x[0].Trim(), value: x[1].Trim()) : new StrKeyValuePair(key: _unrecognizedKey, value : x[0].Trim()))
            .ToList()
            ?? Enumerable.Empty<StrKeyValuePair>();

        public IEnumerable<StrKeyValuePair> ParseWithoutLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
        {            
            if (string.IsNullOrEmpty(rawData))
                return Enumerable.Empty<StrKeyValuePair>();

            var splitted = rawData.Split(pairDelimiter);
            var result = new List<StrKeyValuePair>(splitted.Length);
            foreach (var item in splitted)
            {
                var pair = item.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                    result.Add(new StrKeyValuePair(pair[0].Trim(), pair[1].Trim()));
                else
                    result.Add(new StrKeyValuePair(_unrecognizedKey, pair[0].Trim()));
            }
            return result;
        }
    }
}
