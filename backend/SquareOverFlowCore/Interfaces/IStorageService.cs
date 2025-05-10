using SquareOverFlowCore.Models;

namespace SquareOverFlowCore.Interfaces
{
    public interface IStorageService
    {
        Task<List<Square>> DeleteFile();
        Task<List<Square>> ReadFileAsync();
        Task WriteFile(List<Square> squares);
    }
}