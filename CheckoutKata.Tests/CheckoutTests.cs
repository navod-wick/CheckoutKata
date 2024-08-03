using CheckoutKata.Common.Models;
using CheckoutKata.Service.Implementations;

namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        private readonly List<CheckoutItem> _itemsForSale;

        private const string APPLE = "apple";
        private const string ORANGE = "orange";
        private const string PEAR = "pear";
        private const string MANGO = "mango";

        public CheckoutTests()
        {
            _itemsForSale = new List<CheckoutItem>
            {
                new CheckoutItem('A', APPLE, 5),
                new CheckoutItem('B', ORANGE, 3),
                new CheckoutItem('C', PEAR, 4),
                new CheckoutItem('D', MANGO, 8)
            };
        }

        [Fact]
        public void When_No_PricingRules_Set_Total_Value_Will_Have_No_Impact()
        {
            var service = new Checkout(new Dictionary<char, PricingRule>());
            service.ScanItem(_itemsForSale.Single(i => i.Name == PEAR));
            service.ScanItem(_itemsForSale.Single(i => i.Name == PEAR));
            service.ScanItem(_itemsForSale.Single(i => i.Name == MANGO));

            var total = service.GetTotal();

            Assert.Equal(16, total);
        }

        [Fact]
        public void Calling_Total_Multiple_Times_Works_Successfully()
        {
            var service = new Checkout(new Dictionary<char, PricingRule>());
            service.ScanItem(_itemsForSale.Single(i => i.Name == PEAR));
            service.ScanItem(_itemsForSale.Single(i => i.Name == PEAR));
            service.ScanItem(_itemsForSale.Single(i => i.Name == MANGO));

            service.GetTotal();

            service.ScanItem(_itemsForSale.Single(i => i.Name == APPLE));

            var total = service.GetTotal();

            Assert.Equal(21, total);
        }

        [Fact]
        public void Using_Pricing_Rules_Will_Return_Correct_Results()
        {
            var service = new Checkout(new Dictionary<char, PricingRule>
            {
                {_itemsForSale.Single(i => i.Name == APPLE).Sku, new PricingRule(2, 8)},
                {_itemsForSale.Single(i => i.Name == ORANGE).Sku, new PricingRule(3, 7)},
                {_itemsForSale.Single(i => i.Name == PEAR).Sku, new PricingRule(2, 6)},
                {_itemsForSale.Single(i => i.Name == MANGO).Sku, new PricingRule(2, 14)}
            });

            service.ScanItem(_itemsForSale.Single(i => i.Name == APPLE));
            service.ScanItem(_itemsForSale.Single(i => i.Name == APPLE));
            service.ScanItem(_itemsForSale.Single(i => i.Name == ORANGE));
            service.ScanItem(_itemsForSale.Single(i => i.Name == MANGO));
            service.ScanItem(_itemsForSale.Single(i => i.Name == PEAR));
            service.ScanItem(_itemsForSale.Single(i => i.Name == PEAR));
            service.ScanItem(_itemsForSale.Single(i => i.Name == MANGO));
            service.ScanItem(_itemsForSale.Single(i => i.Name == APPLE));
            
            Assert.Equal(36, service.GetTotal());

            //scan further items to see if the special price is applied to the whole basket
            service.ScanItem(_itemsForSale.Single(i => i.Name == ORANGE));
            service.ScanItem(_itemsForSale.Single(i => i.Name == ORANGE));

            Assert.Equal(40, service.GetTotal());
        }
        
    }
}