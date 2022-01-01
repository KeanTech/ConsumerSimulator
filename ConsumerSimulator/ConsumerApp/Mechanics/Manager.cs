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

        public async Task<bool> LoopProducer()
        {
            try
            {
                await Producer.StartProducer();
                while (Producer.Running)
                {
                    Thread.Sleep(10);
                }

                await UpdateItemList();

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
            var itemList = Producer.Items.ToArray();
            if (Consumer.Items.Count <= 100)
                for (int i = 0; i < Producer.Items.Count; i++)
                {
                    Consumer.Items.Push(itemList[i]);
                    Producer.Items.Pop();
                    UpdateConsumer?.Invoke(this, new PropertyChangedEventArgs(nameof(Consumer)));
                    UpdateProducer?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
                }

            await Task.Delay(1);
            return true;
        }

    }
}
