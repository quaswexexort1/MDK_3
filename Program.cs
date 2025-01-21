
interface IInteger
{
    IInteger Add(IInteger other);
    IInteger Subtract(IInteger other);
    IInteger Multiply(IInteger other);
    void Print();
    int Count { get; }
}

abstract class Integer : IInteger
{
    List<int> digits;

    public abstract IInteger Add(IInteger other);
    public abstract IInteger Subtract(IInteger other);
    public abstract IInteger Multiply(IInteger other);
    protected List<int> GetDigits() => digits;
    public int Count => digits.Count;
}

class Decimal : Integer
{
    public Decimal(string v) : base(v) { }
    public Decimal(List<int> d) : base(d) { }
    public override IInteger Add(IInteger o)
    {
        var a = GetDigits(); var b = ((Decimal)o).GetDigits();
        int c = 0, m = Math.Max(a.Count, b.Count);
        return new Decimal(r);
    }

    public override IInteger Subtract(IInteger o)
    {
        var a = GetDigits(); var b = ((Decimal)o).GetDigits();
        int c = 0, m = a.Count;
        return new Decimal(r);
    }

    public override IInteger Multiply(IInteger o)
    {
        var a = GetDigits(); var b = ((Decimal)o).GetDigits();
        int m = a.Count, n = b.Count;

        return new Decimal(r);
    }

    class Binary : Integer
    {
        public override IInteger Add(IInteger o)
        {
            var a = GetDigits(); var b = ((Binary)o).GetDigits();
            int c = 0, m = Math.Max(a.Count, b.Count);
            return new Binary(r);
        }

        public override IInteger Subtract(IInteger o)
        {
            var a = GetDigits(); var b = ((Binary)o).GetDigits();
            int c = 0, m = a.Count;
            return new Binary(r);
        }

        public override IInteger Multiply(IInteger o)
        {
            var a = GetDigits(); var b = ((Binary)o).GetDigits();
            int m = a.Count, n = b.Count;
            return new Binary(r);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        IInteger d1 = new Decimal("123");
        IInteger d2 = new Decimal("456");
        Console.WriteLine("Decimal:");
        Console.Write($"{d1} + {d2} = "); d1.Add(d2).Print();
        Console.Write($"{d2} - {d1} = "); d2.Subtract(d1).Print();
        Console.Write($"{d1} * {d2} = "); d1.Multiply(d2).Print();
        Console.WriteLine();

        IInteger b1 = new Binary("101");
        IInteger b2 = new Binary("110");
        Console.WriteLine("Binary:");
        Console.Write($"{b1} + {b2} = "); b1.Add(b2).Print();
        Console.Write($"{b2} - {b1} = "); b2.Subtract(b1).Print();
        Console.Write($"{b1} * {b2} = "); b1.Multiply(b2).Print();
        Console.ReadKey();
    }
}

