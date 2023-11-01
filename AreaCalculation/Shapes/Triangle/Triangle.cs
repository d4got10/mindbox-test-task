using AreaCalculation.Core;

namespace AreaCalculation.Shapes.Triangle;

public readonly record struct Triangle() : IShape
{
    public double[] SideLengths { get; } = new double[3];
}