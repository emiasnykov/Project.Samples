using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace GluwaAPI.TestEngine.Utils
{
    [Function("mint")]
    public class MintFunction : FunctionMessage
    {
        [Parameter("uint256", "amount", 1)]
        public BigInteger TokenAmount { get; set; }
    }
}