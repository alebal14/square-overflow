using SquareOverFlowCore.Models;

namespace SquareOverFlowCore
{
    public class SquareService : ISquareService
    {
        private readonly IStorageService _storage;

        public SquareService(IStorageService storage)
        {
            _storage = storage;
        }

        public List<Square> GetAllSquares()
        {
            return _storage.ReadFile();
        }

        public List<Square> AddSquare()
        {
            var squares = GetAllSquares();

            Position newPosition = CalculateNextPosition(squares);
            Color newColor = GenerateRandomColor();

            Square newSquare = new()
            {
                Position = newPosition,
                Color = newColor
            };

            squares.Add(newSquare);

            _storage.WriteFile(squares);

            return squares;
        }

        public List<Square> DeleteSquares()
        {
            return _storage.DeleteFile();
        }


        private Position CalculateNextPosition(List<Square> squares)
        {
            if (!squares.Any())
            {
                return new Position();
            }

            int maxX = squares.Max(s => s.Position.X);
            int maxY = squares.Max(s => s.Position.Y);

            var lastSquare = squares[^1]; ;

            //start a new column
            if (lastSquare.Position.Y == 0)
            {
                return new Position { X = 0, Y = lastSquare.Position.X + 1 };
            }

            //and move down
            if (lastSquare.Position.Y == maxY && lastSquare.Position.X != lastSquare.Position.Y)
            {
                return new Position { X = lastSquare.Position.X + 1, Y = lastSquare.Position.Y };
            }

            //move left
            if (lastSquare.Position.X == maxX && lastSquare.Position.Y > 0)
            {
                return new Position { X = lastSquare.Position.X, Y = lastSquare.Position.Y - 1 };
            }

            return lastSquare.Position;

        }

        private Color GenerateRandomColor()
        {
            Random random = new Random();

            Color newColor = new Color()
            {
                Red = random.Next(0, 256),
                Green = random.Next(0, 256),
                Blue = random.Next(0, 256)
            };

            return newColor;
        }

    }
}
