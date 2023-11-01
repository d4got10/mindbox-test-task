using ErrorOr;

namespace AreaCalculation.Core;

public interface IShapeValidator
{
    ErrorOr<Success> Validate(IShape shape);
}

public interface IShapeValidator<in T> : IShapeValidator where T : IShape
{
    ErrorOr<Success> Validate(T shape);
}