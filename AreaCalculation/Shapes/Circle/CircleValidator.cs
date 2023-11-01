using AreaCalculation.Core;
using ErrorOr;

namespace AreaCalculation.Shapes.Circle;

public class CircleValidator : IShapeValidator<Circle>
{
    public ErrorOr<Success> Validate(Circle shape)
    {
        if (shape.Radius > 0)
            return new Success();
        
        return Error.Validation(code: Errors.InvalidShape, description: "Circle radius must be greater than zero");
    }

    public ErrorOr<Success> Validate(IShape shape) => Validate((Circle)shape);
}