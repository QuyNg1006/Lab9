using System.Globalization;
using BankAccountApp;
using CsvHelper;

namespace BankAccountTests;

// Data-driven tests: BankAccountTestData.csv
public class BankAccountCsvTests
{
    public class TestData
    {
        public string CustomerName { get; set; } = "";
        public decimal InitialBalance { get; set; }
        public decimal DebitAmount { get; set; }
        public string ExpectedBalance { get; set; } = "";
    }

    public static IEnumerable<object[]> GetTestData()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "BankAccountTestData.csv");
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var r in csv.GetRecords<TestData>().ToList())
            yield return new object[] { r.CustomerName, r.InitialBalance, r.DebitAmount, r.ExpectedBalance };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Debit_CsvData_UpdatesBalance(string customerName, decimal initialBalance, decimal debitAmount, string expectedBalance)
    {
        var account = new BankAccount(customerName, initialBalance);

        if (expectedBalance == "Insufficient funds")
        {
            var ex = Assert.Throws<InvalidOperationException>(() => account.Debit(debitAmount));
            Assert.Equal(expectedBalance, ex.Message);
        }
        else
        {
            account.Debit(debitAmount);
            Assert.Equal(decimal.Parse(expectedBalance, CultureInfo.InvariantCulture), account.Balance);
        }
    }
}
