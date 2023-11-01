using AreaCalculation.Shapes.Triangle;
using ErrorOr;

namespace AreaCalculation.Tests.ShapeTests;

public class TriangleTests
{
    private AreaCalculator _areaCalculator;
    
    [SetUp]
    public void Setup()
    {
        _areaCalculator = new AreaCalculator();
    }

    [Test]
    public void CalculateArea_GivenTriangleWithUnsatisfiedTriangleInequality_ReturnInvalidShapeError()
    {
        var triangle = new Triangle()
        {
            SideLengths =
            {
                [0] = 1,
                [1] = 2,
                [2] = 4,
            }
        };
        
        ErrorOr<double> result = _areaCalculator.CalculateArea(triangle);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True, "Calculation should return error");
            Assert.That(result.FirstError.Code, Is.EqualTo(Errors.InvalidShape), $"Error code should be \"{Errors.InvalidShape}\"");
        });
    }
    
    [Test]
    public void CalculateArea_GivenTriangleWithNegativeSideLength_ReturnInvalidShapeError()
    {
        var triangle = new Triangle()
        {
            SideLengths =
            {
                [0] = -1,
                [1] = 2,
                [2] = 3,
            }
        };
        
        ErrorOr<double> result = _areaCalculator.CalculateArea(triangle);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True, "Calculation should return error");
            Assert.That(result.FirstError.Code, Is.EqualTo(Errors.InvalidShape), $"Error code should be \"{Errors.InvalidShape}\"");
        });
    }
    
    [TestCase(9.921_567_41, 4, 5, 6)]
    [TestCase(0.994_032_19, 1.41, 1.41, 2)]
    [TestCase(6, 3, 4, 5)]
    [TestCase(54, 9, 12, 15)]
    public void CalculateArea_GivenValidTriangle_ReturnArea(double expectedResult, params double[] sideLengths)
    {
        var triangle = new Triangle()
        {
            SideLengths =
            {
                [0] = sideLengths[0],
                [1] = sideLengths[1],
                [2] = sideLengths[2],
            }
        };
        
        ErrorOr<double> result = _areaCalculator.CalculateArea(triangle);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.False, "Calculation should NOT return error");
            Assert.That(result.Value, Is.EqualTo(expectedResult).Within(0.000_001), "Calculated value should equal to expected value");
        });
    }
}