using Rev1.Enums;

namespace Rev1.Services
{
    public class TransactionServices
    {
        private readonly AccountService _accountService;
        private int _transactionIdCounter = 1;

        private readonly List<Transaction> _transactions = new();

        public IReadOnlyList<Transaction> Transactions
            => _transactions.AsReadOnly();

        public TransactionServices(AccountService accountService)
        {
            _accountService = accountService;
        }

        private Account CheckTransaction(string accN)
        {
            var account = _accountService.FindAccount(accN);

            if (account is null)
                throw new ArgumentException("Invalid Account");

            return account;
        }

     public void CreateTransaction (TransactionType transactionType, decimal amount, string? description, Account? sourceAccount, Account? destinationAccount)
        {
            var transaction = new Transaction
            {
                Id = _transactionIdCounter++,
                TransactionType = transactionType,
                Amount = amount,
                Description = description,
                SourceAccount = sourceAccount,
                DestinationAccount = destinationAccount,
               
            };

            _transactions.Add(transaction);
        }
        

     public void Deposit(string accN, decimal amount)
{
    var account = CheckTransaction(accN);

    account.Deposit(amount);

    CreateTransaction(
        TransactionType.Deposit,
        amount,
        "Cash Deposit",
        null,
        account
    );
}

   public void Withdraw(string accN, decimal amount)
{
    var account = CheckTransaction(accN);

    account.Withdraw(amount);

    CreateTransaction(
        TransactionType.Withdraw,
        amount,
        "Cash Withdrawal",
        account,
        null
    );
}

       public void Transfer(string acc1, string acc2, decimal amount)
{
    var account1 = CheckTransaction(acc1);
    var account2 = CheckTransaction(acc2);

    if (ReferenceEquals(account1, account2))
        throw new ArgumentException("Cannot Transfer To Same Account");

    account1.Withdraw(amount);
    account2.Deposit(amount);

    CreateTransaction(
        TransactionType.Transfer,
        amount,
        $"Transfer from {account1.OwnerName} to {account2.OwnerName}",
        account1,
        account2
    );
}

public void DisplayAccountTransactions(string accN)
{
    var account = CheckTransaction(accN);

    var transactions = Transactions.Where(t =>
        t.SourceAccount == account ||
        t.DestinationAccount == account
    ).OrderByDescending(t => t.TransactionDate);

    if (!transactions.Any())
    {
        Console.WriteLine("No Transactions Found!");
        return;
    }

    foreach (var transaction in transactions)
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Transaction ID: {transaction.Id}");
        Console.WriteLine($"Type: {transaction.TransactionType}");
        Console.WriteLine($"Amount: {transaction.Amount}");
        Console.WriteLine($"Date: {transaction.TransactionDate}");
        Console.WriteLine($"Description: {transaction.Description}");
    }
}
    }
}