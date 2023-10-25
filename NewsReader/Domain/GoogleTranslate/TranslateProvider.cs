using Google.Cloud.Translate.V3;

namespace NewsReader.Domain.GoogleTranslate
{
    public class TranslateProvider : ITranslateProvider
    {
        private static readonly string _projectId = "";

        public async Task<string> ExecuteAsync(string text, string language, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            if (string.IsNullOrWhiteSpace(language))
            {
                throw new ArgumentException();
            }

            TranslationServiceClient client = await TranslationServiceClient.CreateAsync();

            var response = await client.TranslateTextAsync($"projects/{_projectId}", language, new string[] { text }, cancellationToken);

            // Will always contain a single entry
            return response.Translations[0].TranslatedText;
        }
    }
}
