using System;
using System.Collections.Generic;
using System.Text;

namespace GluwaAPI.TestEngine.Models
{
    public sealed class ExchangeAddressItem
    {
        public AddressItem Sender { get; set; }

        public AddressItem Receiver { get; set; }

        public ExchangeAddressItem() { }

    }
}
