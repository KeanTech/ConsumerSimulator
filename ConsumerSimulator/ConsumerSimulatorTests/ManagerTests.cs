using ConsumerApp.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsumerSimulatorTests
{
    public class ManagerTests
    {
        [Fact]
        public async void LoopConsumer_ShouldReturnTrue()
        {
            Manager manager = new();
            manager.Consumer.Items.Push(new());
            Assert.True(await manager.LoopConsumer());
        }

        [Fact]
        public async void LoopProducer_ShouldReturnTrue()
        {
            Manager manager = new();
            Assert.True(await manager.LoopProducer());
        }

        [Fact]
        public async void UpdateItemList_ShouldReturnTrueAndMovedItemsFromList()
        {
            Manager manager = new();
            
            await manager.LoopProducer();
            Assert.NotEmpty(manager.Consumer.Items);
            Assert.Empty(manager.Producer.Items);
            
            await manager.LoopConsumer();
            Assert.Empty(manager.Consumer.Items);
        }
    }
}
