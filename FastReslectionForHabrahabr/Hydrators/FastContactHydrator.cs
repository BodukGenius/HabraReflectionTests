using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FastReslectionForHabrahabr.Hydrators
{
    public class FastContactHydrator : ContactHydratorBase
    {
        private static readonly Dictionary<string, Action<Contact, string>> _propertySettersMap;

        static FastContactHydrator()
        {
            var type = typeof(Contact);
            var properties = type.GetProperties();
            _propertySettersMap = new Dictionary<string, Action<Contact, string>>(properties.Length);

            foreach (var property in properties)
            {
                _propertySettersMap.Add(property.Name, GetSetterAction(property));
            }
        }

        private static Action<Contact, string> GetSetterAction(PropertyInfo property)
        {
            var setterInfo = property.GetSetMethod();
            var paramValueOriginal = Expression.Parameter(property.PropertyType, "value");
            var paramEntity = Expression.Parameter(typeof(Contact), "entity");
            var setterExp = Expression.Call(paramEntity, setterInfo, paramValueOriginal);
            
            var lambda = (Expression<Action<Contact, string>>)Expression.Lambda(setterExp, paramEntity, paramValueOriginal);

            return lambda.Compile();
        }

        public FastContactHydrator(IRawStringParser normalizer, IStorage db) : base(normalizer, db)
        {
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var kv in correlations)
            {
                if (_propertySettersMap.TryGetValue(kv.PropertyName, out var setter))
                    setter(contact, kv.Value);
            }
            return contact;
        }
    }
}
