﻿using FastReslectionForHabrahabr.Helpers;
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
        private readonly IRawStringParser _normalizer;
        private readonly IStorage _db;
        protected static readonly string _typeName;
        protected static readonly IEnumerable<ContactMapSchema> _mapSchemas = MockHelper.GetFakeData();

        static ContactHydratorBase()
        {
            var type = typeof(Contact);
            _typeName = type.FullName;
        }

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
            var result = new List<PropertyToValueCorrelation>(10);
            var mailPairs = _normalizer.ParseWithoutLinq(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas = _mapSchemas.ToArray();
            foreach (var item in mapSchemas)
            {
                if (!item.EntityName.Equals(_typeName, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                foreach(var pair in mailPairs)
                {
                    if (!pair.Key.Equals(item.Key, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    result.Add(new PropertyToValueCorrelation {  PropertyName = item.Property, Value = pair.Value});
                }
            }
            return result.ToArray();
        }

        protected abstract Contact GetContact(PropertyToValueCorrelation[] correlation);

        private PropertyToValueCorrelation[] GetPropertiesValues(string rawData, CancellationToken abort)
        {
            var mailPairs = _normalizer.ParseWithLinq(rawData: rawData, pairDelimiter: Environment.NewLine);
            var mapSchemas = 
                _mapSchemas
                .Where(x => x.EntityName.ToUpperInvariant() == _typeName.ToUpperInvariant())
                .Select(x => new { x.Key, x.Property })
                .ToArray();

            return
                mailPairs
                .Join(mapSchemas, x => x.Key, x => x.Key, 
                    (x, y) => new PropertyToValueCorrelation { PropertyName = y.Property, Value = x.Value })
                .ToArray();
        }
    }
}
