namespace FinancialApi.Application.Responses.Balance;

public class GetBalanceResponse
{
    public decimal Balance { get; set; }

    public GetBalanceResponse(decimal balance)
    {
        Balance = balance;
    }
}