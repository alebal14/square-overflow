using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SquareOverFlowCore.Extensions;
using SquareOverFlowCore.Interfaces;
using SquareOverFlowCore.Models;
using System.Text.Json;

namespace SquareOverFlowCore
{
    public class StorageService : IStorageService
    {
        private readonly string _dataFilePath;
        private readonly ILogger<StorageService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public StorageService(ILogger<StorageService> logger, IConfiguration configuration)
        {
            _dataFilePath = configuration["Values:DataFilePath"]!;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        }

        public async Task<List<Square>> ReadFileAsync()
        {
            try
            {
                if (!File.Exists(_dataFilePath))
                {
                    _logger.LogInformation("Data file not found at {FilePath}, returning empty list", _dataFilePath);
                    return new List<Square>();
                }

                string json = await File.ReadAllTextAsync(_dataFilePath);
                return JsonSerializer.Deserialize<List<Square>>(json, _jsonOptions) ?? new List<Square>();
            }
            catch (Exception ex) when (ex is IOException || ex is JsonException)
            {
                _logger.LogError(ex, "Error reading squares from {FilePath}: {ErrorType}",
                    _dataFilePath, ex.GetType().Name);
                throw new StorageException($"Failed to read squares data: {ex.Message}", ex);
            }
        }

        public async Task WriteFile(List<Square> squares)
        {
            try
            {
                EnsureDirectoryExists();
                string updatedJson = JsonSerializer.Serialize(squares, _jsonOptions);
                await File.WriteAllTextAsync(_dataFilePath, updatedJson);
            }
            catch (Exception ex) when (ex is IOException || ex is JsonException)
            {
                _logger.LogError(ex, "Error writing squares to {FilePath}: {ErrorType}",
                    _dataFilePath, ex.GetType().Name);
                throw new StorageException($"Failed to save squares data: {ex.Message}", ex);
            }
        }

        public async Task<List<Square>> DeleteFile()
        {
            try
            {
                if (File.Exists(_dataFilePath))
                {
                    await Task.Run(() => File.Delete(_dataFilePath));
                    _logger.LogInformation("Successfully deleted file: {FilePath}", _dataFilePath);
                }
                else
                {
                    _logger.LogInformation("File not found, nothing to delete: {FilePath}", _dataFilePath);
                }

                return new List<Square>();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                _logger.LogError(ex, "Error deleting file {FilePath}: {ErrorType}",
                    _dataFilePath, ex.GetType().Name);
                throw new StorageException($"Failed to delete squares data: {ex.Message}", ex);
            }
        }

        private void EnsureDirectoryExists()
        {
            string? directory = Path.GetDirectoryName(_dataFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                _logger.LogInformation("Created directory: {Directory}", directory);
            }
        }
    }
}