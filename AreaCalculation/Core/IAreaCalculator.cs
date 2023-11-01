using ErrorOr;

namespace AreaCalculation.Core;

public interface IAreaCalculator
{
    ErrorOr<double> CalculateArea<T>(T shape) where T : IShape;
}