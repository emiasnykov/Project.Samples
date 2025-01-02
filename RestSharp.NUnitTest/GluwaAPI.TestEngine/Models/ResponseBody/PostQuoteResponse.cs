using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
    /// <summary>
    /// The response generated from create quote
    /// </summary>
    public sealed class PostQuoteResponse
    {
        [Required]
        public string Conversion { get; set; }
        /// <summary>
        /// Used to accept the quote
        /// </summary>

        [Required]
        public string Checksum { get; set; }

        /// <summary>
        /// total source amount that will be exchanged
        /// </summary>
        [Required]
        public string TotalSourceAmount { get; private set; }

        /// <summary>
        /// exchange fee
        /// </summary>
        [Required]
        public string TotalFee { get; private set; }

        /// <summary>
        /// total estimated exchange amount in exchanged currency
        /// </summary>
        [Required]
        public string TotalEstimatedExchangedAmount { get; private set; }

        /// <summary>
        /// Average of all the prices listed
        /// </summary>
        [Required]
        public string AveragePrice { get; private set; }

        [Required]
        public string BestPrice { get; private set; }


        public string WorstPrice { get; private set; }

        /// <summary>
        /// All the orders that can fulfill this quote
        /// </summary>

        public List<MatchedOrder> MatchedOrders { get; private set; }

        /// <summary>
        /// When the quote was created
        /// </summary>

        public string CreatedDateTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>

        public string TimeToLive { get; set; }

        [JsonConstructor]
        public PostQuoteResponse(
            string conversion,
            string totalSourceAmount,
            string totalFee,
            string totalEstimatedExchangedAmount,
            string averagePrice,
            string bestPrice,
            string worstPrice,
            List<MatchedOrder> matchedOrders,
            string createdDateTime,
            string timeToLive,
            string checksum)
        {
            Conversion = conversion;
            TotalSourceAmount = totalSourceAmount;
            TotalFee = totalFee;
            TotalEstimatedExchangedAmount = totalEstimatedExchangedAmount;
            AveragePrice = averagePrice;
            BestPrice = bestPrice;
            WorstPrice = worstPrice;
            MatchedOrders = matchedOrders;
            CreatedDateTime = createdDateTime;
            TimeToLive = timeToLive;
            Checksum = checksum;
        }

        public PostQuoteResponse()
        {
        }
    }
}
