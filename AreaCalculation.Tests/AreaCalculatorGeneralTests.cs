using AreaCalculation.Core;
using ErrorOr;

namespace AreaCalculation.Tests;

public class AreaCalculatorGeneralTests
{
    private AreaCalculator _areaCalculator;
    
    [SetUp]
    public void Setup()
    {
        _areaCalculator = new AreaCalculator();
    }

    [Test]
    public void CalculateArea_GivenUnsupportedType_ReturnUnsupportedTypeError()
    {
        IShape myShape = new MyShape();
        ErrorOr<double> result = _areaCalculator.CalculateArea(myShape);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True, "MyShape should not be supported");
            Assert.That(result.FirstError.Code, Is.EqualTo(Errors.UnsupportedType), $"Error code should be \"{Errors.UnsupportedType}\"");
        });
    }
    
    [Test]
    public void CalculateArea_GivenNull_ReturnUnsupportedTypeError()
    {
        IShape myShape = null;
        ErrorOr<double> result = _areaCalculator.CalculateArea(myShape);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.True, "null should not be supported");
            Assert.That(result.FirstError.Code, Is.EqualTo(Errors.NullArgument), $"Error code should be \"{Errors.NullArgument}\"");
        });
    }


    private class MyShape : IShape
    {
    }
}