using ConsumerApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerApp.Mechanics
{
    public class Producer
    {
        public Stack<Item> Items { get; set; }
        public int ItemCount { get; set; }
        public bool Running { get; set; }

        public Producer()
        {
            Items = new();
        }

        /// <summary>
        ///  Produces an item for later consumtion
        /// </summary>
        /// <returns>Item</returns>
        private async Task<Item> Produce()
        {
            Task<Item> item = Task<Item>.Factory.StartNew(() =>
            {
                Task.Delay(100);
                return new Item();
            });
            ItemCount++;
            return await Task.FromResult(await item);
        }

        /// <summary>
        /// Starts the Producer 
        /// </summary>
        public async Task<bool> StartProducer()
        {
            Running = true;

            try
            {
                var item = await Produce();
                Items.Push(item);
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
