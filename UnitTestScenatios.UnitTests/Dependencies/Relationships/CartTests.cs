using FluentAssertions;
using NSubstitute;
using UnitTestScenatios.Dependecies;
using UnitTestScenatios.Dependecies.Relationships;

namespace UnitTestScenatios.UnitTests.Dependencies.Relationships;

public class CartTests
{
    // [Fact]
    // public void AddItem_IntoCart_ShouldThrowOperationException_WhenUserWasNotLoggedIn()
    // {
    //     // arrange
    //     var userAuth = new UserAuthentication();
    //     var cart = new Cart(userAuth);
    //
    //     // act
    //     var addItemAction = () => cart.AddItem("new item!");
    //     
    //     // assertion
    //     addItemAction.Should().Throw<InvalidOperationException>().WithMessage("User must be authenticated!");
    // }

    // [Fact]
    // public void AddItem_IntoCart_ShouldBeSuccessful_WhenUserWasLoggedIn()
    // {
    //     // arrange
    //     var userAuth = new UserAuthentication();
    //     userAuth.Login("Ali", "123Ali");
    //     var cart = new Cart(userAuth);
    //     
    //     // act
    //     var addResult = cart.AddItem("new item!");
    //     
    //     // assertion
    //     addResult.Should().BeTrue();
    // }

    // [Fact]
    // public void SearchItemAdd_IntoCart_ShouldBeIndependent()
    // {
    //     // arrange
    //     var search = new Search();
    //     var item = search.SearchItem("iphone");
    //     
    //     var userAuth = new UserAuthentication();
    //     userAuth.Login("Ali", "123Ali");
    //     var cart = new Cart(userAuth);
    //     
    //     
    //     // act
    //     var addResult = cart.AddItem(item);
    //     
    //     // assertion
    //     addResult.Should().BeTrue();
    // }

    [Fact]
    public void CalculateTotal_ShouldReturnTotal()
    {
        // a
        var cart = new Cart();
        cart.AddItem(new CartItem() { Name = "first", Amount = 1.5m });
        cart.AddItem(new CartItem() { Name = "second", Amount = 3 });
        cart.AddItem(new CartItem() { Name = "third", Amount = 0.5m });

        // a
        var total = cart.CalculateTotal();

        // a 
        total.Should().Be(5);
    }

    [Fact]
    public void PaymentProcess_ShouldProcessCorrectly_WithDifferentMethods()
    {
        PaymentMethod paypal = new PayPalPayment();
        PaymentMethod creditCard = new CreditCardPayment();

        paypal.Proccess(100m).Should().BeTrue();
        creditCard.Proccess(100m).Should().BeTrue();
    }

    [Fact]
    public void Receipt_ShouldBeGeneratedCorrectly()
    {
        var goods = new List<Item>();
        goods.Add(new Item() { Name = "first", Amount = 1.5m });
        goods.Add(new Item() { Name = "second", Amount = 3 });
        goods.Add(new Item() { Name = "third", Amount = 0.5m });

        PaymentMethod paypal = new PayPalPayment();

        var receipt = new Receipt(goods, paypal);

        var generatedReceipt = receipt.GenerateReceipt();

        generatedReceipt.Length.Should().BeGreaterThan(3);
    }

    [Fact]
    public void Receipt_ShouldGenerateInCorrect_SendData()
    {
        var apiClient = Substitute.For<IApiClient>();
        apiClient.GetKey(Arg.Any<string>()).Returns((info) => throw new Exception());

        var receipt = new OrderService(apiClient);
        receipt.SendData("Test").Should().BeFalse();
    }
}