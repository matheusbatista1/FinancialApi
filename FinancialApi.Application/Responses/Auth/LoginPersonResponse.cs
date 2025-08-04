namespace FinancialApi.Application.Responses.Auth;

public class LoginPersonResponse
{
    public string Token { get; set; }
    public LoginPersonResponse(string token)
    {
        Token = token;
    }
}