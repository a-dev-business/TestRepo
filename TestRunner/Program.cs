


using UnitTestScenatios.Example_five;

var callGroup = new CallGroup<string>(3, async (ops) =>
{
    Console.WriteLine("همه آماده‌ان! عملیات مشترک شروع شد:");
    
    foreach (var op in ops)
        Console.WriteLine($"- {op}");
    
    
}, TimeSpan.FromSeconds(10));

await Task.WhenAll(
    callGroup.Join("UserA"),
    callGroup.Join("UserB"),
    callGroup.Join("UserC")
);