using System.Security.Principal;
using Rev1.Enums;

public class Account
{
    public int Id  { get; private set; } 

    public string AccountNumber { get; init; } = default!;

    public string OwnerName { get; private set; } = default!;

    public Gender Gender { get; private set; }

    public decimal Balance { get; private set; }

    public DateTime CreatedAt {get;init;} =DateTime.Now;

    public ICollection<Transaction> SentTransactions { get; }
        = new List<Transaction>();

    public ICollection<Transaction> ReceivedTransactions { get; }
        = new List<Transaction>();

 public Account(string accountNumber, string ownerName,
                   decimal initialBalance, Gender gender)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Invalid Account Number");

        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("Invalid Owner Name");

        if (initialBalance < 0)
            throw new ArgumentException("Invalid Initial Balance");

        AccountNumber = accountNumber;
        OwnerName = ownerName;
        Balance = initialBalance;
        Gender = gender;
        CreatedAt = DateTime.Now;
    }

        public void Deposit (decimal amount)
    {
        if(amount<=0)throw  new ArgumentException("Invalid Amount");
        Balance += amount;
    }
    public void Withdraw(decimal amount)
    {
        if(amount>Balance||amount<=0) throw new ArgumentException("Invalid withdrawal");
        Balance-=amount;
    }
    public void UpdateOnerName(string newName)
    {
        if(string.IsNullOrWhiteSpace(newName))  throw new ArgumentException("Owner name cannot be empty");
        OwnerName=newName;

    }
}