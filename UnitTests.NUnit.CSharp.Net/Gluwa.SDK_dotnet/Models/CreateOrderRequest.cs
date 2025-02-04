using Gluwa.SDK_dotnet.Models.Exchange;

namespace Gluwa.SDK_dotnet.Tests.Models
{
    public class CreateOrderRequest
    {
        /// <summary>
        /// Currency conversion according to the order maker. Format: "SourceTarget"
        /// Example: UsdgKrwg means converting Usdg to Krwg
        /// </summary>
        public EConversion? Conversion { get; set; }

        /// <summary>
        /// The address that funds the source amount
        /// </summary>
        public string SendingAddress { get; set; }

        /// <summary>
        /// The address for the exchanged money
        /// </summary>
        public string ReceivingAddress { get; set; }

        /// <summary>
        /// How much money do you want to sell from source
        /// </summary>
        public string SourceAmount { get; set; }

        /// <summary>
        /// Gas Price for ethereum transaction
        /// </summary>
        public string Price { get; set; }
    }
}