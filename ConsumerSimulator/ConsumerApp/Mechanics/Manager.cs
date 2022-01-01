using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerApp.Mechanics
{
    public class Manager
    {
        public Producer Producer { get; set; }
        public Consumer Consumer { get; set; }
        public event PropertyChangedEventHandler UpdateProducer;
        public event PropertyChangedEventHandler UpdateConsumer;
        public Manager()
        {
            Producer = new Producer();
            Consumer = new Consumer();
        }

        public async Task<bool> RunAsync()
        {
            Random random = new Random();
            var produceNumberOfItems = random.Next(100);
            while (Producer.Items.Count < produceNumberOfItems)
                await LoopProducer();
            Debug.WriteLine("Producer Done");

            await UpdateItemList();
            var consumeNumberOfItems = random.Next(Consumer.Items.Count);
            while (Consumer.Items.Count > consumeNumberOfItems)
                await LoopConsumer();
            Debug.WriteLine("Consumer Done");
            return true;
        }

        public async Task<bool> LoopProducer()
        {
            try
            {
                await Producer.StartProducer();
                while (Producer.Running)
                {
                    Thread.Sleep(10);
                }

                UpdateProducer?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
                await Task.Delay(10);
                

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }


        public async Task<bool> LoopConsumer()
        {
            try
            {
                await Consumer.StartConsumer();
                while (Consumer.Running)
                {
                    Thread.Sleep(10);
                }

                UpdateConsumer?.Invoke(this, new PropertyChangedEventArgs(nameof(Consumer)));

                await Task.Delay(10);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        private async Task<bool> UpdateItemList()
        {
            while (Producer.Items.Count > 1 && Consumer.Items.Count < 100)
            {
                Consumer.Items.Push(Producer.Items.Pop());
                UpdateConsumer?.Invoke(this, new PropertyChangedEventArgs(nameof(Consumer)));
                UpdateProducer?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
            }

            await Task.Delay(1);
            return true;
        }

    }
}
