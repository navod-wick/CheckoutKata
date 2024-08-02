using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckoutKata.Common.Models;

namespace CheckoutKata.Service.Interfaces;

public interface ICheckout
{
    void ScanItem(CheckoutItem  item);

    int GetTotal();
}

