using GluwaAPI.TestEngine.Models;
using System.Collections.Generic;
using System.Net;

namespace GluwaAPI.TestEngine.EErrorTypeUtils
{
    public class ErrorOptions 
    {
     
        /*
         * Expected Error body
         */

		// Exchange AmountTooSmall message for BTC
        public static ValidationErrorItem AmountTooSmallBTC
        {
            get
            {
                return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, getAmountTooSmall("Amount", EXCHANGE_AMOUNT_TOO_SMALL_BTC));
            }
        }		
		
		// Order Exchange Source AmmountTooSmall for BTC
        public static ValidationErrorItem SourceAmountTooSmallBTC
        {
            get
            {
                return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, getAmountTooSmall("SourceAmount", "Source" + EXCHANGE_AMOUNT_TOO_SMALL_BTC));
            }
        }
		
		// Invalid Txn Amount message for zero txns
        public static ValidationErrorItem InvalidTxnAmount
        {
            get
            {
                List<InnerError> innerErrors = new List<InnerError>()
                {
                    new InnerError(EErrorType.InvalidAmount, "Amount", ZERO_INVALID_AMOUNT)
                };
                return new ValidationErrorItem(EErrorType.ValidationError, TRANSACTION_REQUEST_ERROR, innerErrors);
            }
        }

		// Merchcant Order ID too long message for txns
        public static ValidationErrorItem LongMerchantOrderID
        {
            get
            {
                List<InnerError> innerErrors = new List<InnerError>()
                {
                    new InnerError(EErrorType.InvalidValue, "MerchantOrderID", QR_CODE_MERCHANT_ORDER_ID_LONG)
                };
                return new ValidationErrorItem(EErrorType.InvalidBody, INVALID_BODY, innerErrors);
            }
        }

        // Body
        public static ValidationErrorItem missingBody = new ValidationErrorItem(EErrorType.MissingBody, MISSING_BODY, null);
        public static ValidationErrorItem invalidSignatureBody = new ValidationErrorItem(EErrorType.BadRequest, INVALID_SIGNATURE, null);

        // Signature query
        public static ValidationErrorItem forbiddenSignature = new ValidationErrorItem(EErrorType.InvalidSignature, UNAUTHORIZED_SIGNATURE, null);
        public static ValidationErrorItem missingSignature = new ValidationErrorItem(EErrorType.SignatureMissing, MISSING_SIGNATURE, null);
        public static ValidationErrorItem expiredSignature = new ValidationErrorItem(EErrorType.SignatureExpired, EXPIRED_SIGNATURE, null);
        public static ValidationErrorItem invalidBase64Format = new ValidationErrorItem(EErrorType.InvalidSignature, INVALID_ETH_TXN_SIGNATURE_FORMAT, null);
        public static ValidationErrorItem invalidAddressSignatureFormat = new ValidationErrorItem(EErrorType.InvalidSignature, INVALID_ADDRESS_SIGNATURE, null);
        public static ValidationErrorItem invalidNotificationSignature = new ValidationErrorItem(EErrorType.BadRequest, INVALID_NOTIFICATION_SIGNATURE, null);
        
        // Query Parameters
        public static ValidationErrorItem invalidTxnHashQuery = new ValidationErrorItem(EErrorType.BadRequest, INVALID_TXN_HASH, null);
        public static ValidationErrorItem invalidAddressQuery = new ValidationErrorItem(EErrorType.BadRequest, INVALID_ADDRESS_PARAMETER, null);

        // Forbidden
        public static ValidationErrorItem invalidCredentials = new ValidationErrorItem(EErrorType.Forbidden, INVALID_CREDENTIALS, null);
        public static ValidationErrorItem invalidChecksum = new ValidationErrorItem(EErrorType.Forbidden, INVALID_CHECKSUM, null);
        public static ValidationErrorItem orderNotAvailable = new ValidationErrorItem(EErrorType.NotFound, ORDER_NOT_AVAILABLE, null);
        public static ValidationErrorItem exchangeRequestsForbidden = new ValidationErrorItem(EErrorType.Forbidden, EXCHANGE_REQUESTS_NOT_OWNED, null);

        // Conflict
        public static ValidationErrorItem acceptQuoteConflict = new ValidationErrorItem(EErrorType.Conflict, ACCEPT_QUOTE_CONFLICT, null);
        public static ValidationErrorItem exchangeRequestConflict = new ValidationErrorItem(EErrorType.Conflict, EXCHANGE_REQUEST_CONFLICT, null);
        public static ValidationErrorItem transactionUnpegConflict = new ValidationErrorItem(EErrorType.Conflict, TRANSACTION_UNPEG_EXISTED_IDEM, null);
        public static ValidationErrorItem transactionPegConflict = new ValidationErrorItem(EErrorType.Conflict, TRANSACTION_PEG_EXISTED_IDEM, null);
        public static ValidationErrorItem transactionConflict = new ValidationErrorItem(EErrorType.Conflict, TRANSACTION_EXISTED_IDEM, null);
        public static ValidationErrorItem alreadyRegistered = new ValidationErrorItem(EErrorType.Conflict, ALREADY_REGISTERED, null);
        public static ValidationErrorItem tokensConflict = new ValidationErrorItem(EErrorType.Conflict, TOKEN_REQUEST_CONFLICT, null);

        // Not Found
        public static ValidationErrorItem quoteOrderNotFound = new ValidationErrorItem(EErrorType.NotFound, MATCHED_ORDER_NOT_FOUND, null);
        public static ValidationErrorItem exchangeRequestNotFound = new ValidationErrorItem(EErrorType.NotFound, EXCHANGE_REQUEST_NOT_FOUND, null);
        public static ValidationErrorItem resourceNotFound = new ValidationErrorItem(EErrorType.NotFound, TXN_HASH_NOT_FOUND, null);
        public static ValidationErrorItem blockNotFound = new ValidationErrorItem(EErrorType.NotFound, BLOCK_NOT_FOUND, null);
        public static ValidationErrorItem transactionNotFound = new ValidationErrorItem(EErrorType.NotFound, TRANSACTION_NOT_FOUND, null);

        // QR Code
        public static ValidationErrorItem invalidQRCodeFormat = new ValidationErrorItem(EErrorType.BadRequest, UNSUPPORTED_MIME_TYPE, null);

        // Authorization
        public static ValidationErrorItem missingOtp = new ValidationErrorItem(EErrorType.MissingOtp, MISSING_OTP, null);
        public static AuthErrorItem invalidGrant = new AuthErrorItem(EErrorType.unsupported_grant_type, null);
        public static AuthErrorItem invalidScope = new AuthErrorItem(EErrorType.invalid_scope, null);
        public static AuthErrorItem invalidClient = new AuthErrorItem(EErrorType.invalid_client, null);
        public static AuthErrorItem invalidUserName = new AuthErrorItem(EErrorType.invalid_grant, INVALID_USERNAME);


        /*
         * Error Messages
        */

        // ValidationError 
        public const string VALIDATION_ERROR = "There are one or more validation errors. See InnerErrors for more details";
       
        // Body
        public const string INVALID_BODY = "One or more fields are invalid in body.";
        private const string MISSING_BODY = "body is missing.";

        // Signature
        private const string UNAUTHORIZED_SIGNATURE = "The supplied signature is not authorized for this resource.";
        private const string MISSING_SIGNATURE = "No request signature header was provided.";
        private const string EXPIRED_SIGNATURE = "Signature is expired.";
        private const string INVALID_ETH_TXN_SIGNATURE_FORMAT = "Address signature is not a valid base64-encoded value.";
        private const string INVALID_SIGNATURE = "Signature is invalid";
        private const string INVALID_ADDRESS_SIGNATURE = "Address signature does not have a valid format.";
        private const string INVALID_ADDRESS_SIGNATURE_SSGDG = "AddressSignature is not valid for the provided Address.";
        private const string SIGNATURE_COULD_NOT_BE_VERIFIED = "Signature could not be verified.";
        private const string INVALID_ADDRESS_REGISTER_SIGNATURE = "is not valid for the provided Address.";

        private const string INVALID_NOTIFICATION_SIGNATURE = "Invalid Signature.";

        // Query parameters
        private const string INVALID_TXN_HASH = "Invalid txnHash value.";
        private const string INVALID_ADDRESS_PARAMETER = "Invalid address value.";
        private const string INVALID_URL_PARAMETERS = "one of more Url parameters are invalid.";

        // Not found
        private const string MATCHED_ORDER_NOT_FOUND = "Could not find orders that can fulfill the request.";
        private const string EXCHANGE_REQUEST_NOT_FOUND = "Exchange request not found.";
        private const string TXN_HASH_NOT_FOUND = "Resource not found.";
        private const string BLOCK_NOT_FOUND = "No block found for transaction hash.";
        private const string TRANSACTION_NOT_FOUND = "Transaction not found.";

        // Forbidden
        private const string INVALID_CREDENTIALS = "Invalid credentials to access this resource.";
        private const string INVALID_CHECKSUM = "Invalid checksum";
        private const string ORDER_NOT_AVAILABLE = "Order is not available.";
        private const string EXCHANGE_REQUESTS_NOT_OWNED = "You do not own this exchange request.";

        // Conflict
        private const string ACCEPT_QUOTE_CONFLICT = "This quote has already been accepted.";
        private const string EXCHANGE_REQUEST_CONFLICT = "Exchange request cannot be accepted.";
        private const string TRANSACTION_UNPEG_EXISTED_IDEM = "An unpeg with the same Idem value has already been accepted.";
        private const string TRANSACTION_PEG_EXISTED_IDEM = "A peg with the same Idem value has already been accepted.";
        private const string TRANSACTION_EXISTED_IDEM = "A transaction from this address with the same idempotent key already exists.";
        private const string TOKEN_REQUEST_CONFLICT = "A request with the same Idem value has already been accepted.";

        // Register Address errors
        private const string ALREADY_REGISTERED = "This address is already registered.";

        // Transaction Error
        private const string TRANSACTION_REQUEST_ERROR = "Request to create transaction is not valid. See inner errors.";

        // Authorization
        private const string MISSING_OTP = "OTP header is missing.";
        private const string INVALID_USERNAME = "invalid_username_or_password";

        /*
         * Inner Error Message
         */

        // Body
        private const string ZERO_INVALID_AMOUNT = "Amount must be greater than 0.";

        // Exchange body
        private const string EXCHANGE_AMOUNT_TOO_SMALL_BTC = "Amount must be greater than or equal to 0.0001 BTC.";
        private const string NOT_ENOUGHT_BALANCE = "Not enough balance in";

        // QR Code
        private const string OR_CODE_AMOUNT_TOO_SMALL = "The Amount must be greater than the fee,";
        private const string QR_CODE_MERCHANT_ORDER_ID_LONG = "The field MerchantOrderID must be a string with a maximum length of 60.";
        private const string UNSUPPORTED_MIME_TYPE = "Unsupported MIME type.";

        // Amount errors
        private const string INSUFFICIENT_BURN_BALANCE = "Owner account does not have enough funds to burn";
        /*
         * Errors with parameters
         */

        /// <summary>
        /// Not Enough Balance error
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetExpectedNotEnoughBalanceError(string address)
        {

            List<InnerError> innerErrors = new List<InnerError>()
            {
                new InnerError(EErrorType.NotEnoughBalance, "SourceAmount", $"{NOT_ENOUGHT_BALANCE} {address}.")
            };
            return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, innerErrors);
        }

        /// <summary>
        /// Invalid query parameters
        /// </summary>
        /// <param name="invalidValue"></param>
        /// <param name="errorPath"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetExpectedInvalidQueryParameterError(string invalidValue, string errorPath)
        {
            List<InnerError> innerErrors = new List<InnerError>()
            {
                new InnerError(EErrorType.InvalidValue, errorPath, $"The value '{invalidValue}' is not valid.")
            };
            return new ValidationErrorItem(EErrorType.InvalidUrlParameters, INVALID_URL_PARAMETERS, innerErrors);

        }

        /// <summary>
        /// InsufficientBurnbalance
        /// </summary>
        /// <param name="burnAmount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetInsufficientBurnBalanceError(string burnAmount, string currency)
        {
            List<InnerError> innerError = new List<InnerError>()
            {
                new InnerError(EErrorType.InsufficientFunds, "Amount", $"{INSUFFICIENT_BURN_BALANCE} {burnAmount} {currency}.")
            };

            return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, innerError);
        }

        /// <summary>
        /// Order Not Found Error
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetExpectedOrderNotFoundError(string ID)
        {
            return new ValidationErrorItem(EErrorType.NotFound, $"Order {ID} not found.", null);
        }

        /// <summary>
        /// Expected Missing Field Error
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetExpectedMissingFieldError(string field)
        {
            List<InnerError> innerErrors = new List<InnerError>()
            {
                new InnerError(EErrorType.Required, field, $"The {field} field is required.")
            };
            return new ValidationErrorItem(EErrorType.InvalidBody, INVALID_BODY, innerErrors);
        }

        /// <summary>
        /// Expected Invalid Tokens Body
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        // Invalid Tokens body
        public static ValidationErrorItem TokensBodyInvalid
        {
            get
            {
                List<InnerError> innerError = new List<InnerError>()
                {
                    new InnerError(EErrorType.Required, "Idem", "The Idem field is required."),
                    new InnerError(EErrorType.Required, "Amount", "The Amount field is required."),

                };
                return new ValidationErrorItem(EErrorType.InvalidBody, INVALID_BODY, innerError);

            }
        }

        /// <summary>
        /// Invalid Value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetExpectedInvalidFieldError(string value, string field)
        {
            List<InnerError> innerErrors = new List<InnerError>()
            {
                new InnerError(EErrorType.InvalidValue, field, $" \"{value}\" is not a valid {field}.")
            };
            return new ValidationErrorItem(EErrorType.InvalidBody, INVALID_BODY, innerErrors);
        }

        /// <summary>
        /// To generate invalid address signature
        /// </summary>
        /// <param name="signatureType"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetInvalidAddressSignature(string signatureType)
        {
            List<InnerError> innerError = new List<InnerError>()
            {
                new InnerError(EErrorType.InvalidAddressSignature, signatureType, $"{signatureType} {INVALID_ADDRESS_REGISTER_SIGNATURE}")
            };

            return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, innerError);
        }

        // Invalid mint token amount for SSgDg
        public static ValidationErrorItem InvalidSSgDgTokenAmount
        {
            get
            {
                List<InnerError> innerError = new List<InnerError>()
                {
                    new InnerError(EErrorType.InvalidAmount, "Amount", ZERO_INVALID_AMOUNT)
                };
                return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, innerError);

            }
        }

        // Signature could not be verified for Register Address
        public static ValidationErrorItem SignatureNotVerified
        {
            get
            {
                List<InnerError> innerError = new List<InnerError>()
                {
                    new InnerError(EErrorType.InvalidAddressSignature, "Signature", SIGNATURE_COULD_NOT_BE_VERIFIED)
                };

                return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, innerError);
            }
        }

        /// <summary>
        /// Missing Required field error
        /// Wrapper from GluwaApi Error options
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetRequiredFieldError(string field)
        {
            return GetExpectedMissingFieldError(field);
        }

        /// <summary>
        /// InvalidValueError wrapper from GluwaApiError
        /// </summary>
        /// <param name="value"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetInvalidValueError(string value, string field)
        {
            return GetExpectedInvalidFieldError(value, field);
        }

        /// <summary>
        /// AmountTooSmall error for QR Code
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetQRCodeAmountTooSmallError(string fee, string currency)
        {
            List<InnerError> innerErrors = new List<InnerError>()
            {
                new InnerError(EErrorType.AmountTooSmall, "Amount", $"{OR_CODE_AMOUNT_TOO_SMALL} {fee} {currency}")
            };

            return new ValidationErrorItem(EErrorType.ValidationError, VALIDATION_ERROR, innerErrors);
        }

        /// <summary>
        /// For invalid currencies in RegisterAddress
        /// </summary>
        /// <param name="invalidValues"></param>
        /// <returns></returns>
        public static ValidationErrorItem GetInvalidCurrenciesError(List<string> invalidValues)
        {
            List<InnerError> innerError = new List<InnerError>()
            {
                new InnerError(EErrorType.InvalidValue, "Currencies[0]", $"Error converting value \"{invalidValues[0]}\" to type 'Gluwa.Commons.Currency.ECurrency'. Path 'Currencies[0]', line 1, position 76."),
                new InnerError(EErrorType.InvalidValue, "Currencies[1]", $"Error converting value \"{invalidValues[1]}\" to type 'Gluwa.Commons.Currency.ECurrency'. Path 'Currencies[1]', line 1, position 83."),
            };

            return new ValidationErrorItem(EErrorType.InvalidBody, INVALID_BODY, innerError);
        }

        /// <summary>
        /// Get too small amount
        /// </summary>
        /// <param name="amountPath"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static List<InnerError> getAmountTooSmall(string amountPath, string errorMessage)
        {
            return new List<InnerError>()
            {
                new InnerError(EErrorType.AmountTooSmall, amountPath, errorMessage)
            };
        }

        /// <summary>
        /// NotFound error handling
        /// </summary>
        /// <param name="status"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static ValidationErrorItem getErrorMsg(HttpStatusCode status, string environment)
        {
            if (environment == "Test" && status.ToString() == "NotFound")
            {
                return blockNotFound;
            }
            else return transactionNotFound;           
        }		
    }
}
