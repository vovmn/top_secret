namespace COM.API.Infrastructure.Integration
{
    /// <summary>
    /// Клиент для загрузки файлов в File Storage Service.
    /// Используется для загрузки актов открытия объекта (PDF/изображения).
    /// </summary>
    public class FileStorageClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;

        public FileStorageClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseAddress = configuration["FileStorageService:BaseUrl"] ?? throw new InvalidOperationException("FileStorageService:BaseUrl не настроен.");
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        /// <summary>
        /// Загружает файл в File Storage Service и возвращает уникальный fileId.
        /// </summary>
        /// <param name="fileStream">Поток файла.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Уникальный идентификатор файла.</returns>
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
        {
            using var formData = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            formData.Add(fileContent, "file", fileName);

            var response = await _httpClient.PostAsync("/upload", formData, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FileUploadResponse>(cancellationToken: cancellationToken);
            return result?.FileId ?? throw new InvalidOperationException("File Storage Service не вернул fileId.");
        }

        // Вспомогательный класс для десериализации ответа
        private class FileUploadResponse
        {
            public string? FileId { get; set; }
        }
    }
}
