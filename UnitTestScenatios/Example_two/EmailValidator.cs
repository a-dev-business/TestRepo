namespace UnitTestScenatios.Example_two;

public class EmailValidator
{
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}