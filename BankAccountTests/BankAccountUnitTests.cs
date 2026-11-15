using BankAccountApp;

namespace BankAccountTests;

// Inline-data tests: EP (valid / insufficient / invalid amount) + BVA (boundaries around balance and zero)
public class BankAccountUnitTests
{
    // ---- Debit ----
    [Theory]
    [InlineData("John Doe", 1000, 200, 800)]       // EP: valid amount
    [InlineData("Jane Smith", 500, 100, 400)]
    [InlineData("Alice Johnson", 300, 50, 250)]
    [InlineData("Boundary", 100, 100, 0)]          // BVA: debit == balance
    [InlineData("Boundary", 100, 0.01, 99.99)]     // BVA: smallest valid amount
    public void Debit_ValidAmount_UpdatesBalance(string name, decimal initial, decimal debit, decimal expected)
    {
        var account = new BankAccount(name, initial);
        account.Debit(debit);
        Assert.Equal(expected, account.Balance);
    }

    [Theory]
    [InlineData("Bob Brown", 100, 150)]            // EP: insufficient funds
    [InlineData("Boundary", 100, 100.01)]          // BVA: balance + smallest unit
    public void Debit_InsufficientFunds_ThrowsException(string name, decimal initial, decimal debit)
    {
        var account = new BankAccount(name, initial);
        var ex = Assert.Throws<InvalidOperationException>(() => account.Debit(debit));
        Assert.Equal("Insufficient funds", ex.Message);
        Assert.Equal(initial, account.Balance);    // balance must be unchanged
    }

    [Theory]
    [InlineData(0)]                                // BVA: zero
    [InlineData(-0.01)]                            // BVA: just below zero
    [InlineData(-50)]                              // EP: negative
    public void Debit_InvalidAmount_ThrowsArgumentOutOfRange(decimal debit)
    {
        var account = new BankAccount("A", 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(debit));
    }

    // ---- Credit ----
    [Theory]
    [InlineData("John Doe", 1000, 200, 1200)]
    [InlineData("Jane Smith", 500, 100, 600)]
    [InlineData("Alice Johnson", 300, 50, 350)]
    [InlineData("Boundary", 0, 0.01, 0.01)]        // BVA: smallest valid amount on empty account
    public void Credit_ValidAmount_UpdatesBalance(string name, decimal initial, decimal credit, decimal expected)
    {
        var account = new BankAccount(name, initial);
        account.Credit(credit);
        Assert.Equal(expected, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Credit_InvalidAmount_ThrowsArgumentOutOfRange(decimal credit)
    {
        var account = new BankAccount("A", 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(credit));
    }

    // ---- Withdraw ----
    [Theory]
    [InlineData("John Doe", 1000, 200, 800)]
    [InlineData("Jane Smith", 500, 100, 400)]
    [InlineData("Boundary", 100, 100, 0)]
    public void Withdraw_ValidAmount_UpdatesBalance(string name, decimal initial, decimal amount, decimal expected)
    {
        var account = new BankAccount(name, initial);
        account.Withdraw(amount);
        Assert.Equal(expected, account.Balance);
    }

    [Fact]
    public void Withdraw_InsufficientFunds_ThrowsException()
    {
        var account = new BankAccount("A", 100);
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(150));
    }
}
