using SquareOverFlowCore.Models;

namespace SquareOverFlowCore.Interfaces
{
    public interface ISquareService
    {
        List<Square> ClearSquaresStorage();
        Square GenerateSquare();
        List<Square> LoadSquaresFromStorage();
    }
}