using COM.API.Infrastructure.Interfaces;

namespace COM.API.Infrastructure.Integration
{
    /// <summary>
    /// Клиент для загрузки файлов в File Storage Service.
    /// Используется для загрузки актов открытия объекта (PDF/изображения).
    /// </summary>
    public class FileStorageClient : IFileStorageClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _uploadEndpoint;

        public FileStorageClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            var baseAddress = configuration["FileStorageService:BaseUrl"]
                ?? throw new InvalidOperationException("FileStorageService:BaseUrl не настроен.");

            _uploadEndpoint = new Uri(new Uri(baseAddress), "/upload").ToString();
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
            if (fileStream == null)
                throw new ArgumentNullException(nameof(fileStream));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Имя файла не может быть пустым.", nameof(fileName));

            // Сбрасываем позицию потока на начало (на случай, если он уже был прочитан)
            if (fileStream.CanSeek)
                fileStream.Position = 0;

            using var formData = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            formData.Add(fileContent, "file", fileName);

            var response = await _httpClient.PostAsync(_uploadEndpoint, formData, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FileUploadResponse>(cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("File Storage Service вернул пустой ответ.");

            return result.FileId ?? throw new InvalidOperationException("File Storage Service не вернул fileId.");
        }

        // Вспомогательный класс для десериализации ответа
        private class FileUploadResponse
        {
            public string? FileId { get; set; }
        }
    }
}
