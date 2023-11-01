namespace AreaCalculation.Core;

public interface IAreaCalculationStrategy
{
}

public interface IAreaCalculationStrategy<in T> : IAreaCalculationStrategy
{
    double Calculate(T shape);
}