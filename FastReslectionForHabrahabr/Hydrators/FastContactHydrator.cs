using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace FastReslectionForHabrahabr.Hydrators
{
    public class FastContactHydrator : ContactHydratorBase
    {
        private static readonly Action<Contact, ReadOnlyListWrapper> _Setter;

        private readonly struct ReadOnlyListWrapper
        {
            private readonly IReadOnlyList<PropertyToValueCorrelation> _PropertyToValueCorrelations;

            public int Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _PropertyToValueCorrelations.Count;
            }
            public PropertyToValueCorrelation this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _PropertyToValueCorrelations [index];
            }

            public ReadOnlyListWrapper(IReadOnlyList<PropertyToValueCorrelation> propertyToValueCorrelations)
                => _PropertyToValueCorrelations = propertyToValueCorrelations;
        }

        static FastContactHydrator()
        {
            var type = typeof(Contact);
            var properties = type.GetProperties();
            var comparer = Services.PropertyNameEqualityComparerHolder.Instance;

            var comparerType = comparer.GetType();

            var contactParameter = Expression.Parameter(typeof(Contact), "contact");
            var correlationsParameter = Expression.Parameter(typeof(ReadOnlyListWrapper), "correlations");

            var equalsMI = comparerType.GetMethod("Equals", new[] { typeof(string), typeof(string) });
            var getHashcodeMI = comparerType.GetMethod("GetHashCode", new[] { typeof(string) });

            var comparerVar = Expression.Variable(comparerType, "comparer");
            var indexVar = Expression.Variable(typeof(int), "i");

            var breakLoop = Expression.Label();

            var itemVar = Expression.Variable(typeof(PropertyToValueCorrelation), "item");
            var propNameVar = Expression.Variable(typeof(string), "propName");
            var hashcodeVar = Expression.Variable(typeof(int), "hashcode");


            var lambdaBlock = Expression.Block(new[] { comparerVar, indexVar },
                new Expression[]
                {
                    Expression.Assign(comparerVar,
                                Expression.Convert(
                                    Expression.Field(null
                                        , typeof(Services.PropertyNameEqualityComparerHolder)
                                        , nameof(Services.PropertyNameEqualityComparerHolder.Instance)
                                     ), comparerType)
                                ),
                    Expression.Assign(indexVar, Expression.Constant(0, typeof(int))),
                    Expression.Loop(
                        Expression.Block( new [] { itemVar, propNameVar, hashcodeVar },
                            new Expression[] {
                                    Expression.IfThen(
                                        Expression.GreaterThanOrEqual(indexVar, Expression.Property(correlationsParameter, nameof(ReadOnlyListWrapper.Count)))
                                        , Expression.Break(breakLoop)
                                    ),

                                    Expression.Assign(itemVar, Expression.Property(correlationsParameter, "Item", indexVar)),
                                    Expression.Assign(propNameVar, Expression.Property(itemVar, nameof(PropertyToValueCorrelation.PropertyName))),
                                    Expression.Assign(hashcodeVar, Expression.Call(comparerVar, getHashcodeMI, propNameVar)),
                                    //Expression.Call(null, typeof(Console).GetMethod("WriteLine", new [] { typeof(int) }), indexVar),

                                    getSwitchBlock(),

                                    Expression.PreIncrementAssign(indexVar) // i++;

                                }
                            )
                        , breakLoop),
                }).Reduce();

            var lambda = Expression.Lambda<Action<Contact, ReadOnlyListWrapper>>(lambdaBlock, contactParameter, correlationsParameter);
            _Setter = lambda.Compile();
            

            Expression getSwitchBlock()
            {
                var cases = (from pr in properties
                            let propertyName = comparer.Transform(pr.Name)
                            let hashCode = comparer.GetHashCode(propertyName)
                            group (Property: pr, Name: propertyName) by hashCode into grouped
                            select getSwitchCase(grouped.ToList(), grouped.Key)
                            ).ToArray();

                return Expression.Switch(hashcodeVar, cases);

                SwitchCase getSwitchCase(IReadOnlyList<(PropertyInfo Property, string Name)> grProperties, int hashcode)
                {
                    Expression getTest(string name) => Expression.Call(comparerVar, equalsMI, Expression.Constant(name, typeof(string)), propNameVar);
                    Expression assignProperty(PropertyInfo propertyInfo)
                        => Expression.Assign(Expression.Property(contactParameter, propertyInfo)
                                            , Expression.Property(itemVar, nameof(PropertyToValueCorrelation.Value))
                                            );

                    var info = grProperties[0];
                    var ifBlock = Expression.IfThen(getTest(info.Name), assignProperty(info.Property));
                        

                    for (int i = 1; i < grProperties.Count; i++)
                    {
                        info = grProperties[i];
                        ifBlock = Expression.IfThenElse(getTest(info.Name), assignProperty(info.Property), ifBlock);
                    }

                    var result = Expression.SwitchCase(ifBlock, Expression.Constant(hashcode, typeof(int)));
                    return result;
                }
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

        protected override Contact GetContact(IReadOnlyList<PropertyToValueCorrelation> correlations)
        {
            var contact = new Contact();
            _Setter(contact, new ReadOnlyListWrapper(correlations));
            return contact;
        }
    }
}
