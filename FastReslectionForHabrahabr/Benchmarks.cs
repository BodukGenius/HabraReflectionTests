using Autofac;
using BenchmarkDotNet.Attributes;
using FastReslectionForHabrahabr.Helpers;
using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.IoCModules;
using FastReslectionForHabrahabr.Hydrators;
using FastReslectionForHabrahabr.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FastReslectionForHabrahabr
{
    [MemoryDiagnoser]
    //[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmarks
    {
        
        private readonly FastContactHydrator _FastContactHydrator = new FastContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
        private readonly SlowContactHydrator _SlowContactHydrator = new SlowContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
        private readonly ManualContactHydrator _ManualContactHydrator = new ManualContactHydrator(new DefaultRawStringParser(), MockHelper.InstanceDb());
        private readonly string[] _BenchData = GetBenchData().ToArray();

        public static IEnumerable<string> GetBenchData()
        {
            var rn = Environment.NewLine;
            yield return $"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22";
        }


        [Benchmark]
        public void FastHydrationLinq()
        {
            foreach (var data in _BenchData)
            {
                HydrateWithLinq(_FastContactHydrator, data);
            }
        }

        [Benchmark]
        public void FastHydration()
        {
            foreach (var data in _BenchData)
            {
                HydrateWithoutLinq(_FastContactHydrator, data);
            }
        }

        [Benchmark]
        public void SlowHydrationLinq()
        {
            foreach (var data in _BenchData)
            {
                HydrateWithLinq(_SlowContactHydrator, data);
            }
        }

        [Benchmark]
        public void SlowHydration()
        {
            foreach (var data in _BenchData)
            {
                HydrateWithoutLinq(_SlowContactHydrator, data);
            }
        }

        [Benchmark]
        public void ManualHydrationLinq()
        {
            foreach (var data in _BenchData)
            {
                HydrateWithLinq(_ManualContactHydrator, data);
            }
        }


        [Benchmark(Baseline = true)]
        public void ManualHydration()
        {
            foreach (var data in _BenchData)
            {
                HydrateWithoutLinq(_ManualContactHydrator, data);
            }
        }

        private static void HydrateWithoutLinq(IEntityHydrator<Contact> parser, string data)
        {
            var contact = parser.HydrateWithoutLinq(data, CancellationToken.None);
        }

        private static void HydrateWithLinq(IEntityHydrator<Contact> parser, string data)
        {
            var contact = parser.HydrateWithLinq(data, CancellationToken.None);
        }
    }
}
