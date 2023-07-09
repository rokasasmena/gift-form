namespace GiftFormAPI.Models
{
    public class Child
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public List<Gift> Gifts { get; set; }
    }
}
