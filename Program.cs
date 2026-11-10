
using Rev1.Enums;
using Rev1.Services;

AccountService accountService = new();
TransactionServices transactionServices = new(accountService);

bool isRunning = true;

while (isRunning)
{
    

    Console.WriteLine("================================");
    Console.WriteLine("       BANK MANAGEMENT SYSTEM");
    Console.WriteLine("================================");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. Deposit");
    Console.WriteLine("3. Withdraw");
    Console.WriteLine("4. Transfer");
    Console.WriteLine("5. View Account");
    Console.WriteLine("6. Account Transactions History");
    Console.WriteLine("0. Exit");
    Console.WriteLine("================================");

    Console.Write("Choose Operation: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                CreateAccount();
                break;

            case "2":
                Deposit();
                break;

            case "3":
                Withdraw();
                break;

            case "4":
                Transfer();
                break;

            case "5":
                ViewAccount();
                break;

            case "6":
                AccountHistory();
                break;

            case "0":
                isRunning = false;
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid Choice!");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected Error: {ex.Message}");
    }

}


// ===============================
// CREATE ACCOUNT
// ===============================

void CreateAccount()
{
    Console.WriteLine("\n--- Create Account ---");

    Console.Write("Account Number: ");
    string accN = Console.ReadLine()!;

    Console.Write("Owner Name: ");
    string ownerName = Console.ReadLine()!;

    decimal balance = ReadAmount("Initial Balance: ");

    Gender gender;

    while (true)
    {
        Console.WriteLine("1. Male");
        Console.WriteLine("2. Female");
        Console.Write("Choose Gender: ");

        if (int.TryParse(Console.ReadLine(), out int choice)
            && Enum.IsDefined(typeof(Gender), choice))
        {
            gender = (Gender)choice;
            break;
        }

        Console.WriteLine("Invalid Gender!");
    }

    Account account = new(accN, ownerName, balance, gender);

    accountService.CreateAccount(account);

    Console.WriteLine("Account Created Successfully!");
}


// ===============================
// DEPOSIT
// ===============================

void Deposit()
{
    Console.WriteLine("\n--- Deposit ---");

    Console.Write("Account Number: ");
    string accN = Console.ReadLine()!;

    decimal amount = ReadAmount("Deposit Amount: ");

    transactionServices.Deposit(accN, amount);

    Console.WriteLine("Deposit Completed Successfully!");
}


// ===============================
// WITHDRAW
// ===============================

void Withdraw()
{
    Console.WriteLine("\n--- Withdraw ---");

    Console.Write("Account Number: ");
    string accN = Console.ReadLine()!;

    decimal amount = ReadAmount("Withdraw Amount: ");

    transactionServices.Withdraw(accN, amount);

    Console.WriteLine("Withdrawal Completed Successfully!");
}


// ===============================
// TRANSFER
// ===============================

void Transfer()
{
    Console.WriteLine("\n--- Transfer ---");

    Console.Write("Source Account: ");
    string source = Console.ReadLine()!;

    Console.Write("Destination Account: ");
    string destination = Console.ReadLine()!;

    decimal amount = ReadAmount("Transfer Amount: ");

    transactionServices.Transfer(source, destination, amount);

    Console.WriteLine("Transfer Completed Successfully!");
}


// ===============================
// VIEW ACCOUNT
// ===============================

void ViewAccount()
{
    Console.WriteLine("\n--- View Account ---");

    Console.Write("Account Number: ");
    string accN = Console.ReadLine()!;

    var account = accountService.FindAccount(accN);

    if (account is null)
    {
        Console.WriteLine("Account Not Found!");
        return;
    }

    Console.WriteLine($"Account Number: {account.AccountNumber}");
    Console.WriteLine($"Owner Name: {account.OwnerName}");
    Console.WriteLine($"Gender: {account.Gender}");
    Console.WriteLine($"Balance: {account.Balance}");
    Console.WriteLine($"Created At: {account.CreatedAt}");
}


// ===============================
// ACCOUNT HISTORY
// ===============================

void AccountHistory()
{
    Console.WriteLine("\n--- Account Transactions History ---");

    Console.Write("Account Number: ");
    string accN = Console.ReadLine()!;

    transactionServices.DisplayAccountTransactions(accN);
}


// ===============================
// READ DECIMAL AMOUNT
// ===============================

decimal ReadAmount(string message)
{
    decimal amount;

    while (true)
    {
        Console.Write(message);

        if (decimal.TryParse(Console.ReadLine(), out amount)
            && amount >= 0)
        {
            return amount;
        }

        Console.WriteLine("Invalid Amount! Try Again.");
    }
}
