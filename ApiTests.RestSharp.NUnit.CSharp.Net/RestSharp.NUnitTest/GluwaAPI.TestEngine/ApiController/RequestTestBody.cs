using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GluwaAPI.TestEngine.ApiController
{
    public class RequestTestBody
    {
        /// <summary>
        /// Generate valid idem
        /// </summary>
        /// <returns></returns>
        internal static string GetIdem()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Create body for POST v1/Quote
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="amount"></param>
        /// <param name="sender"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static PostQuoteBody CreatePostQuoteBody(EConversion conversion,
                                                        decimal amount,
                                                        AddressItem sender,
                                                        AddressItem receiver)
        {
            PostQuoteBody body = new PostQuoteBody()
            {
                Conversion = conversion.ToString(),
                SendingAddress = sender.Address,
                SendingAddressSignature = SignaturesUtil.GetXRequestSignature(sender.PrivateKey),
                ReceivingAddress = receiver.Address,
                ReceivingAddressSignature = SignaturesUtil.GetXRequestSignature(receiver.PrivateKey),
                Amount = amount.ToString()
            };

            if (conversion.ToSourceCurrency() == ECurrency.Btc)
            {
                body.BtcPublicKey = sender.PublicKey;
            }

            return body;
        }

        /// <summary>
        /// Create body for POST v1/Order
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="sourceAmount"></param>
        /// <param name="price"></param>
        /// <param name="maker"></param>
        /// <returns></returns>
        public static PostOrderBody CreatePostOrderBody(EConversion conversion,
                                                        decimal sourceAmount,
                                                        decimal price,
                                                        ExchangeAddressItem maker)
        {
            PostOrderBody body = new PostOrderBody()
            {
                Conversion = conversion.ToString(),
                SendingAddress = maker.Sender.Address,
                SendingAddressSignature = SignaturesUtil.GetXRequestSignature(maker.Sender.PrivateKey),
                ReceivingAddress = maker.Receiver.Address,
                ReceivingAddressSignature = SignaturesUtil.GetXRequestSignature(maker.Receiver.PrivateKey),
                SourceAmount = sourceAmount.ToString(),
                Price = price.ToString()
            };

            if (conversion.ToSourceCurrency() == ECurrency.Btc)
            {
                body.BtcPublicKey = maker.Sender.PublicKey;
            }

            return body;
        }

        /// <summary>
        /// Returns body for PUT v1/Quote
        /// </summary>
        /// <param name="quote"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static PutQuoteBody CreatePutQuoteBody(EConversion conversion,
                                                      PostQuoteResponse quote,
                                                      AddressItem sender,
                                                      string environment)
        {
            // Set up items needed for body
            List<MatchedOrderBody> matchedOrderBodyList = new List<MatchedOrderBody> { };
            List<MatchedOrder> matchedOrders = quote.MatchedOrders;

            if (conversion.ToSourceCurrency() == ECurrency.Btc)
            {
                for (var i = 0; i < matchedOrders.Count; i++)
                {
                    CreateSignatureResponse BtcSignatureResponses = SignaturesUtil.CreateBtcExchangeSignature(quote.MatchedOrders[0], sender);
                    MatchedOrderBody order = new MatchedOrderBody()
                    {
                        OrderID = matchedOrders[i].OrderID,
                        ReserveTxnSignature = BtcSignatureResponses.reserveTxnSignature,
                        ExecuteTxnSignature = BtcSignatureResponses.executeTxnSignature,
                        ReclaimTxnSignature = BtcSignatureResponses.reclaimTxnSignature
                    };
                    matchedOrderBodyList.Add(order);

                }
            }
            else
            {
                for (var i = 0; i < matchedOrders.Count; i++)
                {
                    GluwacoinSignature gluwaSignature = SignaturesUtil.GetGluwacoinReservedTxnSignature(sender.Address,
                                                                                                        sender.PrivateKey,
                                                                                                        quote.MatchedOrders[0],
                                                                                                        environment,
                                                                                                        conversion.ToSourceCurrency());
                    MatchedOrderBody order = new MatchedOrderBody()
                    {
                        OrderID = matchedOrders[i].OrderID,
                        ReserveTxnSignature = gluwaSignature.ReserveTxnSignature,
                        Nonce = gluwaSignature.Nonce
                    };
                    matchedOrderBodyList.Add(order);
                }
            }

            PutQuoteBody body = new PutQuoteBody()
            {
                Checksum = quote.Checksum,
                MatchedOrders = matchedOrderBodyList
            };

            return body;
        }

        /// <summary>
        /// Created body for PATCH v1/ExchangeRequests
        /// </summary>
        /// <param name="exchangeRequest">ExchangeRequest item</param>
        /// <param name="sender">Address Item</param>
        /// <returns></returns>
        public static PatchExchangeRequestBody CreatePatchExchangeRequestBody(EConversion conversion,
                                                                              GetExchangeRequestResponse exchangeRequest,
                                                                              AddressItem sender,
                                                                              string environment)
        {
            // Set up items needed for body
            PatchExchangeRequestBody body = new PatchExchangeRequestBody(sender.Address);
            ECurrency makerSourceCurrency = conversion.ToSourceCurrency();

            if (makerSourceCurrency == ECurrency.Btc)
            {
                // Btc as the source
                CreateSignatureResponse signatures = SignaturesUtil.CreateBtcExchangeSignature(exchangeRequest, sender);
                body.ReserveTxnSignature = signatures.reserveTxnSignature;
                body.ExecuteTxnSignature = signatures.executeTxnSignature;
                body.ReclaimTxnSignature = signatures.reclaimTxnSignature;
                return body;
            }
            else
            {
                // Get the signature and nonce for Gluwacoin
                GluwacoinSignature quoteSignature = SignaturesUtil.GetGluwacoinReservedTxnSignature(sender.Address,
                                                                                                    sender.PrivateKey,
                                                                                                    exchangeRequest,
                                                                                                    environment,
                                                                                                    conversion.ToSourceCurrency());
                body.ReserveTxnSignature = quoteSignature.ReserveTxnSignature;
                body.Nonce = quoteSignature.Nonce;
                return body;
            }
        }

        /// <summary>
        /// Create body for POST for v1/Transactions
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <param name="receiverAddress"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PostTransactionsBody CreatePostTransactionsBody(AddressItem senderAddress,
                                                                      string receiverAddress,
                                                                      ECurrency currency,
                                                                      decimal amount,
                                                                      decimal fee,
                                                                      string environment)
        {
            // Create signature
            string note = $"Test transaction Created at {System.DateTime.Now:yyyyMMddHHmmssffff}";

            if (currency == ECurrency.Btc)
            {
                string signature = SignaturesUtil.CreateBtcTransferSignature(fee.ToString(),
                                                                            amount.ToString(),
                                                                            senderAddress.Address,
                                                                            receiverAddress,
                                                                            senderAddress.PrivateKey);
                return new PostTransactionsBody(signature,
                                                amount.ToString(),
                                                senderAddress.Address,
                                                currency.ToString(),
                                                receiverAddress,
                                                fee.ToString(),
                                                GetIdem(),
                                                note);
            }
            else if (currency == ECurrency.Eth)
            {
                // Create signature
                var signature = SignaturesUtil.CreateEtherTransferSignature(senderAddress.PrivateKey,
                                                                                 receiverAddress,
                                                                                 amount,
                                                                                 currency,
                                                                                 environment);
                var body = new PostTransactionsBody(signature,
                                                    amount.ToString(),
                                                    senderAddress.Address,
                                                    currency.ToString(),
                                                    receiverAddress,
                                                    fee.ToString(),
                                                    GetIdem(),
                                                    note);


                return body;
            }
            else if (currency == ECurrency.Gcre || currency == ECurrency.Usdc || currency == ECurrency.Usdt || currency == ECurrency.Gate)
            {
                // Create signature
                var signature = SignaturesUtil.CreateCoinTransferSignature(senderAddress,
                                                                                 receiverAddress,
                                                                                 amount,
                                                                                 currency,
                                                                                 environment);
                var body = new PostTransactionsBody(signature,
                                                    amount.ToString(),
                                                    senderAddress.Address,
                                                    currency.ToString(),
                                                    receiverAddress,
                                                    fee.ToString(),
                                                    GetIdem(),
                                                    note);


                return body;
            }
            else if (currency == ECurrency.BnbUsdc)
            {
                // Create signature
                var signature = SignaturesUtil.PostTransferBnbSignatureAsync(senderAddress,
                                                                                 receiverAddress,
                                                                                 amount,
                                                                                 environment,
                                                                                 currency);
                var body = new PostTransactionsBody(signature.Result,
                                                    amount.ToString(),
                                                    senderAddress.Address,
                                                    currency.ToString(),
                                                    receiverAddress,
                                                    fee.ToString(),
                                                    GetIdem(),
                                                    note);
                return body;
            }
            else
            {
                var (signature, nonce) = SignaturesUtil.GetGluwacoinTxnSignature(senderAddress.Address,
                                                                               senderAddress.PrivateKey,
                                                                               receiverAddress,
                                                                               fee.ToString(),
                                                                               amount.ToString(),
                                                                               currency.ToString(),
                                                                               environment);
                var body = new PostTransactionsBody(signature,
                                                    amount.ToString(),
                                                    senderAddress.Address,
                                                    currency.ToString(),
                                                    receiverAddress,
                                                    fee.ToString(),
                                                    GetIdem(),
                                                    note);
                body.Nonce = nonce;

                return body;
            }
        }

        /// <summary>
        /// Create body for POST v1/Pegs
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="sender"></param>
        /// <param name="receiver"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="idem"></param>
        /// <returns></returns>
        public static PostPegsBody CreatePegsBody(ECurrency currency,
                                                  AddressItem sender,
                                                  string receiver,
                                                  string amount,
                                                  string fee,
                                                  string idem,
                                                  string environment)
        {
            (string signature, string nonce) = SignaturesUtil.GetGluwacoinTxnSignature(sender.Address,
                                                                                       sender.PrivateKey,
                                                                                       receiver,
                                                                                       fee,
                                                                                       amount,
                                                                                       currency.ToString(),
                                                                                       environment);
            return new PostPegsBody()
            {
                Source = sender.Address,
                Amount = amount,
                Currency = currency.ToString(),
                Fee = fee,
                Signature = signature,
                Idem = idem,
                Nonce = nonce
            };
        }

        /// <summary>
        /// Create body for POST v1/Unpegs
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="sender"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <returns></returns>
        public static PostUnpegsBody CreateUnpegsBody(ECurrency currency,
                                                      AddressItem sender,
                                                      string sidechainABI,
                                                      string amount,
                                                      string fee,
                                                      string environment)
        {
            string signature = SignaturesUtil.GetAllowanceSignature(currency, sender.PrivateKey, sidechainABI, amount, environment);

            string idem = TransferFunctions.GenerateIdem();

            return new PostUnpegsBody()
            {
                Source = sender.Address,
                Amount = amount,
                Currency = currency.ToString(),
                Fee = fee,
                AllowanceSignature = signature,
                Idem = idem,
            };
        }

        /// <summary>
        /// Create body for POST v1/QRCode
        /// </summary>
        /// <param name="targetAddress"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="note"></param>
        /// <param name="merchantOrderID"></param>
        /// <returns></returns>
        public static PostQRCodeBody CreatePostQRCodeBody(AddressItem targetAddress,
                                                          string currency,
                                                          string amount,
                                                          string note = "",
                                                          string merchantOrderID = "")
        {
            string signature = SignaturesUtil.GetXRequestSignature(targetAddress.PrivateKey);
            var body = new PostQRCodeBody(targetAddress.Address, signature, currency, amount);

            if (!string.IsNullOrEmpty(note)) { body.Note = note; }

            if (!string.IsNullOrEmpty(merchantOrderID)) { body.MerchantOrderID = merchantOrderID; }

            return body;
        }

        /// <summary>
        /// Create body for POST v1/Investments (Deposit option)
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="sender"></param>
        /// <param name="sidechainABI"></param>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PostInvestmentRequestBody CreatePostInvestmentDepositBody(string amount,
                                                                               AddressItem sender,
                                                                               string action,
                                                                               string type,
                                                                               string environment)
        {
            string signature = SignaturesUtil.GetAllowanceSignature(sender.PrivateKey, amount, type, environment);


            PostInvestmentRequestBody body = new PostInvestmentRequestBody()
            {
                Action = action,
                AccountType = type,
                Address = sender.Address,
                ApproveTxnSignature = signature,
                Amount = amount,
                SideLetterDocumentId = null,
                SubscriptionAgreementDocumentId = null
            };
            // TODO: create a new endpoint in qa.functions 
            body.SideLetterDocumentId = Shared.GetSideLetterDocument(type, environment);
            body.SubscriptionAgreementDocumentId = Shared.SubscriptionAgreementDocumen(type, environment);

            return body;
        }

        /// <summary>
        /// Create body for POST v1/Investments (PLSA/Withdraw option)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PostInvestmentRequestBody CreatePostInvestmentWithdrawBody(AddressItem sender,
                                                                                 string amount,
                                                                                 string action,
                                                                                 string type)
        {
            PostInvestmentRequestBody body = new PostInvestmentRequestBody()
            {
                Action = action,
                AccountType = type,
                Address = sender.Address,
                Amount = amount,
            };

            return body;
        }

        /// <summary>
        /// Create body for POST v1/Investments (Bond/Withdraw option)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="action"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PostInvestmentRequestBody CreatePostInvestmentWithdrawBody(AddressItem sender,
                                                                                 string action,
                                                                                 string type)
        {
            PostInvestmentRequestBody body = new PostInvestmentRequestBody()
            {
                Action = action,
                AccountType = type,
                Address = sender.Address,
            };

            return body;
        }

        /// <summary>
        /// Create body for POST v1/TokenWrapping/Request
        /// </summary>
        /// <param name="currencySource"></param>
        /// <param name="currencyTarget"></param>
        /// <param name="sender"></param>
        /// <param name="amount"></param>
        /// <param name="idem"></param>
        /// <returns></returns>
        public static PostTokenWrappingBody CreateTokenWrappingBody(string currencySource,
                                                                    ECurrency currencyTarget,
                                                                    AddressItem sender,
                                                                    string amount,
                                                                    string idem,
                                                                    string environment)
        {
            long BIGINT_CONVERSION = 1_000_000;
            decimal dAmount = decimal.Parse(amount);
            dAmount *= BIGINT_CONVERSION;
            BigInteger amountBig = new BigInteger(dAmount);
            (string signature, string mintSignature) = SignaturesUtil.GenerateRawMintSignature(sender, amountBig, environment);

            return new PostTokenWrappingBody()
            {
                Amount = amount,
                Address = sender.Address,
                ApproveTxnSignature = signature,
                SourceToken = currencySource,
                TargetToken = currencyTarget.ToString(),
                MintTxnSignature = mintSignature,
                IdempotentKey = idem,
            };
        }

        /// <summary>
        /// Create body for POST v1/TokenUnwrapping/Request
        /// </summary>
        /// <param name="currencyTarget"></param>
        /// <param name="currencySource"></param>
        /// <param name="sender"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="idem"></param>
        /// <returns></returns>
        public static PostTokenUnwrappingBody CreateTokenUnWrappingBody(ECurrency currencyTarget,
                                                                        string currencySource,
                                                                        AddressItem sender,
                                                                        string amount,
                                                                        string fee,
                                                                        string idem,
                                                                        string environment)
        {
            long BIGINT_CONVERSION = 1_000_000;
            decimal dFee = decimal.Parse(fee);
            dFee *= BIGINT_CONVERSION;
            BigInteger feeBig = new BigInteger(dFee);
            decimal dAmount = decimal.Parse(amount);
            dAmount *= BIGINT_CONVERSION;
            BigInteger amountBig = new BigInteger(dAmount);
            string nonce = SignaturesUtil.GetNonce();
            BigInteger nonceBig = BigInteger.Parse(nonce);

            string burnSignature = SignaturesUtil.GetBurnSignature(sender.PrivateKey,
                                                                   amountBig,
                                                                   feeBig,
                                                                   nonceBig,
                                                                   environment);
            return new PostTokenUnwrappingBody()
            {
                SourceToken = currencyTarget.ToString(),
                TargetToken = currencySource,
                Amount = amount,
                BurnSignature = burnSignature,
                Nonce = nonce,
                Fee = fee,
                Address = sender.Address,
                IdempotentKey = idem,
            };
        }

        /// <summary>
        /// Create body for POST v1/gatewaydao/stake
        /// </summary>
        public static PostStakeBody CreateStakeBody(AddressItem owner,
                                                    string amount,
                                                    string idem,
                                                    string environment)
        {
            (string signature, string mintSignature) = SignaturesUtil.GenerateMintToStakeSignature(owner, amount, environment);

            return new PostStakeBody()
            {
                Source = owner.Address,
                Amount = amount,
                MintToStakeSignature = mintSignature,
                ApprovalSignature = signature,
                Idem = idem
            };
        }

        /// <summary>
        /// Create body for POST v1/gatewaydao/unstake
        /// </summary>
        public static PostUnstakeBody CreateUnstakeBody(AddressItem owner,
                                                        string amount,
                                                        string idem,
                                                        string environment)
        {
            string signature = SignaturesUtil.GenerateGtdUnstakeSignature(owner, amount, environment);

            return new PostUnstakeBody()
            {
                Source = owner.Address,
                Amount = amount,
                UnstakeRawTxnSignature = signature,
                Idem = idem
            };
        }

        /// <summary>
        /// Create body for POST v1/gatewaydao/burn
        /// </summary>
        public static PostBurnBody CreateBurnBody(AddressItem owner,
                                                        string amount,
                                                        string idem,
                                                        string environment)
        {
            string signature = SignaturesUtil.GenerateGtdBurnSignature(owner, amount, environment);

            return new PostBurnBody()
            {
                Source = owner.Address,
                Amount = amount,
                BurnRawTxnSignature = signature,
                Idem = idem
            };
        }

        /// <summary>
        /// Create body for POST v1/gatewaydao/claim
        /// </summary>
        public static PostClaimBody CreateClaimBody(AddressItem owner,
                                                    string environment,
                                                    string idem)
        {
            string signature = SignaturesUtil.GenerateGateMintAllRewardSignature(owner, environment);

            return new PostClaimBody()
            {
                Source = owner.Address,
                Signature = signature,
                Currency = ECurrency.Gate.ToString(),
                Idem = idem
            };
        }

        /// <summary>
        /// Create KYC Details Body
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="addressLine1"></param>
        /// <param name="city"></param>
        /// <param name="province"></param>
        /// <param name="postalCode"></param>
        /// <param name="countryIsoCode"></param>
        /// <param name="birthDate"></param>
        /// <param name="birthPlace"></param>
        /// <param name="nationality"></param>
        /// <param name="idCardNo"></param>
        /// <param name="occupation"></param>
        /// <param name="sourceOfFunds"></param>
        /// <param name="taxReferenceNumberType"></param>
        /// <param name="taxReferenceNumber"></param>
        /// <returns></returns>
        public static PostKYCDetailsBody CreateKYCDetailsBody(string firstName,
                                                              string lastName,
                                                              string addressLine1,
                                                              string city,
                                                              string province,
                                                              string postalCode,
                                                              string countryIsoCode,
                                                              string birthDate,
                                                              string birthPlace,
                                                              string nationality,
                                                              string idCardNo,
                                                              string occupation,
                                                              string sourceOfFunds,
                                                              string taxReferenceNumberType,
                                                              string taxReferenceNumber)
        {
            PostKYCDetailsBody body = new PostKYCDetailsBody()
            {
                FirstName = firstName,
                LastName = lastName,
                AddressLine1 = addressLine1,
                City = city,
                Province = province,
                PostalCode = postalCode,
                CountryIsoCode = countryIsoCode,
                BirthDate = DateTime.Parse(birthDate),
                BirthPlace = birthPlace,
                Nationality = nationality,
                IdCardNo = idCardNo,
                Occupation = occupation,
                SourceOfFunds = sourceOfFunds,
                TaxReferenceNumberType = taxReferenceNumberType,
                TaxReferenceNumber = taxReferenceNumber
            };

            return body;
        }

        /// <summary>
        /// Create body for POST V1/Transactions/Ethereum/CreateReplaceData
        /// </summary>
        /// <param name="txnId"></param>
        /// <param name="signature"></param>
        /// <param name="hash"></param>
        /// <param name="replaceId"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static PostTransactionReplaceDataBody CreateTransactionReplaceDataBody(string txnId,
                                                                                      string signature,
                                                                                      string hash,
                                                                                      string idem,
                                                                                      string replaceId,
                                                                                      ECurrency currency)
        {
            return new PostTransactionReplaceDataBody(
                txnId,
                idem,
                signature,
                hash,
                replaceId,
                currency.ToString()
                );
        }

        /// <summary>
        /// Create body for PUT v1/Transactions/Ethereum/TransferReplace
        /// </summary>
        /// <param name="txnId"></param>
        /// <param name="senderAddress"></param>
        /// <param name="address"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PutTransactionReplaceDataBody CreatePutTransactionReplaceDataBody(string txnId,
                                                                                        AddressItem senderAddress,
                                                                                        string address,
                                                                                        ECurrency currency,
                                                                                        decimal amount,
                                                                                        decimal fee,
                                                                                        string environment)
        {
            // Create signature
            string note = $"Created at {System.DateTime.Now:yyyyMMddHHmmssffff}";

            if (currency == ECurrency.Eth)
            {
                // Create signature
                var signature = SignaturesUtil.CreateEtherTransferSignature(senderAddress.PrivateKey,
                                                                            address,
                                                                            amount,
                                                                            currency,
                                                                            environment);
                var body = new PutTransactionReplaceDataBody(txnId,
                                                            signature,
                                                            amount.ToString(),
                                                            senderAddress.Address,
                                                            currency.ToString(),
                                                            address,
                                                            fee.ToString(),
                                                            GetIdem(),
                                                            note);


                return body;
            }
            else
            {
                // Create signature
                var signature = SignaturesUtil.CreateCoinTransferSignatureAsync(senderAddress,
                                                                                address,
                                                                                amount,
                                                                                currency,
                                                                                environment);
                var body = new PutTransactionReplaceDataBody(txnId,
                                                            signature.Result,
                                                            amount.ToString(),
                                                            senderAddress.Address,
                                                            currency.ToString(),
                                                            address,
                                                            fee.ToString(),
                                                            GetIdem(),
                                                            note);

                return body;
            }
        }

        /// <summary>
        /// Create body for POST V1/FtaPools/{PoolID}/Approve
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="senderAddress"></param>
        /// <param name="poolId"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PostFtaApproveBody CreatePostApproveTransactionBody(string amount,
                                                                          AddressItem senderAddress,
                                                                          string poolId,
                                                                          ECurrency currency,
                                                                          string environment)

        {
            var signature = SignaturesUtil.GetFtaApproveSignature(senderAddress, amount, environment, currency);

            PostFtaApproveBody body = new PostFtaApproveBody()
            {
                OwnerAddress = senderAddress.Address,
                ApproveTxnSignature = signature,
                Amount = amount,
                PoolID = poolId
            };

            return body;
        }

        /// <summary>
        /// Create body for POST V1/FtaPools/Deposit
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <param name="poolId"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PostFtaDepositBody CreatePostFtaDepositBody(AddressItem senderAddress,
                                                                string poolId,
                                                                string amount,
                                                                ECurrency currency,
                                                                string environment)
        {
            string signature;
            string nonce = AmountUtils.GetNonce();
            BigInteger nonceBig = BigInteger.Parse(nonce);
            (string r, string s, int v, bool IsCreateAccount, string poolHash, string identityHash) = TransferFunctions.generateRsvAndHash(senderAddress,
                                                                                                                        poolId,
                                                                                                                        amount,
                                                                                                                        nonce,
                                                                                                                        currency,
                                                                                                                        environment);

            if (IsCreateAccount == true)
            {
                var signatureA = SignaturesUtil.CreateAccountBySigSignature(senderAddress.PrivateKey,
                                                                           senderAddress.Address,
                                                                           amount,
                                                                           identityHash,
                                                                           poolHash,
                                                                           nonceBig,
                                                                           v,
                                                                           r,
                                                                           s,
                                                                           environment,
                                                                           currency: currency);
                signature = signatureA.Result;
            }
            else
            {
                var signatureB = SignaturesUtil.CreateBalanceBySigSignature(senderAddress.PrivateKey,
                                                                           senderAddress.Address,
                                                                           amount,
                                                                           poolHash,
                                                                           nonceBig,
                                                                           v,
                                                                           r,
                                                                           s,
                                                                           currency: currency,
                                                                           environment: environment);
                signature = signatureB.Result;
            }

            PostFtaDepositBody body = new PostFtaDepositBody()
            {
                OwnerAddress = senderAddress.Address,
                GluwaNonce = nonce,
                DepositTxnSignature = signature,
                Amount = amount,
                PoolId = poolId,
                Currency = currency.ToString(),
            };
            return body;
        }

        /// <summary>
        /// Create body for POST V1/FtaPools/Deposit
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <param name="poolId"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PostFtaDepositBody CreatePostFtaDepositBodyNegativeTests(AddressItem senderAddress,
                                                                                string poolId,
                                                                                string amount,
                                                                                ECurrency currency,
                                                                                string environment)
        {
            string nonce = AmountUtils.GetNonce();
            BigInteger nonceBig = BigInteger.Parse(nonce);
            int v = 28;
            string s = Shared.VALID_TXNID;  // Dummy s
            string r = Shared.VALID_TXNID;  // Dummy r
            string poolHash = Shared.VALID_TXNID;  // Dummy pool hash

            var signature = SignaturesUtil.CreateBalanceBySigSignature(senderAddress.PrivateKey,
                                                                       senderAddress.Address,
                                                                       amount,
                                                                       poolHash,
                                                                       nonceBig,
                                                                       v,
                                                                       r,
                                                                       s,
                                                                       environment: environment);
            PostFtaDepositBody body = new PostFtaDepositBody()
            {
                OwnerAddress = senderAddress.Address,
                GluwaNonce = nonce,
                DepositTxnSignature = signature.Result,
                Amount = amount,
                PoolId = poolId,
                Currency = currency.ToString(),
            };
            return body;
        }

        /// <summary>
        /// Get an Authorization Signature
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <param name="poolId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static PostFtaAuthorizationBody CreatePostFtaAuthorizationBody(AddressItem senderAddress,
                                                                              string poolId,
                                                                              string amount,
                                                                              string nonce,
                                                                              ECurrency currency)
        {
            PostFtaAuthorizationBody body = new PostFtaAuthorizationBody()
            {
                OwnerAddress = senderAddress.Address,
                Amount = amount,
                PoolId = poolId,
                Currency = currency.ToString(),
                GluwaNonce = nonce
            };

            return body;
        }

        /// <summary>
        /// POST Fta withdraw
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <param name="poolId"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PostFtaWithdrawBody CreatePostFtaWithdrawBody(AddressItem senderAddress,
                                                                    string poolId,
                                                                    ECurrency currency,
                                                                    string environment)
        {
            List<string> hash = TransferFunctions.getMatureBalanceHash(senderAddress, poolId, currency, environment);
            List<string> balanceHashStrings = hash;
            var signature = SignaturesUtil.CreateWithdrawBalancesSignature(senderAddress.PrivateKey,
                                                                           senderAddress.Address,
                                                                           balanceHashStrings.ToArray(),
                                                                           environment,
                                                                           currency);
            PostFtaWithdrawBody body = new PostFtaWithdrawBody()
            {
                OwnerAddress = senderAddress.Address,
                TxnSignature = signature.Result,
                PoolID = poolId,
                Currency = currency.ToString(),
                GluwaNonce = AmountUtils.GetNonce()
            };

            return body;
        }

        /// <summary>
        /// POST Fta withdraw
        /// </summary>
        /// <param name="senderAddress"></param>
        /// <param name="poolId"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static PostFtaWithdrawBody CreatePostFtaWithdrawBodyNegativeTests(AddressItem senderAddress,
                                                                                string poolId,
                                                                                ECurrency currency,
                                                                                string environment)
        {
            List<string> balanceHashStrings = new List<string>();
            balanceHashStrings.Add(Shared.VALID_TXNID);  // Dummy hash
            var signature = SignaturesUtil.CreateWithdrawBalancesSignature(senderAddress.PrivateKey,
                                                                           senderAddress.Address,
                                                                           balanceHashStrings.ToArray(),
                                                                           environment,
                                                                           currency);
            PostFtaWithdrawBody body = new PostFtaWithdrawBody()
            {
                OwnerAddress = senderAddress.Address,
                TxnSignature = signature.Result,
                PoolID = poolId,
                Currency = currency.ToString(),
                GluwaNonce = AmountUtils.GetNonce()
            };

            return body;
        }

        /// <summary>
        /// POST V1/Wallet body
        /// </summary>
        /// <param name="address"></param>
        /// <param name="chainId"></param>
        /// <returns></returns>
        public static object CreatePostWalletBody(string address, string chainId)
        {
            {
                PostWalletBody body = new PostWalletBody()
                {
                    Address = address,
                    ChainTypeId = chainId
                };

                return body;
            }
        }
    }
}
