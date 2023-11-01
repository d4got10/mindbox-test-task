using AreaCalculation.Core;

namespace AreaCalculation.Shapes.Triangle;

public class TriangleAreaCalculationStrategy : IAreaCalculationStrategy<Triangle>
{
    private const double EqualityCheckPrecision = 0.000_000_1;
    
    public double Calculate(Triangle shape)
    {
        var lengths = new double[3];
        shape.SideLengths.CopyTo(lengths, 0);
        Array.Sort(lengths);
        
        if (IsRightTriangle(lengths))
        {
            return CalculateForRightTriangle(lengths);
        }

        return CalculateForGenericTriangle(lengths);
    }

    private bool IsRightTriangle(double[] lengths)
    {
        double maxDifference = lengths[2] * lengths[2] * EqualityCheckPrecision;
        double squareDifference = lengths[0] * lengths[0] + lengths[1] * lengths[1] - lengths[2] * lengths[2];

        return Math.Abs(squareDifference) < maxDifference;
    }
    
    private double CalculateForRightTriangle(double[] lengths)
    {
        return 0.5 * lengths[0] * lengths[1];
    }

    private double CalculateForGenericTriangle(double[] lengths)
    {
        double p = 0.5 * (lengths[0] + lengths[1] + lengths[2]);
        double squaredS = p * (p - lengths[0]) * (p - lengths[1]) * (p - lengths[2]);
        return Math.Sqrt(squaredS);
    }
}