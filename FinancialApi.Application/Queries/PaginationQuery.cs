namespace FinancialApi.Application.Queries;

public class PaginationQuery
{
    public int ItemsPerPage { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;
}