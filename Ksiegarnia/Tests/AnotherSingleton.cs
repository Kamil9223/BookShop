using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Tests
{
    public class AnotherSingleton
    {
        private readonly SingletonService singletonService;

        public AnotherSingleton(SingletonService singletonService)
        {
            this.singletonService = singletonService;
        }

        public void SetSomething()
        {
            foreach (var sample in singletonService.samples)
                Console.WriteLine(sample.Number);
        }
    }
}
