struct Pos
{
    public int X { get; set; }
    public int Y { get; set; }

    public Pos(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Pos operator +(Pos a)
    {
        a.X += a.X;
        a.Y += a.Y;
        return a;
    }
    public static Pos operator +(Pos a, Pos b)
    {
        return new Pos(a.X + b.X, a.Y + b.Y);
    }
    public static Pos operator -(Pos a, Pos b)
    {
        return new Pos(a.X - b.X, a.Y - b.Y);
    }
    public static bool operator ==(Pos a, Pos b)
    {
        return a.X == b.X && a.Y == b.Y;
    }
    public static bool operator ==(Pos a, (int, int) b)
    {
        return a.X == b.Item1 && a.Y == b.Item2;
    }
    public static bool operator !=(Pos a, Pos b)
    {
        return a.X != b.X || a.Y != b.Y;
    }
    public static bool operator !=(Pos a, (int, int) b)
    {
        return a.X != b.Item1 || a.Y != b.Item2;
    }
    public override bool Equals(object o)
    {
        if (!(o is Pos))
            return false;
        return this == (Pos)o;
    }
    public bool Outside(Pos area)
    {
        return (X > area.X) ||
               (Y > area.Y) ||
               (X < 0) ||
               (Y < 0);
    }
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}