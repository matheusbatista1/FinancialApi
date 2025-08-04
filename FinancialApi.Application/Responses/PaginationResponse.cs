namespace FinancialApi.Application.Responses;

public class PaginationResponse
{
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}