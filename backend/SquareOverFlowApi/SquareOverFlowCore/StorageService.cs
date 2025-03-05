using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SquareOverFlowCore.Extensions;
using SquareOverFlowCore.Models;
using System.Text.Json;

namespace SquareOverFlowCore
{
    public class StorageService : IStorageService
    {
        private readonly string _dataFilePath;
        private readonly ILogger<StorageService> _logger;
        private readonly IConfiguration _configuration;

        public StorageService(ILogger<StorageService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _dataFilePath = configuration["Values:DataFilePath"];
            _logger = logger;
        }

        public List<Square> ReadFile()
        {
            try
            {
                if (!File.Exists(_dataFilePath))
                {
                    _logger.LogInformation("Data file not found at {FilePath}, returning empty list", _dataFilePath);
                    return new List<Square>();
                }

                string json = File.ReadAllText(_dataFilePath);
                return JsonSerializer.Deserialize<List<Square>>(json) ?? new List<Square>();
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "IO error occurred while reading from file: {FilePath}", _dataFilePath);
                throw new StorageException("Failed to read squares data from file.", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred with file: {FilePath}", _dataFilePath);
                throw new StorageException("Failed to parse squares data from file.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while reading from file: {FilePath}", _dataFilePath);
                throw new StorageException("An unexpected error occurred while reading squares data.", ex);
            }
        }

        public void WriteFile(List<Square> squares)
        {
            try
            {
                // Skapa katalogen om den inte finns
                string? directory = Path.GetDirectoryName(_dataFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                string updatedJson = JsonSerializer.Serialize(squares, options);
                File.WriteAllText(_dataFilePath, updatedJson);

            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "IO error occurred while writing to file: {FilePath}", _dataFilePath);
                throw new StorageException("Failed to save squares data to file.", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON serialization error occurred with file: {FilePath}", _dataFilePath);
                throw new StorageException("Failed to serialize squares data.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while writing to file: {FilePath}", _dataFilePath);
                throw new StorageException("An unexpected error occurred while saving squares data.", ex);
            }
        }

        public List<Square> DeleteFile()
        {
            try
            {
                // Check if file exists before attempting to delete
                if (File.Exists(_dataFilePath))
                {
                    File.Delete(_dataFilePath);
                    _logger.LogInformation("Successfully deleted file: {FilePath}", _dataFilePath);
                }
                else
                {
                    _logger.LogInformation("File not found, nothing to delete: {FilePath}", _dataFilePath);
                }

                return new List<Square>();
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "IO error occurred while deleting file: {FilePath}", _dataFilePath);
                throw new StorageException("Failed to delete squares data file.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Access denied while attempting to delete file: {FilePath}", _dataFilePath);
                throw new StorageException("Access denied when trying to delete squares data file.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting file: {FilePath}", _dataFilePath);
                throw new StorageException("An unexpected error occurred while deleting squares data file.", ex);
            }
        }
    }
}
