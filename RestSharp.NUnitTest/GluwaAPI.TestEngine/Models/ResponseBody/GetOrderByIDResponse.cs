using System.Collections.Generic;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
    public sealed class GetOrderByIDResponse
    {
        public string ID { get; private set; }

        public string Conversion { get; private set; }

        public string SendingAddress { get; private set; }

        public string SourceAmount { get; private set; }

        public string Price { get; private set; }

        public string ReceivingAddress { get; private set; }

        public string Status { get; private set; }

        public List<OrderExchangeItem> Exchanges { get; private set; }

        public GetOrderByIDResponse(
            string id,
            string conversion,
            string sendingAddress,
            string sourceAmount,
            string price,
            string receivingAddress,
            string status,
            List<OrderExchangeItem> exchanges)
        {
            ID = id;
            Conversion = conversion;
            SendingAddress = sendingAddress;
            SourceAmount = sourceAmount;
            Price = price;
            ReceivingAddress = receivingAddress;
            Status = status;
            Exchanges = exchanges;
        }
    }


    public sealed class OrderExchangeItem
    {
        public string SendingAddress { get; private set; }

        public string ReceivingAddress { get; private set; }

        public string SourceAmount { get; private set; }

        public string Fee { get; private set; }

        public string ExchangedAmount { get; private set; }

        public string Price { get; private set; }

        public string Status { get; private set; }

        public OrderExchangeItem(
            string sendingAddress,
            string receivingAddress,
            string sourceAmount,
            string fee,
            string exchangedAmount,
            string price,
            string status)
        {
            SendingAddress = sendingAddress;
            ReceivingAddress = receivingAddress;
            SourceAmount = sourceAmount;
            Fee = fee;
            ExchangedAmount = exchangedAmount;
            Price = price;
            Status = status;
        }
    }
}
