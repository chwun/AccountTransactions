using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models;

public class Transaction
{
    public Guid Id { get; set; }

    public string SourceOrDestination { get; set; } = "";

    public string Reference { get; set; } = "";

    public decimal Amount { get; set; }

    public TransactionDto ToDto()
    {
        return new()
        {
            Id = Id,
            SourceOrDestination = SourceOrDestination,
            Reference = Reference,
            Amount = Amount
        };
    }
}
