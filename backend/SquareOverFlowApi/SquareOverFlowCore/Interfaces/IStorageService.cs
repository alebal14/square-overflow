using SquareOverFlowCore.Models;

namespace SquareOverFlowCore.Interfaces
{
    public interface IStorageService
    {
        List<Square> DeleteFile();
        List<Square> ReadFile();
        void WriteFile(List<Square> squares);
    }
}