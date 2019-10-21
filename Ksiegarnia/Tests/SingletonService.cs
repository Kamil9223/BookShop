using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Tests
{
    public class SingletonService
    {
        public List<SampleService> samples = new List<SampleService>();

        public SingletonService()
        {
            for (int i = 0; i < 5; i++)
                samples.Add(new SampleService(i));
        }


    }
}
