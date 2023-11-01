using AreaCalculation.Core;
using ErrorOr;

namespace AreaCalculation.Shapes.Triangle;

public class TriangleValidator : IShapeValidator<Triangle>
{
    public ErrorOr<Success> Validate(Triangle shape)
    {
        double[] lengths = shape.SideLengths;
        for (int i = 0; i < 3; i++)
        {
            if (lengths[i] <= 0)
            {
                return Error.Validation(code: Errors.InvalidShape, description: "Triangle side lengths must be positive");
            }
        }

        bool triangleInequality = lengths[0] + lengths[1] > lengths[2]
                                  && lengths[0] + lengths[2] > lengths[1]
                                  && lengths[1] + lengths[2] > lengths[0];
        if (triangleInequality)
            return new Success();
        
        return Error.Validation(code: Errors.InvalidShape, description: "Triangle inequality must be satisfied");
    }

    public ErrorOr<Success> Validate(IShape shape) => Validate((Triangle)shape);
}