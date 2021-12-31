namespace ConsumerApp.Models
{
    public class Item
    {
        private static int number;
        public string Name { get; set; }
        public int Id { get; set; }

        public Item(string name = "Soda")
        {
            Id = number;
            number++;
        }

        public override string ToString()
        {
            return $"Item: $#{Name}#$\nId: {Id} ";
        }
    }
}
