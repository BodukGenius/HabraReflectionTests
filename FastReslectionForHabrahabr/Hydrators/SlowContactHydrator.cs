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
    public class SlowContactHydrator : ContactHydratorBase
    {
        private static readonly Dictionary<string, PropertyInfo> _properties;

        static SlowContactHydrator()
        {
            var type = typeof(Contact);
            _properties = type.GetProperties().ToDictionary(x => x.Name);
        }

        public SlowContactHydrator(IRawStringParser normalizer, IStorage db) : base(normalizer, db)
        {
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var kv in correlations)
            {
                if (_properties.TryGetValue(kv.PropertyName, out var property))
                    property.SetValue(contact, kv.Value);
            }
            return contact;
        }
    }
}
