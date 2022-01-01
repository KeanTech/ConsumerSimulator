using ConsumerApp.Mechanics;
using ConsumerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsumerSimulatorTests
{
    public class ConsumerTests
    {
        [Fact]
        public async void Consume_ShouldReturnItem()
        {
            Consumer consumer = new Consumer();
            consumer.Items.Push(new Item());
            consumer.Items.Push(new Item());

            var expected = consumer.Items.Count - 1;
            
            Assert.NotEmpty(consumer.Items);
            await consumer.StartConsumer();

            var actual = consumer.Items.Count;
            
            Assert.NotNull(consumer.Item);

            Assert.Equal(expected, actual);
        }
    }
}
