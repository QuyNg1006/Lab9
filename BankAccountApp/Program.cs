using BankAccountApp;

var account = new BankAccount("Trung Pham", 1000m);
account.Debit(200m);
account.Credit(50m);
Console.WriteLine($"{account.CustomerName}: {account.Balance}");
