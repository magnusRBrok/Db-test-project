namespace DB_Test_API.Models;

public class Account
{
    public int Id { get; set; }
    public  double Balance { get; set; }
    public int CustomerId { get; set; }
    public List<Transaction>? Transactions { get; set; }
}
