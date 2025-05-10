using SquareOverFlowCore.Models;

namespace SquareOverFlowCore.Interfaces
{
    public interface ISquareService
    {
        Task<List<Square>> ClearSquaresStorage();
        Task<Square> GenerateSquare();
        Task<List<Square>> LoadSquaresFromStorage();
    }
}