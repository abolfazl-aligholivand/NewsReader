#nullable disable

namespace NewsReader.Domain.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string NewsGuid { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Media { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Link { get; set; }
        public string Creator { get; set; }

        public int FKCategoryId { get; set; }
        public virtual NewsCategory FKCategory { get; set; }
        public int FKWebsiteId { get; set; }
        public virtual Website FKWebsite { get; set; }
        //public virtual ICollection<NewsKeyword> NewsKeywords { get; set; }
    }
}
