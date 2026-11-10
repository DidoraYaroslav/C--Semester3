using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        if (!TryReadSide("a", out double a) ||
            !TryReadSide("b", out double b) ||
            !TryReadSide("c", out double c))
        {
            Console.WriteLine("Please enter a valid number for each side.");
            return;
        }

        if (!AreSidesPositive(a, b, c))
        {
            Console.WriteLine("All sides must be positive.");
            return;
        }

        if (!CanFormTriangle(a, b, c))
        {
            Console.WriteLine("These sides cannot form a triangle.");
            return;
        }

        double perimeter = CalculatePerimeter(a, b, c);
        double area = CalculateArea(a, b, c);

        if (!double.IsFinite(perimeter) || !double.IsFinite(area) || area <= 0)
        {
            Console.WriteLine("These lengths are outside the calculation range.");
            return;
        }

        Console.WriteLine($"Perimeter: {perimeter:G10}");
        Console.WriteLine($"Area: {area:G10}");
        Console.WriteLine($"Triangle type: {GetTriangleType(a, b, c)}");
    }

    static bool TryReadSide(string name, out double side)
    {
        Console.Write($"Enter side {name}: ");
        string input = (Console.ReadLine() ?? "").Replace(',', '.');

        return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out side)
            && double.IsFinite(side);
    }

    static bool AreSidesPositive(double a, double b, double c)
    {
        return a > 0 && b > 0 && c > 0;
    }

    static bool CanFormTriangle(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    static double CalculatePerimeter(double a, double b, double c)
    {
        return a + b + c;
    }

    static double CalculateArea(double a, double b, double c)
    {
        double p = CalculatePerimeter(a, b, c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    static string GetTriangleType(double a, double b, double c)
    {
        if (a == b && b == c)
        {
            return "Equilateral";
        }

        bool isIsosceles = a == b || a == c || b == c;

        if (IsRightTriangle(a, b, c))
        {
            return isIsosceles ? "Right and isosceles" : "Right";
        }

        return isIsosceles ? "Isosceles" : "Scalene";
    }

    static bool IsRightTriangle(double a, double b, double c)
    {
        // Move the longest side into c.
        if (a > c)
        {
            (a, c) = (c, a);
        }

        if (b > c)
        {
            (b, c) = (c, b);
        }

        double ratioA = a / c;
        double ratioB = b / c;
        double sumOfSquares = ratioA * ratioA + ratioB * ratioB;

        return Math.Abs(sumOfSquares - 1) < 1e-10;
    }
}