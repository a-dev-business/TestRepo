namespace UnitTestScenatios.Dependecies.Relationships;

public class UserAuthentication
{
    public bool IsAuthenticated { get; private set; } = false;
    public void Login(string userName, string password) => this.IsAuthenticated = true;
}