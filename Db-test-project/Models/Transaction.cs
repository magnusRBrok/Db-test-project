﻿namespace DB_Test_API.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public int AccountId { get; set; }
    public DateTime Timestamp { get; set; }
}
