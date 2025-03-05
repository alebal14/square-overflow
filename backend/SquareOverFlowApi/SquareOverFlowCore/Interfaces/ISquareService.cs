using SquareOverFlowCore.Models;

namespace SquareOverFlowCore.Interfaces
{
    public interface ISquareService
    {
        List<Square> ClearSquaresStorage();
        List<Square> GenerateSquare();
        List<Square> LoadSquaresFromStorage();
    }
}