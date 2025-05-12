namespace UnitTestScenatios.Example_three;

public class DiscountCalculator
{
    public decimal CalculateDiscount(decimal totalAmount, MembershipLevel membershipLevel)
    {
        decimal discount = 0;

        switch (membershipLevel)
        {
            case MembershipLevel.Silver:
                discount = 0.05m;
                break;
            case MembershipLevel.Gold:
                discount = 0.1m;
                break;
            case MembershipLevel.Platinum:
                discount = 0.2m;
                break;
            case MembershipLevel.None:
            default:
                discount = 0;
                break;
        }

        return totalAmount - (totalAmount * discount);
    }
}