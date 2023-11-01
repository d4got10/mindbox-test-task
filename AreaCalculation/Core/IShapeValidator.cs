using ErrorOr;

namespace AreaCalculation.Core;

public interface IShapeValidator
{
}

public interface IShapeValidator<in T> : IShapeValidator
{
    ErrorOr<Success> Validate(T shape);
}