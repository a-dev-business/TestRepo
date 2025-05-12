namespace UnitTestScenatios.Dependecies;

public abstract class PaymentMethod
{
    public abstract bool Proccess(decimal amount);
}

public class CreditCardPayment : PaymentMethod
{
    public override bool Proccess(decimal amount)
    {
        return true;
    }
}

public class PayPalPayment : PaymentMethod
{
    public override bool Proccess(decimal amount)
    {
        return true;
    }
}