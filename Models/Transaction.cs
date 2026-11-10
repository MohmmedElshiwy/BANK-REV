using Rev1.Enums;

public class Transaction
{
    public int Id { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }
        = DateTime.Now;

    public string? Description { get; set; }


    public Account? SourceAccount { get; set; }


    public Account? DestinationAccount { get; set; }

   
}