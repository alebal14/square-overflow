using SquareOverFlowCore.Models;

namespace SquareOverFlowCore
{
    public interface IStorageService
    {
        List<Square> DeleteFile();
        List<Square> ReadFile();
        void WriteFile(List<Square> squares);
    }
}