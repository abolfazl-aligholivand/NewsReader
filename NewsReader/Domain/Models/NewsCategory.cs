namespace NewsReader.Domain.Models
{
    public class NewsCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public virtual ICollection<News> Newses { get; set; }
    }
}
