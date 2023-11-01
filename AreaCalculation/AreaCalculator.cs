using AreaCalculation.Core;
using AreaCalculation.Shapes.Circle;
using AreaCalculation.Shapes.Triangle;
using ErrorOr;

namespace AreaCalculation;

public class AreaCalculator : IAreaCalculator
{
    protected readonly Dictionary<Type, ShapeSupport> ShapeSupports = new()
    {
        { typeof(Triangle), new ShapeSupport { Validator = new TriangleValidator(), CalculationStrategy = new TriangleAreaCalculationStrategy() } },
        { typeof(Circle), new ShapeSupport { Validator = new CircleValidator(), CalculationStrategy = new CircleAreaCalculationStrategy() } },
    };

    public ErrorOr<double> CalculateArea<T>(T shape) where T : IShape
    {
        if (!IsSupported<T>())
            return CreateUnsupportedTypeError(typeof(T));
        
        IShapeValidator<T> validator = GetValidator<T>();
        ErrorOr<Success> validationResult = validator.Validate(shape);
        if (validationResult.IsError)
            return validationResult.Errors;

        IAreaCalculationStrategy<T> calculationStrategy = GetCalculationStrategy<T>();
        return calculationStrategy.Calculate(shape);
    }

    public ErrorOr<double> CalculateArea(IShape shape)
    {
        Type type = shape.GetType();
        if (!IsSupported(type))
            return CreateUnsupportedTypeError(type);

        IShapeValidator validator = GetValidator(type);
        ErrorOr<Success> validationResult = validator.Validate(shape);
        if (validationResult.IsError)
            return validationResult.Errors;

        IAreaCalculationStrategy calculationStrategy = GetCalculationStrategy(type);
        return calculationStrategy.Calculate(shape);
    }

    private bool IsSupported(Type type) => ShapeSupports.ContainsKey(type);

    private bool IsSupported<T>() => IsSupported(typeof(T));

    private IShapeValidator GetValidator(Type type) => ShapeSupports[type].Validator;

    private IShapeValidator<T> GetValidator<T>() where T : IShape => (IShapeValidator<T>)GetValidator(typeof(T));

    private IAreaCalculationStrategy GetCalculationStrategy(Type type) => ShapeSupports[type].CalculationStrategy;

    private IAreaCalculationStrategy<T> GetCalculationStrategy<T>() where T : IShape => (IAreaCalculationStrategy<T>)GetCalculationStrategy(typeof(T));

    private static Error CreateUnsupportedTypeError(Type type) => 
        Error.Conflict(code: Errors.UnsupportedType, description: $"Type {type} is not supported by area calculator");
}