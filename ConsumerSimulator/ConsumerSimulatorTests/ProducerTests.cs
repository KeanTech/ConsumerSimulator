using ConsumerApp.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsumerSimulatorTests
{
    public class ProducerTests
    {
        [Fact]
        public async void Produce_ShouldReturnNewItem()
        {
            Producer producer = new();

            await producer.StartProducer();
            Assert.NotEmpty(producer.Items);
        }

        [Fact]
        public async void StartProducer_ShouldReturnTrue()
        {
            Producer producer = new();

            Assert.True(await producer.StartProducer());
        }
    }
}
