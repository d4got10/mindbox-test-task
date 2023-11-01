using AreaCalculation;
using AreaCalculation.Core;
using AreaCalculation.Shapes.Circle;
using AreaCalculation.Shapes.Triangle;
using ErrorOr;

var areaCalculator = new AreaCalculator();

Console.WriteLine("Enter triangle side lengths:");
IShape shape = new Triangle
{
    SideLengths =
    {
        [0] = double.Parse(Console.ReadLine()!),
        [1] = double.Parse(Console.ReadLine()!),
        [2] = double.Parse(Console.ReadLine()!),
    }
};
ErrorOr<double> errorOrTriangleArea = areaCalculator.CalculateArea(shape);
if (errorOrTriangleArea.IsError)
{
    Error error = errorOrTriangleArea.FirstError;
    Console.WriteLine("Error has occured! Error code: {0}. Description: {1}", 
        error.Code, error.Description);
}
else
{
    Console.WriteLine("Area of a triangle: {0}", errorOrTriangleArea.Value);
}

Console.WriteLine("Enter circle radius:");
shape = new Circle
{
    Radius = double.Parse(Console.ReadLine()!)
};
ErrorOr<double> errorOrCircleArea = areaCalculator.CalculateArea(shape);
if (errorOrCircleArea.IsError)
{
    Error error = errorOrCircleArea.FirstError;
    Console.WriteLine("Error has occured! Error code: {0}. Description: {1}", 
        error.Code, error.Description);
}
else
{
    Console.WriteLine("Area of a circle: {0}", errorOrCircleArea.Value);
}