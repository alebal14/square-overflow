using SquareOverFlowCore.Models;

namespace SquareOverFlowCore
{
    public interface ISquareService
    {
        List<Square> AddSquare();
        List<Square> DeleteSquares();
        List<Square> GetAllSquares();
    }
}