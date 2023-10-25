namespace NewsReader.Domain.GoogleTranslate
{
    public interface ITranslateProvider
    {
        Task<string> ExecuteAsync(string text, string language, CancellationToken cancellationToken);
    }
}
