// See https://aka.ms/new-console-template for more information


using CheckoutKata.Common.Models;
using CheckoutKata.Service.Implementations;

var itemsForSale = new List<CheckoutItem>
{
    new CheckoutItem('A', "apple", 5),
    new CheckoutItem('B', "orange", 3),
    new CheckoutItem('C', "pear", 4),
    new CheckoutItem('D', "mango", 8)
};

var service = new Checkout(new Dictionary<char, PricingRule>
{
    {'A', new PricingRule(2, 8)},
    {'B', new PricingRule(3, 7)},
    {'C', new PricingRule(2, 6)},
    {'D', new PricingRule(2, 14)}
});

Console.WriteLine("Scanning an apple");
service.ScanItem(itemsForSale.Single(i => i.Name == "apple"));

Console.WriteLine("Scanning an apple");
service.ScanItem(itemsForSale.Single(i => i.Name == "apple"));

Console.WriteLine("Scanning an mango");
service.ScanItem(itemsForSale.Single(i => i.Name == "mango"));

Console.WriteLine("Scanning an pear");
service.ScanItem(itemsForSale.Single(i => i.Name == "pear"));

Console.WriteLine("Scanning an pear");
service.ScanItem(itemsForSale.Single(i => i.Name == "pear"));

Console.WriteLine($"Total: {service.GetTotal()}");