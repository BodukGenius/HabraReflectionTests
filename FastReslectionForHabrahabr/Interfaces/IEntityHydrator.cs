﻿using FastReslectionForHabrahabr.Hydrators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastReslectionForHabrahabr.Interfaces
{
    public interface IEntityHydrator<TEntity> where TEntity : class
    {
        Contact HydrateWithLinq(string rawData, CancellationToken abort);
        Contact HydrateWithoutLinq(string rawData, CancellationToken abort);
    }
}
