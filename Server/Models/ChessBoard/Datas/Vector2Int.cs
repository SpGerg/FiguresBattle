namespace Server.Models.Map.Datas
{
    public struct Vector2Int
    {
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public static bool operator ==(Vector2Int left, Vector2Int right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Vector2Int left, Vector2Int right)
        {
            return left.X != right.X && left.Y != right.Y;
        }

        public static Vector2Int operator +(Vector2Int left, Vector2Int right)
        {
            return new Vector2Int(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2Int operator -(Vector2Int left, Vector2Int right)
        {
            return new Vector2Int(left.X - right.X, left.Y - right.Y);
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is not Vector2Int vector2Int)
            {
                return false;
            }

            return vector2Int == this;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
