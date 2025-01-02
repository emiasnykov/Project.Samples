using System;

namespace GluwaAPI.TestEngine.CurrencyUtils
{
    public static class ECurrencyExtensions
    {
        /// <summary>
        /// Get default currency amount for test
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static decimal ToDefaultCurrencyAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.sUsdcg:
                case ECurrency.Usdcg:
                case ECurrency.sSgDg:
                    return 1m;
                case ECurrency.Btc:
                    return 0.0001m;
                case ECurrency.NgNg:
                case ECurrency.sNgNg:
                    return 501m;
                default:
                    throw new Exception("No existing amount for currency");
            }
        }

        /// <summary>
        /// Test Amounts we use for AmountTooSmall
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static decimal ToAmountTooSmallCurrencyAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.sUsdcg:
                case ECurrency.Usdcg:
                    return 0.99m;
                case ECurrency.Btc:
                    return 0.000099m;
                default:
                    throw new Exception("No existing smallest amount for currency");
            }
        }

        /// <summary>
        /// Get default currency fee for test
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToDefaultCurrencyFee(this ECurrency currency, decimal fee)
        {
            switch (currency)
            {
                case ECurrency.sUsdcg:
                case ECurrency.Usdcg:
                    return (2m * fee).ToString();
                case ECurrency.Btc:
                    return (2.5m * fee).ToString();
                default:
                    throw new Exception("No existing fee for currency");
            }
        }

        /// <summary>
        /// Return Expired X-Request Signature
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToExpiredXRequestSignature(this ECurrency currency)
        {
            return currency switch
            {
                ECurrency.NgNg => "MTYxNzE0ODY1MC4weDY5NDJjOTk4ZDdlOTNlMjAwMDk4MWRjNjFkZjQ5ZTg2NGJiMWFlYmIxNDA4YjI5ODVkNDk1ZWMyZTU1YmNiZDQxZTA5MmRkM2YzNDVhN2M0NGFjYTY2NTZiZDA2NDEwYTljNGJjOWUyNTY5OGZiYzVjNTc5NmI0YzRlN2Y5YjVjMWM=",
                ECurrency.sNgNg => "MTYyNTIzMzc2Ni4weGRkMTZjMTYwMjgwNmU2NThkNTZkZmRhYjk2Zjk0YWM4NTU4OGNjNGJiY2FiNmViNGE3Mzk3MzI0MGUwYTlkN2I3Yjg3ZWYxNzg0ZGQyNmRjMjMyZWIxNGE5NTNiZjJkNzYzYmFiMmI2ZjZmOGQwMDM2MzhmOGM2NDg5YmZmOTc3MWM=",
                ECurrency.sUsdcg => "MTYxMjMxNjcxMy4weDY3MGI0ZDMxODIxNmJkOWNiOGM5M2I3ZWRjOGY4YjY2NDFjMTkyYzc1MDZlZjVkZDllMDgzNzQ4YzBkMTliNjIzYTQ3NjQyNzcxNmQ2YWQ3NTQ5NzczZmY5MWU5OWU1YjllYjBkNzUzMTBkNTc5NTQ5YjVkYmRlM2RiNmVjNWE0MWM=",
                ECurrency.Btc => "MTU5MTA0ODUyMi5JQmw0TWtlZ1pKUndsa1NTcFdaR1RWRWdUM0JiaFZlRVBYNXhyTDRIMXBPbkRaN1RlVW5LVFo1ekhKSkNQZkxRMUJPN25KUmhwK00xbTZKUGlGSzFGREk9",
                ECurrency.Usdcg => "MTYyNTIyOTE2MC4weDJkYWQ2ZGRiMjRmOTY4MzVkNTJkYzlmZDQxZDg0ZjYzODVhNGJjZmJmZGJhMmQ1ZGUyMDUyNjIyZTIwZDI0YWI1YWUyYjMyYTg5MGM3YTZmNGZkOWJkOTI2MzU4ZWIzOWExMTVlZWE0ODJiYzM3ODNiMWE0YzFhNGQxM2Q3OTMyMWM=",
                ECurrency.Eth => "MTY1MTE4OTQxNS4weGM4MzU3NmFlNjQ4NGIwMjUyMmY2MmU0NjQ0ZWE4ODEzNzVkODUzYWQyYjAwNjAwNTk2ZmFiNDMxOGZmYTBhN2MxNzczNzI0MWYxMTBkZWZiZTRhYzM1YmVhMTJiMTllODI0NGVkODI5N2JkMGM5OWJhNDNlY2ZlYWVmYzUyMjcwMWI=",
                ECurrency.Usdc => "MTY1MTI1NzQzMS4weDQ1ZDI2N2ZhOGM5MDliMjNhOWY0NzY4OGQ3MjI4MmZhM2RmZjg2OWIyZWMyNDliNDdkOTgyY2VmM2E5OTI0MDU1ZDA4ZjNkNGJlNzI1NzZjMmVhMjkyMDZiMTdlYWZlMWIwMmZkZTBlZGUyYmQ1Nzc5YTgyMDE3YzBhYzAxYjE3MWI=",
                ECurrency.Usdt => "MTY1MTI1NzUwNC4weDI0MGJkODhiNDY0NjY2Zjk0ZjNhMTkyZTA5NjQ1ZjdhMjQ4OWE3ODM3ZjEwMjI3NzVlYmM1Y2Q4NDE2YzJjZWIwZmE2NTI0OGVlZWJkNDY5OTc4YjhhYjY1ZGEwMjk0ODBjNmIzMDEwYjAzYTcwZjhlMzRiNDg2ZmY3MWI3NmE1MWM=",
                ECurrency.Gcre => "MTY1MTI1NzY5MS4weDYzZmUxNDMyYWViNzMzOGUyNjU0YjU2NzQyNWVlYjM5MDc5OWFhZTUzNGZiNjAwNDVhMWYyZTlmODc1OGVjMWE0Zjc4MTAzY2E2NzQ0NGQ2OWY3ODQ2NzdkOWE5ZTc5NDUyZjA2ZGQ1NjQyODhlY2QxZWQ0OWM2ZjJiZWY3N2EyMWI=",
                ECurrency.Gate => "MTY1MTI1NzY5MS4weDYzZmUxNDMyYWViNzMzOGUyNjU0YjU2NzQyNWVlYjM5MDc5OWFhZTUzNGZiNjAwNDVhMWYyZTlmODc1OGVjMWE0Zjc4MTAzY2E2NzQ0NGQ2OWY3ODQ2NzdkOWE5ZTc5NDUyZjA2ZGQ1NjQyODhlY2QxZWQ0OWM2ZjJiZWY3N2EyMWI=",
                _ => throw new Exception($"No expired X-Request Signature available for {currency}"),
            };
        }
    }
}
