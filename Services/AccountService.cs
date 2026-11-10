namespace Rev1.Services
{
    public class AccountService
    {   

        private readonly List<Account> _accounts = new();

public IReadOnlyList<Account> Accounts => _accounts.AsReadOnly();





    public void CreateAccount(Account account)
        {
            if(Accounts.Any(a=>a.AccountNumber==account.AccountNumber)){
                 throw new ArgumentException("Account Number Already Exists!");
          
            }
            _accounts.Add(account);
        }

        public Account? FindAccount(string accN)
        {
          
            return Accounts.FirstOrDefault(a=>a.AccountNumber ==accN);
        }


    public void DisplayAccount()
        {
            if(!_accounts.Any())return;
            foreach(var acc in Accounts)
            {
                System.Console.WriteLine($"Acc Number {acc.AccountNumber}");
                System.Console.WriteLine($"Owner Name {acc.OwnerName}");
                System.Console.WriteLine($"Balance {acc.Balance}");
                System.Console.WriteLine($"Created At {acc.CreatedAt}");
            }
        } 

    public void UpdateOnerName(string newName , string accN)
        {
            var acc = FindAccount(accN);
            if(acc is null) return;
            acc.UpdateOnerName(newName);

        }

        public void Delte (string accNumber)
        {
           var account = FindAccount(accNumber);
           if(account is null) System.Console.WriteLine("Invalid AccNumber ");
           if(account.Balance !=0)System.Console.WriteLine("Cannot Delete This Acoount With Remaining Balance!");
           _accounts.Remove(account);
           System.Console.WriteLine("Account Deleted Successfult ");
        }
    

    }
}