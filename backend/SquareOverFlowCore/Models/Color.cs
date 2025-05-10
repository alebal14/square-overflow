namespace SquareOverFlowCore.Models
{
    public class Color : IEquatable<Color>
    {
        public byte Red { get; set; } = 0;
        public byte Green { get; set; } = 0;
        public byte Blue { get; set; } = 0;

        public bool Equals(Color? other)
        {
            if (other == null) return false;
            return Red == other.Red && Green == other.Green && Blue == other.Blue;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Color);
        }

        public override int GetHashCode()
        {            
            return (Red << 16) | (Green << 8) | Blue;
        }

        public static bool operator ==(Color left, Color right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }
    }
}
