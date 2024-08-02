using CheckoutKata.Common.Models;
using CheckoutKata.Service.Interfaces;

namespace CheckoutKata.Service.Implementations;

public class Checkout : ICheckout
{
    private readonly Dictionary<char, PricingRule> _pricingRules;

    private readonly List<CheckoutItem> _items;

    public Checkout(Dictionary<char, PricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        _items = new List<CheckoutItem>();
    }

    public void ScanItem(CheckoutItem item)
    {
        _items.Add(item);
    }

    public int GetTotal()
    {
        var totalPrice = 0;

        var processedItems = new List<char>();

        foreach (var item in _items)
        {
            if (processedItems.Any(i => i == item.Sku))
            {
                continue;
            }
            else
            {
                var basketItems = _items.Where(i => i.Sku == item.Sku).ToList();
                var result = CalculateSpecialPrices(item.Sku, basketItems.Count);
                totalPrice += result.total;
                totalPrice += result.remaining * basketItems.First().UnitPrice;
                processedItems.Add(item.Sku);
            }
        }

        return totalPrice;
    }

    private (int total, int remaining) CalculateSpecialPrices(char sku, int numberOfitems)
    {
        if (_pricingRules.ContainsKey(sku))
        {
            var numberOfGroups = numberOfitems / _pricingRules[sku].Quantity;
            
            var totalPrice = numberOfGroups * _pricingRules[sku].Price;
            
            var remainder = numberOfitems % _pricingRules[sku].Quantity;

            return (totalPrice, remainder);
        }
        else
        {
            return (0, numberOfitems);
        }
    }
}

