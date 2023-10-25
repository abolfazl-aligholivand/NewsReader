namespace NewsReader.Domain.Models
{
    public class Website
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string FeedLink { get; set; }
        public int FKCategoryId { get; set; }
        public virtual WebsiteCategory FKCategory { get; set; }
        public virtual ICollection<News> Newses { get; set; }
    }
}
