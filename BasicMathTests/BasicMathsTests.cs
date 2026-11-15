using BasicMath;

namespace BasicMathTests;

// Exercise 1 - inline data, EP (Equivalence Partitioning) + BVA (Boundary Value Analysis)
public class BasicMathsTests
{
    private readonly BasicMaths _bm = new();

    [Theory]
    [InlineData(1, 1, 2)]                          // EP: positive numbers
    [InlineData(-1, -1, -2)]                       // EP: negative numbers
    [InlineData(0, 0, 0)]                          // EP: zero
    [InlineData(2147483647.0, 1, 2147483648.0)]    // BVA: upper boundary (int.MaxValue + 1)
    [InlineData(-2147483648.0, -1, -2147483649.0)] // BVA: lower boundary (int.MinValue - 1)
    public void Add_ReturnsCorrectSum(double a, double b, double expected)
        => Assert.Equal(expected, _bm.Add(a, b));

    [Theory]
    [InlineData(5, 3, 2)]                          // EP: positive
    [InlineData(-5, -3, -2)]                       // EP: negative
    [InlineData(0, 0, 0)]                          // EP: zero
    [InlineData(-2147483648.0, 1, -2147483649.0)]  // BVA: lower boundary
    public void Subtract_ReturnsCorrectDifference(double a, double b, double expected)
        => Assert.Equal(expected, _bm.Subtract(a, b));

    [Theory]
    [InlineData(2, 3, 6)]                          // EP: positive
    [InlineData(-2, -3, 6)]                        // EP: negative * negative
    [InlineData(0, 5, 0)]                          // EP: zero
    [InlineData(2147483647.0, 1, 2147483647.0)]    // BVA: max value
    public void Multiply_ReturnsCorrectProduct(double a, double b, double expected)
        => Assert.Equal(expected, _bm.Multiply(a, b));

    [Theory]
    [InlineData(6, 3, 2)]                          // EP: positive
    [InlineData(-6, -3, 2)]                        // EP: negative
    [InlineData(0, 1, 0)]                          // EP: zero numerator
    [InlineData(2147483647.0, 1, 2147483647.0)]    // BVA: max value
    [InlineData(1, 3, 0.33333333)]                 // non-terminating result
    public void Divide_ReturnsCorrectQuotient(double a, double b, double expected)
        => Assert.Equal(expected, _bm.Divide(a, b), 8);

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        var ex = Assert.Throws<DivideByZeroException>(() => _bm.Divide(1, 0));
        Assert.Equal("Division by zero is not allowed.", ex.Message);
    }
}
