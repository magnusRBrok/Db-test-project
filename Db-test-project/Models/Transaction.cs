namespace DB_Test_API.Models;

public class Transaction
{
    public int Id { get; set; }
    public float Amount { get; set; }
    public int RecipientAccountId { get; set; }
    public int SenderAccountId {  get; set; }
    public DateTime Timestamp { get; set; }
}
