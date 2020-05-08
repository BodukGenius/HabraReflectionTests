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
        public async Task FastHydrationLinq()
        {
            foreach (var data in _BenchData)
            {
                await HydrateWithLinq(_FastContactHydrator, data);
            }
        }

        [Benchmark]
        public async Task FastHydration()
        {
            foreach (var data in _BenchData)
            {
                await HydrateWithoutLinq(_FastContactHydrator, data);
            }
        }

        [Benchmark]
        public async Task SlowHydrationLinq()
        {
            foreach (var data in _BenchData)
            {
                await HydrateWithLinq(_SlowContactHydrator, data);
            }
        }

        [Benchmark]
        public async Task SlowHydration()
        {
            foreach (var data in _BenchData)
            {
                await HydrateWithoutLinq(_SlowContactHydrator, data);
            }
        }

        [Benchmark]
        public async Task ManualHydrationLinq()
        {
            foreach (var data in _BenchData)
            {
                await HydrateWithLinq(_ManualContactHydrator, data);
            }
        }


        [Benchmark(Baseline = true)]
        public async Task ManualHydration()
        {
            foreach (var data in _BenchData)
            {
                await HydrateWithoutLinq(_ManualContactHydrator, data);
            }
        }

        private static async Task HydrateWithoutLinq(IEntityHydrator<Contact> parser, string data)
        {
            var contact = await parser.HydrateWithoutLinq(data, CancellationToken.None);
        }

        private static async Task HydrateWithLinq(IEntityHydrator<Contact> parser, string data)
        {
            var contact = await parser.HydrateWithLinq(data, CancellationToken.None);
        }
    }
}
