using AreaCalculation.Core;

namespace AreaCalculation.Shapes.Circle;

public class CircleAreaCalculationStrategy : IAreaCalculationStrategy<Circle>
{
    public double Calculate(Circle shape)
    {
        return shape.Radius * shape.Radius * Math.PI;
    }
}