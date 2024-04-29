namespace DB_Test_API.Models;

public class Transaction
{
    public int Id { get; set; }
    public float Amount { get; set; }
    public int AccountId { get; set; }
    public DateTime Timestamp { get; set; }
}
