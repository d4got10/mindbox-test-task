using AreaCalculation.Shapes.Circle;
using ErrorOr;

namespace AreaCalculation.Tests.ShapeTests;

public class CircleTests
{
    private AreaCalculator _areaCalculator;
    
    [SetUp]
    public void Setup()
    {
        _areaCalculator = new AreaCalculator();
    }

    [TestCase(-5)]
    [TestCase(0)]
    public void CalculateArea_GivenCircleWithNonPositiveRadius_ReturnInvalidShapeError(double radius)
    {
        var circle = new Circle()
        {
            Radius = radius
        };
        
        ErrorOr<double> result = _areaCalculator.CalculateArea(circle);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True, "Calculation should return error");
            Assert.That(result.FirstError.Code, Is.EqualTo(Errors.InvalidShape), $"Error code should be \"{Errors.InvalidShape}\"");
        });
    }
    
    [TestCase(3.141_592_65,1)]
    [TestCase(12.566_370_61, 2)]
    [TestCase(1, 0.564_189_58)]
    public void CalculateArea_GivenValidTriangle_ReturnArea(double expectedResult, double radius)
    {
        var circle = new Circle()
        {
            Radius = radius
        };
        
        ErrorOr<double> result = _areaCalculator.CalculateArea(circle);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.False, "Calculation should NOT return error");
            Assert.That(result.Value, Is.EqualTo(expectedResult).Within(0.000_001), "Calculated value should equal to expected value");
        });
    }
}