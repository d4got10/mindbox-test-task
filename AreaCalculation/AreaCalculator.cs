using AreaCalculation.Core;
using AreaCalculation.Shapes.Circle;
using AreaCalculation.Shapes.Triangle;
using ErrorOr;

namespace AreaCalculation;


//Предполагается что фигуры не вырожденные (их площадь не равна 0)
public class AreaCalculator : IAreaCalculator
{
    protected readonly Dictionary<Type, ShapeSupport> ShapeSupports = new()
    {
        { typeof(Triangle), new ShapeSupport { Validator = new TriangleValidator(), CalculationStrategy = new TriangleAreaCalculationStrategy() } },
        { typeof(Circle), new ShapeSupport { Validator = new CircleValidator(), CalculationStrategy = new CircleAreaCalculationStrategy() } },
    };

    public ErrorOr<double> CalculateArea<T>(T shape)
    {
        if (!IsSupported<T>())
            return Error.Conflict(code: Errors.UnsupportedType, description: $"Type {typeof(T)} is not supported by area calculator");
        
        IShapeValidator<T> validator = GetValidator<T>();
        ErrorOr<Success> validationResult = validator.Validate(shape);
        if (validationResult.IsError)
            return validationResult.Errors;

        IAreaCalculationStrategy<T> calculationStrategy = GetCalculationStrategy<T>();
        return calculationStrategy.Calculate(shape);
    }

    private bool IsSupported<T>()
    {
        return ShapeSupports.ContainsKey(typeof(T));
    }
    
    private IShapeValidator<T> GetValidator<T>()
    {
        return (IShapeValidator<T>)ShapeSupports[typeof(T)].Validator;
    }

    private IAreaCalculationStrategy<T> GetCalculationStrategy<T>()
    {
        return (IAreaCalculationStrategy<T>)ShapeSupports[typeof(T)].CalculationStrategy;
    }
}