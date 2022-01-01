using ConsumerApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerApp.Mechanics
{
    public class Consumer
    {
        public Stack<Item> Items { get; set; }
        public Item Item { get; set; }
        public bool Running { get; set; }
        public Consumer()
        {
            Items = new Stack<Item>();
            Items.Push(new Item());
            Items.Push(new Item());
            Items.Push(new Item());
            Items.Push(new Item());
            Items.Push(new Item());
            Items.Push(new Item());
        }

        public async Task<bool> StartConsumer()
        {
            Running = true;
            Item = await Consume();
            Running = false;
            return true;
        }

        private async Task<Item> Consume()
        {
            Task<Item> task = Task<Item>.Factory.StartNew(() =>
            {
                Task.Delay(100);
                var item = Items.Pop();
                return item;
            });
             
            return await Task.FromResult(await task);
        }
    }
}
