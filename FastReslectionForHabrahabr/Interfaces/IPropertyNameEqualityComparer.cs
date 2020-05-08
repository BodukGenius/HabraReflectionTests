using System;
using System.Collections.Generic;
using System.Text;

namespace FastReslectionForHabrahabr.Interfaces
{
    public interface IPropertyNameEqualityComparer : IEqualityComparer<string>
    {
        string Transform(string propertyName);
    }
}
