using FluentAssertions;
using UnitTestScenatios.Example_three;

namespace UnitTestScenatios.UnitTests.Example_three;

public class DiscountCalculatorTests
{
    private readonly DiscountCalculator discountCalculator;
    
    public DiscountCalculatorTests()
    {
        discountCalculator = new DiscountCalculator();
    }
    
    [Theory]
    [InlineData(20000, MembershipLevel.Silver, 19000)]
    [InlineData(20000, MembershipLevel.Gold, 18000)]
    [InlineData(20000, MembershipLevel.Platinum, 16000)]
    [InlineData(20000, MembershipLevel.None, 20000)]
    [InlineData(20000, null, 20000)]
    public void CalculateDiscount_ShouldReturnValidDiscountedAmount(decimal totalAmount, MembershipLevel level, decimal expectedDiscountedAmount)
    {
        // act
        var discountedAmount = discountCalculator.CalculateDiscount(totalAmount, level);
        
        // assertion
        discountedAmount.Should().Be(expectedDiscountedAmount);
    }
}