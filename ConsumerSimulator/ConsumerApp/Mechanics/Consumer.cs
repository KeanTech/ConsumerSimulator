using ConsumerApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Items.Push(new Item());
            //Items.Push(new Item());
            //Items.Push(new Item());
            //Items.Push(new Item());
            //Items.Push(new Item());
            //Items.Push(new Item());
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

        public async Task<bool> StartConsumer()
        {
            try
            {
                Running = true;
                Item = await Consume();
                Running = false;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

    }
}
