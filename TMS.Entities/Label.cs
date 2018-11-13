namespace TMS.Entities
{
    public class Label
    {
        public int Id { get; set; }

        public UserApp User{ get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Color { get; set; }
    }
}
