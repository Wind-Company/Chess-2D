
namespace Chess.Engine
{
    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator +(Point direction1, Point direction2)
        {
            return new Point(direction1.x + direction2.x, direction1.y + direction2.y);
        }
        public static Point operator -(Point direction1, Point direction2)
        {
            return new Point(direction1.x - direction2.x, direction1.y - direction2.y);
        }
        public static Point operator *(Point direction, int number)
        {
            return new Point(direction.x * number, direction.y * number);
        }
        public static Point operator /(Point direction, int number)
        {
            return new Point(direction.x / number, direction.y / number);
        }
        public static implicit operator Point(Position position)
        {
            return new Point((int)position % 8, (int)position / 8);
        }
        public static implicit operator Position(Point point)
        {
            if (point.x < 0 || point.x >= 8 || point.y < 0 || point.y >= 8) return Position.Missing;
            return (Position)(point.y * 8 + point.x);
        }
    }
}