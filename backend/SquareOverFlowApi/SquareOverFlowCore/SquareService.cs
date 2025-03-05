using SquareOverFlowCore.Interfaces;
using SquareOverFlowCore.Models;

namespace SquareOverFlowCore
{
    public class SquareService : ISquareService
    {
        private readonly IStorageService _storage;
        
        private HashSet<Color> _usedColors = [];
        public SquareService(IStorageService storage)
        {
            _storage = storage;
        }

        public List<Square> LoadSquaresFromStorage()
        {
            return _storage.ReadFile();
        }

        public List<Square> GenerateSquare()
        {
            var squares = LoadSquaresFromStorage();

            Position newPosition = DetermineNextSquarePosition(squares);
            Color newColor = GenerateRandomColor(squares);

            Square newSquare = new()
            {
                Position = newPosition,
                Color = newColor
            };

            squares.Add(newSquare);

            _storage.WriteFile(squares);

            return squares;
        }

        public List<Square> ClearSquaresStorage()
        {
            _usedColors.Clear();
            return _storage.DeleteFile();
        }

        private Position DetermineNextSquarePosition(List<Square> squares)
        {
            if (!squares.Any())
            {
                return new Position();
            }

            var lastSquare = squares[^1];
            int maxX = squares.Max(s => s.Position.X);
            int maxY = squares.Max(s => s.Position.Y);

            // Start a new column
            if (lastSquare.Position.Y == 0)
            {
                return new Position { X = 0, Y = lastSquare.Position.X + 1 };
            }

            // Move down when at the top of a column
            if (lastSquare.Position.Y == maxY && lastSquare.Position.X != lastSquare.Position.Y)
            {
                return new Position { X = lastSquare.Position.X + 1, Y = lastSquare.Position.Y };
            }

            // Move left when at the right edge
            if (lastSquare.Position.X == maxX && lastSquare.Position.Y > 0)
            {
                return new Position { X = lastSquare.Position.X, Y = lastSquare.Position.Y - 1 };
            }

            return lastSquare.Position;

        }

        private Color GenerateRandomColor(List<Square> squares)
        {
            var random = new Random();
            const int maxAttempts = 100;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                attempts++;

                Color newColor = new Color()
                {
                    Red = (byte)random.Next(0, 256),
                    Green = (byte)random.Next(0, 256),
                    Blue = (byte)random.Next(0, 256)
                };

                if (!_usedColors.Contains(newColor))
                {
                    _usedColors.Add(newColor);
                    return newColor;
                }
            }

            Color fallbackColor = new Color()
            {
                Red = (byte)random.Next(0, 256),
                Green = (byte)random.Next(0, 256),
                Blue = (byte)random.Next(0, 256)
            };

            _usedColors.Add(fallbackColor);
            return fallbackColor;

        }
    }
}
