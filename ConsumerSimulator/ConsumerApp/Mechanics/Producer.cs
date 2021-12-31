using ConsumerApp.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerApp.Mechanics
{
    public class Producer
    {
        public Stack<Item> Items { get; set; }
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
                while (Running)
                {
                    var item = await Produce();
                    Items.Push(item);
                    if (Items.Count == 100)
                    {
                        Running = false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
