namespace NewsReader.Domain.Models
{
    public class WebsiteCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public virtual ICollection<Website> Websites { get; set; }
    }
}
