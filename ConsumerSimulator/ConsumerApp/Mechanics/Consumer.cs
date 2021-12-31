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
        public async Task<bool> StartConsumer()
        {
            Running = true;
            Item = await Consume();
            Running = false;
            return true;
        }

        private async Task<Item> Consume()
        {
            Task<Item> item = Task<Item>.Factory.StartNew(() =>
            {
                Task.Delay(100);
                return Items.Pop();
            });

            return await Task.FromResult(await item);
        }
    }
}
