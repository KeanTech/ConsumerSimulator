using System;
using System.ComponentModel;
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
                Update?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
                //CountUpdate?.Invoke(this, new PropertyChangedEventArgs(nameof(Producer)));
                await Task.Delay(10);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
