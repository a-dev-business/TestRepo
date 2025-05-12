namespace UnitTestScenatios.Dependecies.Relationships;

public class Cart
{
    private readonly List<CartItem> _items = [];
    public bool AddItem(CartItem item)
    {
        _items.Add(item);

        return true;
    }

    public decimal CalculateTotal()
    {
        var total = this._items.Sum(i => i.Amount);
        return total;
    }
}

public class CartItem
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
}