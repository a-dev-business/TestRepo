namespace UnitTestScenatios.Dependecies;

public class Receipt
{
    private List<Item> goods;
    private PaymentMethod paymentMethod;

    public Receipt(List<Item> goods, PaymentMethod paymentMethod)
    {
        this.goods = goods;
        this.paymentMethod = paymentMethod;
    }

    public string GenerateReceipt()
    {
        var total = goods.Sum(g => g.Amount);
        return $"Total: {total} \n Paid with payment method '{paymentMethod.GetType().Name}'";
    }
}

public class Item
{
    public string Name  { get; set; }
    public decimal Amount { get; set; }
}