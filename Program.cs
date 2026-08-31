using System.Globalization;

var orders = new List<PizzaOrder>();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Pizza Order Menu");
    Console.WriteLine("1. Create pizza order");
    Console.WriteLine("2. Print saved order");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            CreateOrder(orders);
            break;
        case "2":
            PrintOrder(orders);
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid option. Please choose 1, 2, or 0.");
            break;
    }
}

static void CreateOrder(List<PizzaOrder> orders)
{
    Console.Write("Enter pizza size: ");
    var size = ReadRequiredInput();

    Console.Write("Enter crust type: ");
    var crust = ReadRequiredInput();

    Console.Write("Enter toppings (comma-separated, optional): ");
    var toppingsInput = Console.ReadLine();
    var toppings = string.IsNullOrWhiteSpace(toppingsInput)
        ? []
        : toppingsInput
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

    var order = new PizzaOrder(orders.Count + 1, size, crust, toppings);
    orders.Add(order);

    Console.WriteLine($"Saved order #{order.Id}.");
}

static void PrintOrder(List<PizzaOrder> orders)
{
    if (orders.Count == 0)
    {
        Console.WriteLine("No orders have been saved yet.");
        return;
    }

    Console.Write($"Enter order number (1-{orders.Count}): ");
    var raw = Console.ReadLine();

    if (!int.TryParse(raw?.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out var orderNumber))
    {
        Console.WriteLine("Order number must be a whole number.");
        return;
    }

    var order = orders.FirstOrDefault(o => o.Id == orderNumber);

    if (order is null)
    {
        Console.WriteLine("Order not found.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"Order #{order.Id}");
    Console.WriteLine($"Size: {order.Size}");
    Console.WriteLine($"Crust: {order.Crust}");
    Console.WriteLine(order.Toppings.Count == 0
        ? "Toppings: none"
        : $"Toppings: {string.Join(", ", order.Toppings)}");
}

static string ReadRequiredInput()
{
    while (true)
    {
        var value = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        Console.Write("Value is required. Please enter again: ");
    }
}

internal sealed record PizzaOrder(int Id, string Size, string Crust, List<string> Toppings);
