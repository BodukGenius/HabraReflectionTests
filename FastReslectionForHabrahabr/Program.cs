using BenchmarkDotNet.Running;
using FastReslectionForHabrahabr.Helpers;
using FastReslectionForHabrahabr.Hydrators;
using FastReslectionForHabrahabr.Services;
using System;

namespace FastReslectionForHabrahabr
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fch = new FastContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
            BenchmarkRunner.Run(typeof(Benchmarks));
            Console.ReadKey();
        }
    }
}
