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
        public EventHandler CountUpdate { get; set; }
        public event PropertyChangedEventHandler UpdateProducer;
        public event PropertyChangedEventHandler UpdateConsumer;
        public Manager()
        {
            Producer = new Producer();
            Consumer = new Consumer();
        }

        public void Start()
        {
            new Thread(async () => 
            {
                await LoopProducer();
            }).Start();

            new Thread(async () => 
            {
                await LoopConsumer();
            }).Start();

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
                //CountUpdate?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
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
                //CountUpdate?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
                await Task.Delay(10);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

    }
}
