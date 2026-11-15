using System.Globalization;
using BasicMath;
using CsvHelper;

namespace BasicMathTests;

// Exercise 2 - data-driven tests, test data read from CSV files (TestData folder)
public class BasicMathsCsvTests
{
    public class TestData
    {
        public double A { get; set; }
        public double B { get; set; }
        public double Expected { get; set; }
    }

    public static IEnumerable<object[]> GetTestData(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", fileName);
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var r in csv.GetRecords<TestData>().ToList())
            yield return new object[] { r.A, r.B, r.Expected };
    }

    [Theory]
    [MemberData(nameof(GetTestData), "add_testdata.csv")]
    public void Add_CsvData_ReturnsCorrectSum(double a, double b, double expected)
        => Assert.Equal(expected, new BasicMaths().Add(a, b));

    [Theory]
    [MemberData(nameof(GetTestData), "subtract_testdata.csv")]
    public void Subtract_CsvData_ReturnsCorrectDifference(double a, double b, double expected)
        => Assert.Equal(expected, new BasicMaths().Subtract(a, b));

    [Theory]
    [MemberData(nameof(GetTestData), "multiply_testdata.csv")]
    public void Multiply_CsvData_ReturnsCorrectProduct(double a, double b, double expected)
        => Assert.Equal(expected, new BasicMaths().Multiply(a, b));

    [Theory]
    [MemberData(nameof(GetTestData), "divide_testdata.csv")]
    public void Divide_CsvData_ReturnsCorrectQuotient(double a, double b, double expected)
        => Assert.Equal(expected, new BasicMaths().Divide(a, b), 8);
}
