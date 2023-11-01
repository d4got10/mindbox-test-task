namespace AreaCalculation.Core;

public struct ShapeSupport
{
    public IShapeValidator Validator { get; set; }
    public IAreaCalculationStrategy CalculationStrategy { get; set; }
}