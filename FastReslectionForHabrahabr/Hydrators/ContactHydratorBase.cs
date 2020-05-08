using FastReslectionForHabrahabr.Helpers;
using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastReslectionForHabrahabr.Hydrators
{
    public abstract class ContactHydratorBase : IEntityHydrator<Contact>
    {
        private readonly struct MapSchemas
        {
            private readonly IReadOnlyDictionary<string, string> _Data;
            public MapSchemas(IEnumerable<ContactMapSchema> contactMapSchemas, string typeName)
            {
                typeName = typeName.ToLowerInvariant();
                _Data = contactMapSchemas.Where(x => x.EntityName.ToLowerInvariant() == typeName)
                    .ToDictionary(x => x.Key, x => x.Property, StringComparer.InvariantCultureIgnoreCase);
            }

            public bool TryGetProperty(string key, out string propertyName) => _Data.TryGetValue(key, out propertyName);
        }

        protected static readonly string _typeName;
        private static readonly MapSchemas _mapSchemas;

        static ContactHydratorBase()
        {
            var type = typeof(Contact);
            _typeName = type.FullName;
            _mapSchemas = new MapSchemas(MockHelper.GetFakeData(), _typeName);
        }

        private readonly IRawStringParser _normalizer;
        private readonly IStorage _db;

        public ContactHydratorBase(IRawStringParser normalizer, IStorage db)
        {
            _normalizer = normalizer;
            _db = db;
        }

        public Contact HydrateWithLinq(string rawData, CancellationToken abort)
            => GetContact(GetPropertiesValues(rawData, abort));

        public Contact HydrateWithoutLinq(string rawData, CancellationToken abort)
            => GetContact(GetPropertiesValuesWithoutLinq(rawData, abort));

        private PropertyToValueCorrelation[] GetPropertiesValuesWithoutLinq(string rawData, CancellationToken abort)
        {
            var result = new List<PropertyToValueCorrelation>();
            foreach(var mp in _normalizer.ParseWithoutLinq(rawData: rawData, pairDelimiter: Environment.NewLine))
            {
                if (_mapSchemas.TryGetProperty(mp.Key, out var propertyName))
                    result.Add(new PropertyToValueCorrelation
                    {
                        PropertyName = propertyName,
                        Value = mp.Value
                    });
            }

            return result.ToArray();
        }

        protected abstract Contact GetContact(PropertyToValueCorrelation[] correlation);

        private PropertyToValueCorrelation[] GetPropertiesValues(string rawData, CancellationToken abort)
        {
            return _normalizer.ParseWithLinq(rawData: rawData, pairDelimiter: Environment.NewLine)
                .Select(x => _mapSchemas.TryGetProperty(x.Key, out var propetyName) ? new PropertyToValueCorrelation { PropertyName = propetyName, Value = x.Value } : null)
                .ToArray();
        }
    }
}
