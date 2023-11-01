using AreaCalculation.Core;

namespace AreaCalculation.Shapes.Circle;

public readonly record struct Circle : IShape
{
    public double Radius { get; init; }
}