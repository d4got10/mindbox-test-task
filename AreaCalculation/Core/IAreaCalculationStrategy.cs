namespace AreaCalculation.Core;

public interface IAreaCalculationStrategy
{
    double Calculate(IShape shape);
}

public interface IAreaCalculationStrategy<in T> : IAreaCalculationStrategy where T : IShape
{
    double Calculate(T shape);
}