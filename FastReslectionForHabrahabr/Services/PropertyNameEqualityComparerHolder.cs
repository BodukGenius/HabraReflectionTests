using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FastReslectionForHabrahabr.Services
{
    public sealed class PropertyNameEqualityComparerHolder
    {
        public static readonly Interfaces.IPropertyNameEqualityComparer Instance = ReferenceEqualityComparer.Instance;

        private sealed class ReferenceEqualityComparer : Interfaces.IPropertyNameEqualityComparer
        {
            public static readonly Interfaces.IPropertyNameEqualityComparer Instance = new ReferenceEqualityComparer();

            private ReferenceEqualityComparer() { }

            public string Transform(string propertyName) => string.Intern(propertyName);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(string x, string y) => ReferenceEquals(x, y);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int GetHashCode(string obj) => RuntimeHelpers.GetHashCode(obj);
        }

        private sealed class DefaultEqualityComparer : Interfaces.IPropertyNameEqualityComparer
        {
            public static readonly Interfaces.IPropertyNameEqualityComparer Instance = new DefaultEqualityComparer();

            private DefaultEqualityComparer() { }

            public bool Equals(string x, string y) => x == y;
            public int GetHashCode(string obj) => obj?.GetHashCode() ?? 0;
            public string Transform(string propertyName) => propertyName;
        }
    }
}
