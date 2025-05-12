namespace UnitTestScenatios.Dependecies;

public class ApiClient : IApiClient
{
    public string GetKey(string user) => "access_token";
}

public interface IApiClient
{
    string GetKey(string user);
}

public class OrderService
{
    
    private readonly IApiClient _apiClient;

    public OrderService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public bool SendData(string data)
    {
        try
        {
            var apiKey = _apiClient.GetKey("ali");
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
}