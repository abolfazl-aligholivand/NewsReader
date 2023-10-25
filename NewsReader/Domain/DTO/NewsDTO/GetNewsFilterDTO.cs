namespace NewsReader.Domain.DTO.NewsDTO
{
    public record GetNewsFilterDTO
    {
        public DateTime? Date { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
