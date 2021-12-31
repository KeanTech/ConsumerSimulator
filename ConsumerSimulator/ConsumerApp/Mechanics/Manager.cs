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
        public EventHandler CountUpdate { get; set; }
        public event PropertyChangedEventHandler Update;
        public Manager()
        {
            Producer = new Producer();
        }

        public async Task<bool> Loop()
        {
            try
            {
                await Producer.StartProducer();
                while (Producer.Running)
                {
                    Thread.Sleep(10);
                }

                Update?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
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
