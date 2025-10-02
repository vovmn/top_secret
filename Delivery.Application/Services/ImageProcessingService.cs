using Delivery.Application.DTOs.Requests;
using Delivery.Application.DTOs.Responses;
using Delivery.Core.Exceptions;
using Delivery.Domain.Entities;
using Delivery.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;

namespace Delivery.Application.Services
{
    public class ImageProcessingService
    {
        private readonly ILogger<ImageProcessingService> _logger;
        private readonly IFileStorageClient _fileStorageClient;

        private const string TessDataPath = @"./tessdata";
        private const string DefaultLanguage = "rus";
        private static readonly string[] AllowedImageExtensions =
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp"
        };

        public ImageProcessingService(ILogger<ImageProcessingService> logger, IFileStorageClient fileStorageClientt)
        {
            _logger = logger;
            _fileStorageClient = fileStorageClientt;
            ValidateTessData();
        }

        public async Task<TTNInfoResponseDto> ProcessImage(UploadTTNRequestDto request, CancellationToken cancellationToken = default)
        {
            if (request.File == null || string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentException("Файл не предоставлен.", nameof(request.File));

            if (!AllowedImageExtensions.Contains(Path.GetExtension(request.FileName)))
                throw new ImageProcessingException("Формат изображения не поддерживается.", new Exception());

            byte[] imageData;
            try
            {
                using var memoryStream = new MemoryStream();
                request.File.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("Error reading image stream", ex);
            }
            DeliveryDocument newdocument = await FetchFields(RecognizeText(imageData));

            await docrepository.Checklists.AddAsync(newdocument, cancellationToken);
            await docrepository.SaveChangesAsync(cancellationToken);

            return new()
            {
                Id = newdocument.Id,
                DocumentNumber = newdocument.DocumentNumber,
                CargoType = newdocument.CargoType,
                CargoVolume = newdocument.CargoVolume,
                VolumeUnit = newdocument.VolumeUnit,
                ShippedAt = newdocument.ShippedAt,
                PasportId = newdocument.PasportId,
            };
        }

        private string RecognizeText(byte[] imageData, string languageCode = DefaultLanguage)
        {
            try
            {
                using var engine = new TesseractEngine(TessDataPath, languageCode, EngineMode.Default);
                using var pixImage = LoadImage(imageData);
                using var page = engine.Process(pixImage);

                _logger.LogInformation($"Confidence: {page.GetMeanConfidence():P}");


                

                return page.GetText();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OCR processing failed");
                throw new OCRProcessingException("Text recognition failed", ex);
            }
        }

        private async Task<DeliveryDocument> FetchFields(string text)
        {
            // нужно найти как вычленить из набора текста нужные поля (обычно по рядомстоящим понятно)

            newdocument.PasportId = await _fileStorageClient.UploadFileAsync(request.File, request.FileName, cancellationToken);
        }

        private Pix LoadImage(byte[] imageData)
        {
            try
            {
                using var memoryStream = new MemoryStream(imageData);
                return PreprocessImage(Pix.LoadFromMemory(memoryStream.ToArray()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Image loading failed");
                throw new ImageProcessingException("Invalid image format", ex);
            }
        }

        private static Pix PreprocessImage(Pix image)
        {
            // Конвертация в grayscale
            var processed = image.ConvertRGBToGray();

            // Бинаризация
            processed = processed.BinarizeOtsuAdaptiveThreshold(16, 16, 0, 0, 1.0f);

            // Удаление шума
            //processed = processed.();

            return processed;
        }


        private static void ValidateTessData()
        {
            var requiredFiles = new[]
            {
            $"{DefaultLanguage}.traineddata",
            "osd.traineddata"
        };

            foreach (var file in requiredFiles)
            {
                var path = Path.Combine(TessDataPath, file);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(
                        $"Tesseract language data file not found: {file}",
                        path);
                }
            }
        }
    }
}
