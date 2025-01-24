namespace GluwaPro.UITest

{
    /// <summary>
    /// Automation element IDs
    /// </summary>
    public static class AutomationID
    {
        // Common
        public static readonly string textHeaderBar = "textHeaderBar";
        public static readonly string labelError = "labelError";
        public static readonly string labelInvalidLoginUsername = "labelInvalidLoginUsername";
        public static readonly string labelEnterPassword = "labelEnterPassword";
        public static readonly string labelOpenGluwaWallet = "labelOpenGluwaWallet";
        public static readonly string buttonBack = "buttonBack";
        public static readonly string buttonClose = "buttonClose";
        public static readonly string buttonNext = "buttonNext";
        public static readonly string buttonNextDisabled = "buttonNextDisabled";
        public static readonly string buttonConfirm = "buttonConfirm";
        public static readonly string buttonFinish = "buttonFinish";
        public static readonly string buttonDone = "buttonDone";

        // Common - Keypad
        public static readonly string buttonKeypad0 = "button0";
        public static readonly string buttonKeypad1 = "button1";
        public static readonly string buttonKeypad2 = "button2";
        public static readonly string buttonKeypad3 = "button3";
        public static readonly string buttonKeypad4 = "button4";
        public static readonly string buttonKeypad5 = "button5";
        public static readonly string buttonKeypad6 = "button6";
        public static readonly string buttonKeypad7 = "button7";
        public static readonly string buttonKeypad8 = "button8";
        public static readonly string buttonKeypad9 = "button9";
        public static readonly string buttonBackSpace = "buttonBackSpace";
        public static readonly string buttonDecimal = "buttonDecimal";
        public static readonly string buttonCancel = "buttonCancel";

        // Common - Passcode
        public static readonly string labelEnterPasscode = "labelEnterPasscode";
        public static readonly string labelWrongPasscode = "labelWrongPasscode";
        public static readonly string textForgotPasscode = "textForgotPasscode";
        public static readonly string textUsePassword = "textUsePassword";
        public static readonly string labelPasscodesMismatch = "labelPasscodesMismatch";
        public static readonly string labelEnterNewPasscode = "labelEnterNewPasscode";
        public static readonly string labelConfirmNewPasscode = "labelConfirmNewPasscode";
        public static readonly string labelTitlePasscode = "labelTitlePasscode";

        // Common - viewWarningPopup - The automation IDs for the buttons of the alert pop are not be abel to modify due to the android style popups
        public static readonly string alertTitle = "alertTitle";
        public static readonly string message = "message";
        public static readonly string button2 = "button2"; // Skip or NOT or Deny NOW for Android
        public static readonly string button1 = "button1"; // Reset or Skip Backup or Finish or UPDATE or Allow for Android

        // Update popup for iOS
        public static readonly string labelUpdate = "Update";
        public static readonly string labelNotNow = "Not now";

        // Common - Password policy
        public static readonly string labelPasswordValidate1 = "labelPasswordValidate1";
        public static readonly string labelPasswordValidate2 = "labelPasswordValidate2";
        public static readonly string labelPasswordValidate3 = "labelPasswordValidate3";
        public static readonly string labelPasswordWeek = "labelPasswordWeek";
        public static readonly string labelPasswordNormal = "labelPasswordNormal";
        public static readonly string labelPasswordStrong = "labelPasswordStrong";
        public static readonly string textForgotLabel = "textForgotLabel";

        // Home/Open Wallet - Common
        public static readonly string viewWelcomeHome = "viewWelcomeHome";
        public static readonly string buttonCreateWallet = "buttonCreateWallet";
        public static readonly string buttonRestoreWallet = "buttonRestoreWallet";

        // Home/Open Wallet - viewHome
        public static readonly string labelWelcomeToGluwa = "labelWelcomeToGluwa";
        public static readonly string labelWelcomeDescription = "labelWelcomeDescription";

        // Home/Open Wallet - viewCreateWallet
        public static readonly string labelPickAPassword = "labelPickAPassword";
        public static readonly string labelSoNoOneElseButYouCan = "labelSoNoOneElseButYouCan";
        public static readonly string labelWellDone = "labelWellDone";
        public static readonly string labelYourWalletIsStored = "labelYourWalletIsStored";
        public static readonly string textSkip = "textSkip";
        public static readonly string labelPrepareToWrite = "labelPrepareToWrite";
        public static readonly string labelIfYourDeviceGetsLost = "labelIfYourDeviceGetsLost";
        public static readonly string buttonBackupMyWallet = "buttonBackupMyWallet";
        public static readonly string buttonStart = "buttonStart";

        // Home/Open Wallet - viewGeneratedRecoveryPhrase
        public static readonly string labelBackupRecoveryPhrase = "labelBackupRecoveryPhrase";
        public static readonly string labelRecoveryPhraseHasBeenGenerated = "labelRecoveryPhraseHasBeenGenerated";
        public static readonly string labelWriteDownTheseWords = "labelWriteDownTheseWords";
        public static readonly string textMnemonic = "textMnemonic";
        public static readonly string labelMnemonicValidate1 = "labelMnemonicValidate1";
        public static readonly string labelMnemonicValidate2 = "labelMnemonicValidate2";
        public static readonly string labelMnemonicValidate3 = "labelMnemonicValidate3";
        public static readonly string labelLetsDoubleCheck = "labelLetsDoubleCheck";
        public static readonly string textSelect1 = "textSelect1";
        public static readonly string textSelect2 = "textSelect2";
        public static readonly string textSelect3 = "textSelect3";
        public static readonly string textSelect4 = "textSelect4";

        // Home/Open Wallet - viewWalletBackedUp
        public static readonly string labelYourWalletIsBackedUp = "labelYourWalletIsBackedUp";
        public static readonly string labelProtectYourRecoveryPhrase = "labelProtectYourRecoveryPhrase";
        public static readonly string buttonContinue = "buttonContinue";

        // Home/Open Wallet - viewRestoreWallet
        public static readonly string labelRestoreYourWallet = "labelRestoreYourWallet";
        public static readonly string labelIfYouCreatedaBackup = "labelIfYouCreatedaBackup";

        // Home/Open Wallet - viewRecoveryPhrase
        public static readonly string labelEnterRecoveryPhrase = "labelEnterRecoveryPhrase";
        public static readonly string labelMnemonic = "labelMnemonic";

        // Home/Open Wallet - viewRestoreWallet
        public static readonly string labelPickANewPassword = "labelPickANewPassword";

        // Home/Open Wallet - viewOpenWalletHome
        public static readonly string viewOpenGluwaWallet = "viewOpenGluwaWallet";
        public static readonly string textForgotPassword = "textForgotPassword";
        public static readonly string buttonOpenWallet = "buttonOpenWallet";

        // Home/Open Wallet - viewRecovery
        public static readonly string labelForgotPassword = "labelForgotPassword";
        public static readonly string labelUnfortunately = "labelUnfortunately";
        public static readonly string labelYouCanRestoreYourWallet = "labelYouCanRestoreYourWallet";

        // Dashboard - Common
        public static readonly string textHeader = "textHeader";
        public static readonly string labelMenu = "labelMenu";
        public static readonly string labelAddress = "labelAddress";
        public static readonly string labelYouSend = "labelYouSend";
        public static readonly string labelYouSendAmount = "labelYouSendAmount";
        public static readonly string labelFee = "labelFee";
        public static readonly string labelFeeAmount = "labelFeeAmount";
        public static readonly string labelConverted = "labelConverted";
        public static readonly string labelConvertedAmount = "labelConvertedAmount";
        public static readonly string labelAmount = "labelAmount";
        public static readonly string labelAmountsUSDCG = "labelAmountSUSDCG";
        public static readonly string labelTransactionID = "labelTransactionID";
        public static readonly string labelTransactionIDAddress = "labelTransactionIDAddress";
        public static readonly string labelSendersAddress = "labelSendersAddress";
        public static readonly string labelSendersAddressDetail = "labelSendersAddressDetail";
        public static readonly string labelEstimatedExchangeRate = "labelEstimatedExchangeRate";
        public static readonly string labelExtimatedExchangeRateAmount = "labelExtimatedExchangeRateAmount";
        public static readonly string labelYouGet = "labelYouGet";
        public static readonly string labelYouGetDetail = "labelYouGetDetail";
        public static readonly string labelExchangedAmount = "labelExchangedAmount";
        public static readonly string labelTotal = "labelTotal";
        public static readonly string labelTotalDetail = "labelTotalDetail";
        public static readonly string labelStatus = "labelStatus";
        public static readonly string labelStatusDetail = "labelStatusDetail";
        public static readonly string labelQuoteAcceptedText = "labelQuoteAcceptedText";
        public static readonly string labelABlockchainTransactionNeeds = "labelABlockchainTransactionNeeds";
        public static readonly string buttonConfirmDisabled = "buttonConfirmDisabled";
        public static readonly string buttonStatusInfo = "buttonStatusInfo";
        public static readonly string buttonAccept = "buttonAccept";
        public static readonly string buttonTransactionIDInfo = "buttonTransactionIDInfo";
        public static readonly string buttonSendersAddressCopy = "buttonSendersAddressCopy";

        // Dashboard - Common - BottomNav
        public static readonly string labelSend = "labelSend";
        public static readonly string labelHistory = "labelHistory";
        public static readonly string labelExchange = "labelExchange";

        // Dashboard - Common - HeaderBar
        public static readonly string buttonSandboxMode = "buttonSandboxMode";
        public static readonly string buttonNotification = "buttonNotification";

        public static readonly string viewQrScan = "viewQrScan";
        public static readonly string buttonQrScan = "buttonQrScan";
        public static readonly string buttonImagePicker = "buttonImagePicker";

        // Dashboard - Common - viewMyWallets
        public static readonly string viewMyWallet = "viewMyWallet";
        public static readonly string labelMyWallet = "labelMyWallet";
        public static readonly string labelUsdcGluwacoin = "USDC Gluwacoin";
        public static readonly string labelUsdcGluwacoinAmount = "USDC Gluwacoin amount";
        public static readonly string labelUsdcGluwacoinAddress = "USDC Gluwacoin address";
        public static readonly string labelsUsdcGluwacoin = "Sidechain USDC Gluwacoin";
        public static readonly string labelsUsdcGluwacoinAmount = "Sidechain USDC Gluwacoin amount";
        public static readonly string labelsUsdcGluwacoinAddress = "Sidechain USDC Gluwacoin address";
        public static readonly string labelUnderstood = "labelUnderstood";
        public static readonly string labelGluwaHasJustAddedCTC1 = "labelGluwaHasJustAddedCTC1";
        public static readonly string labelCreditcoin = "Creditcoin (ERC-20)";
        public static readonly string labelCreditcoinAmount = "Creditcoin (ERC-20) amount";
        public static readonly string labelCreditcoinAddress = "Creditcoin (ERC-20) address";
        public static readonly string labelsNgnGluwacoin = "Sidechain NGN Gluwacoin";
        public static readonly string labelsNgnGluwacoinAmount = "Sidechain NGN Gluwacoin amount";
        public static readonly string labelsNgnGluwacoinAddress = "Sidechain NGN Gluwacoin address";
        public static readonly string labelBitcoin = "Bitcoin";
        public static readonly string labelBitcoinAmount = "Bitcoin amount";
        public static readonly string labelBitcoinAddress = "Bitcoin address";
        public static readonly string labelAddressCopied = "Address copied";

        public static readonly string labelUsdGluwacoin = "USD Gluwacoin";
        public static readonly string labelKrwGluwacoin = "KRW Gluwacoin";

        /*
        public static readonly string labelNgnGluwacoin = "NGN Gluwacoin";
        public static readonly string labelsKrwcGluwacoin = "Sidechain KRWC Gluwacoin"; // to be update
        */
        public static readonly string buttonMyWallet = "buttonMyWallet";

        // Dashboard - viewSend
        public static readonly string viewSend = "viewSend";
        public static readonly string labelSendAmount = "labelSendAmount";
        public static readonly string viewSendPassword = "viewSendPassword";

        // Dashboard - viewSendPreview
        public static readonly string viewSendPreview = "viewSendPreview";
        public static readonly string labelReceiverAddress = "labelReceiverAddress";
        public static readonly string labelReceiverAddressValue = "labelReceiverAddressValue";
        public static readonly string labelEnvironment = "labelEnvironment";
        public static readonly string labelEnvironmentValue = "labelEnvironmentValue";
        public static readonly string buttonInfo = "buttonInfo";
        public static readonly string labelCouldNotRetrieve = "labelCouldNotRetrieve";
        public static readonly string labelSynchronizing = "labelSynchronizing";
        public static readonly string labelIfYouThink = "labelIfYouThink";

        // Dashboard - viewSendToAddress
        public static readonly string labelReceiversAddress = "labelReceiversAddress";
        public static readonly string labelEnterAddress = "labelEnterAddress";
        public static readonly string labelErrorOnSendToAddress = "labelErrorOnSendToAddress";
        public static readonly string labelMustStartWith0x = "labelMustStartWith0x";
        public static readonly string labelMustStartWithCorrectPrefix = "labelMustStartWithCorrectPrefix";
        public static readonly string labelCannotSendToSelf = "labelCannotSendToSelf";
        public static readonly string labelMustBe42Chars = "labelMustBe42Chars";

        // Dashboard - viewAddress
        public static readonly string labelShare = "labelShare";
        public static readonly string labelFromAddress = "labelFromAddress";

        // Dashboard - viewTransactionHistory
        public static readonly string labelTransactionList = "labelTransactionList";
        public static readonly string labelTransactionHistory = "labelTransactionHistory";

        // Dashboard - viewTransactionDetail
        public static readonly string viewTransactionDetail = "viewTransactionDetail";
        public static readonly string labelCreatedDate = "labelCreatedDate";
        public static readonly string labelCreatedDateDetail = "labelCreatedDateDetail";
        public static readonly string labelConfirmedDate = "labelConfirmedDate";
        public static readonly string labelConfirmedDateDetail = "labelConfirmedDateDetail";
        public static readonly string labelAmountDetail = "labelAmountDetail";

        // Dashboard - viewSendTransactionSuccess
        public static readonly string viewTransactionStatus = "viewTransactionStatus";
        public static readonly string labelTransactionStatus = "labelTransactionStatus";
        public static readonly string labelTotalAmount = "labelTotalAmount";
        public static readonly string labelBlockchainText = "labelBlockchainText";

        // Dashboard - viewExchange
        public static readonly string viewExchange = "viewExchange";
        public static readonly string labelExchangeSourceCurrency = "labelExchangeSourceCurrency";
        public static readonly string labelExchangeTargetCurrency = "labelExchangeTargetCurrency";
        public static readonly string sourceIcon = "sourceIcon";
        public static readonly string targetIcon = "targetIcon";
        public static readonly string labelExchangeAmount = "labelExchangeAmount";
        public static readonly string labelBalanceAndCurrency = "labelBalanceAndCurrency";
        
        public static readonly string labelsUsdg = "sUSDC-G";         
        public static readonly string labelBtc = "BTC";

        // Dashboard - viewExchangePassword
        public static readonly string labelEnterWalletPassword = "labelEnterWalletPassword";

        // Dashboard - viewQuoteCreateFail/viewQuoteAcceptFail
        public static readonly string labelTitleQuoteCreateFail = "labelTitleQuoteCreateFail";
        public static readonly string labelTitleQuoteAcceptFail = "labelTitleQuoteAcceptFail"; 
        public static readonly string labelTitle = "labelTitle";
        public static readonly string labelQuoteTitle = "labelQuoteTitle";
        public static readonly string labelQuoteDetail = "labelQuoteDetail";
        public static readonly string labelMessage = "labelMessage";
        public static readonly string labelQuoteMessage = "labelQuoteMessage";

        // Dashboard - viewQuoteCreateSuccess
        public static readonly string labelEstimatedExchangeRateAmount = "labelEstimatedExchangeRateAmount";
        public static readonly string labelQuoteCreatedText = "labelQuoteCreatedText";
        public static readonly string buttonAcceptDisabled = "buttonAcceptDisabled";

        // Dashbaord - viewInvest
        public static readonly string labelInvest = "labelInvest";
        public static readonly string viewPortfolioDashboard = "viewPortfolioDashboard";
        public static readonly string labelPortfolioDashboard = "labelPortfolioDashboard";
        public static readonly string labelPortfolioDashboardAccount = "labelPortfolioDashboardAccount";
        public static readonly string labelValueDayChange = "labelValueDayChange";
        public static readonly string viewAvailableNow = "viewAvailableNow";
        public static readonly string labelAvailableNow = "labelAvailableNow";
        public static readonly string labelInvestDescription1 = "labelInvestDescription1";
        public static readonly string labelInvestDescription2 = "labelInvestDescription2";
        public static readonly string labelInvestDescription3 = "labelInvestDescription3";
        public static readonly string viewTotalValue = "viewTotalValue";
        public static readonly string labelTotalValue = "labelTotalValue";
        public static readonly string labelTotalDeposits = "labelTotalDeposits";
        public static readonly string labelInterestEarned = "labelInterestEarned";
        public static readonly string labelCurrentEffectiveAPY = "labelCurrentEffectiveAPY";
        public static readonly string labelAccountSetupStepsCompleted = "labelAccountSetupStepsCompleted";
        public static readonly string labelIdentityVerification = "labelTitleIdentity Verification";
        public static readonly string labelPersonalInformation = "labelTitlePersonal Information";
        public static readonly string labelProofOfAddress = "labelTitleProof of Address (Optional)";
        public static readonly string labelTermsAndConditionsAgreement = "labelTitleTerms and Conditions Agreement";
        public static readonly string labelOneTimePasswordWitnessVerification = "labelTitleOne-Time Password Witness Verification";
        public static readonly string labelPersonalInformationNotSubmitted = "labelPersonal Information NotSubmitted";
        public static readonly string labelProofOfAddressNotSubmitted = "labelProof of Address (Optional) NotSubmitted";
        public static readonly string labelProofOfAddressSubmitted = "labelProof of Address (Optional) Submitted";
        public static readonly string labelProofOfAddressFailed = "labelProof of Address (Optional) Failed";
        public static readonly string labelVeriffStatusForProofOfAddress = "labelVeriffStatusForProofOfAddress";
        public static readonly string labelTermsAndConditionsAgreementNotSubmitted = "labelTerms and Conditions Agreement NotSubmitted";
        public static readonly string labelOneTimePasswordWitnessVerificationNotSubmitted = "labelOne-Time Password Witness Verification NotSubmitted";
        public static readonly string viewAddressDocumentRequired = "viewAddressDocumentRequired";
        public static readonly string labelAddressDocumentRequired = "labelAddressDocumentRequired";
        public static readonly string labelDocumentRequired = "labelDocumentRequired";
        public static readonly string labelAddressDocumentRequiredParagraph1_1 = "labelAddressDocumentRequiredParagraph1_1";
        public static readonly string labelAddressDocumentRequiredParagraph1_2 = "labelAddressDocumentRequiredParagraph1_2";
        public static readonly string labelAddressDocumentRequiredParagraph2_1 = "labelAddressDocumentRequiredParagraph2_1";
        public static readonly string buttonOK = "buttonOK";
        public static readonly string buttonUpload = "buttonUpload";
        public static readonly string buttonSkip = "buttonSkip";
        public static readonly string viewImportant = "viewImportant";
        public static readonly string labelImportant = "labelImportant";
        public static readonly string labelPleaseDocumentsLinkedParagraph1 = "labelPleaseDocumentsLinkedParagraph1";
        public static readonly string labelPleaseDocumentsLinkedParagraph2 = "labelPleaseDocumentsLinkedParagraph2";
        public static readonly string labelPleaseDocumentsLinkedParagraph7_1 = "labelPleaseDocumentsLinkedParagraph7_1";
        public static readonly string buttonIAgree = "buttonIAgree";
        public static readonly string viewGrabFriendSpousePhone = "viewGrabFriendSpousePhone";
        public static readonly string labelGrabFriendSpousePhone = "labelGrabFriendSpousePhone";
        public static readonly string labelGrabFriendSpousePhoneOrderItem1 = "labelGrabFriendSpousePhoneOrderItem1";
        public static readonly string labelGrabFriendSpousePhoneOrderItem2 = "labelGrabFriendSpousePhoneOrderItem2";
        public static readonly string labelGrabFriendSpousePhoneOrderItem3 = "labelGrabFriendSpousePhoneOrderItem3";
        public static readonly string labelWitnessName = "labelWitnessName";
        public static readonly string labelCountryCode = "labelCountryCode";
        public static readonly string labelWitnessMobilePhone = "labelWitnessMobilePhone";
        public static readonly string labelRequestOneTimePassword = "labelRequestOneTimePassword";
        public static readonly string labelEnterVerificationCode = "labelEnterVerificationCode";
        public static readonly string labelEnterVerificationCodeParagraph1 = "labelEnterVerificationCodeParagraph1";
        public static readonly string buttonSubmit = "buttonSubmit";
        public static readonly string labelYourRequestOTPVerifyHasFailed = "labelYourRequestOTPVerifyHasFailed";
        public static readonly string labelBond = "labelBond";
        public static readonly string labelAuthenticationIncomplete = "labelAuthenticationIncomplete";
        public static readonly string labelSavings = "labelSavings";
        public static readonly string labelResubmit = "labelResubmit";
        public static readonly string labelDeclined = "labelDeclined";
        public static readonly string labelApproved = "labelApproved";
        public static readonly string labelEarnApy = "labelEarnApy";
        public static readonly string labelNotLoaded = "labelNotLoaded";
        public static readonly string viewBond = "viewBond";
        public static readonly string labelBondDescription1 = "labelBondDescription1";
        public static readonly string labelBondDescription2 = "labelBondDescription2";
        public static readonly string labelSavingsDescription1 = "labelSavingsDescription1";
        public static readonly string labelSavingsDescription2 = "labelSavingsDescription2";
        public static readonly string viewPrizeLinked = "viewPrizeLinked";
        public static readonly string labelPrizeLinkedDescription1 = "labelPrizeLinkedDescription1";
        public static readonly string labelPrizeLinkedDescription2 = "labelPrizeLinkedDescription2";
        public static readonly string labelOnAverage = "labelOnAverage";
        public static readonly string labelOnAveragePlus = "labelOnAveragePlus";
        public static readonly string viewWellNeedALittleMoreInformationFirst = "viewWellNeedALittleMoreInformationFirst";
        public static readonly string labelWellNeedALittleMoreInformationFirst = "labelWellNeedALittleMoreInformationFirst";
        public static readonly string labelYourInformationWillBeKeptPrivateAndStored = "labelYourInformationWillBeKeptPrivateAndStored";
        public static readonly string labelFirstName = "labelFirstName";
        public static readonly string labelLastName = "labelLastName";
        public static readonly string labelBirthDate = "labelBirthDate";
        public static readonly string labelAddressLine1 = "labelAddressLine1";
        public static readonly string labelAddressLine2 = "labelAddressLine2";
        public static readonly string labelCity = "labelCity";
        public static readonly string labelProvince = "labelProvince";
        public static readonly string labelPostalCode = "labelPostalCode";
        public static readonly string labelCountryIsoCode = "labelCountryIsoCode";
        public static readonly string labelNationality = "labelNationality";
        public static readonly string labelBirthPlace = "labelBirthPlace";
        public static readonly string labelIdCardNo = "labelIdCardNo";
        public static readonly string labelOccupation = "labelOccupation";
        public static readonly string labelSourceOfFunds = "labelSourceOfFunds";
        public static readonly string labelInvalidCode = "labelInvalidCode";
        public static readonly string labelValidCode = "labelValidCode";
        public static readonly string viewNiceOne = "viewNiceOne";
        public static readonly string labelNiceOne = "labelNiceOne";
        public static readonly string labelNiceOneParagraph1 = "labelNiceOneParagraph1";
        public static readonly string labelNiceOneParagraph2 = "labelNiceOneParagraph2";

        // Invest - Deposit
        public static readonly string viewTransfer = "viewTransfer";
        public static readonly string labelTransfer = "labelTransfer";
        public static readonly string viewDeposit = "viewDeposit";
        public static readonly string labelDeposit = "labelDeposit";
        public static readonly string labelMax = "labelMax";
        public static readonly string viewPleaseDocumentsLinked = "viewPleaseDocumentsLinked";
        public static readonly string labelPleaseDocumentsLinked = "labelPleaseDocumentsLinked";
        public static readonly string labelPleaseDocumentsLinkedParagraph8 = "labelPleaseDocumentsLinkedParagraph8";
        public static readonly string viewTransactionPreview = "viewTransactionPreview";
        public static readonly string labelTotalsUSDCG = "labelTotalsUSDCG";
        public static readonly string labelTransferFromOnPreview = "labelTransferFromOnPreview";
        public static readonly string viewTransactionResult = "viewTransactionResult";
        public static readonly string labelPending = "labelPending";
        public static readonly string labelDepositSubmitDescription = "labelDepositSubmitDescription";
        public static readonly string labelFailed = "labelFailed";
        public static readonly string buttonTransaction = "buttonTransaction";
        public static readonly string buttonTransactions = "buttonTransactions";
        public static readonly string buttonDone2 = "buttonDone2";
        public static readonly string viewTransactionsHistory = "viewTransactionsHistory";
        public static readonly string labelTransactionsHistory = "labelTransactionsHistory";
        public static readonly string labelNoList = "labelNoList";
        public static readonly string labelDateTime = "labelDateTime";
        public static readonly string viewBondAccDeposit = "viewBondAccDeposit";
        public static readonly string labelTransferFrom = "labelTransferFrom";
        public static readonly string labelTransactionType = "labelTransactionType";
        public static readonly string labelDaysUntilMaturity = "labelDaysUntilMaturity";
        public static readonly string labellabelMaturityDate = "labellabelMaturityDate";
        public static readonly string labelTransactionCreated = "labelTransactionCreated";
        public static readonly string labelTransactionModified = "labelTransactionModified";
        public static readonly string labelTransactionReference = "labelTransactionReference";
        public static readonly string viewBondAccount = "viewBondAccount";
        public static readonly string labelAccount = "labelAccount";
        public static readonly string labelTotalBalance = "labelTotalBalance";
        public static readonly string labelAvailableToDrawdown = "labelAvailableToDrawdown";
        public static readonly string labelInterestAccrued = "labelInterestAccrued";
        public static readonly string labelEffectiveAPY = "labelEffectiveAPY";
        public static readonly string labelRecentTransactions = "labelRecentTransactions";
        public static readonly string labelBondAccDeposit = "labelBondAccDeposit";
        public static readonly string labelSavingAccDeposit = "labelSavingAccDeposit";
        public static readonly string buttonDrawdown = "buttonDrawdown";
        public static readonly string buttonDeposit = "buttonDeposit";
        public static readonly string labelDrawdownTransferTo = "labelDrawdownTransferTo";
        public static readonly string labelDrawdownAmount = "labelDrawdownAmount";

        // Invest - Deposit
        public static readonly string labelYouHaveAMatureBondAccountBalance = "labelYouHaveAMatureBondAccountBalance";
        public static readonly string labelDrawdownDescriptionParagraph1 = "labelDrawdownDescriptionParagraph1";
        public static readonly string labelDrawdownDescriptionParagraph2 = "labelDrawdownDescriptionParagraph2";
        public static readonly string labelAvailableAmount = "AvailableAmount";
        public static readonly string labelUnavailableLockedAmount = "labelUnavailableLockedAmount";
        public static readonly string labelAddressDocumentStatusRequired = "labelAddressDocumentStatusRequired";
        public static readonly string labelAddressDocumentStatusActionRequired = "labelAddressDocumentStatusActionRequired";
        public static readonly string labelAddressDocumentStatusReviewInProgress = "labelAddressDocumentStatusReviewInProgress";
        public static readonly string labelAddressDocumentStatusRequiredParagraph1 = "labelAddressDocumentStatusRequiredParagraph1";
        public static readonly string labelAddressDocumentStatusRequiredParagraph2_1 = "labelAddressDocumentStatusRequiredParagraph2_1";
        public static readonly string labelAddressDocumentStatusRequiredParagraph2_2 = "labelAddressDocumentStatusRequiredParagraph2_2";
        public static readonly string labelAddressDocumentStatusRequiredParagraph3 = "labelAddressDocumentStatusRequiredParagraph3";
        public static readonly string labelAddressDocumentStatusRequiredParagraph4 = "labelAddressDocumentStatusRequiredParagraph4";
        public static readonly string labelAddressDocumentStatusRequiredParagraph5 = "labelAddressDocumentStatusRequiredParagraph5";
        public static readonly string labelAddressDocumentStatusRequiredParagraph6 = "labelAddressDocumentStatusRequiredParagraph6";
        public static readonly string labelAddressDocumentStatusRequiredParagraph7 = "labelAddressDocumentStatusRequiredParagraph7";
        public static readonly string labelDrawdownHeader = "labelDrawdownHeader";
        public static readonly string labelDrawdownAddressTitle = "labelDrawdownAddressTitle";
        public static readonly string labelDrawdownAddressParagraph1 = "labelDrawdownAddressParagraph1";
        public static readonly string labelDrawdownAddressParagraph2 = "labelDrawdownAddressParagraph2";
        public static readonly string labelDrawdownTransactionPreview = "labelDrawdownTransactionPreview"; 
        public static readonly string labelNotSubmittedForPreDeposit = "labelNotSubmittedForPreDeposit";
        public static readonly string labelSubmittedForPreDeposit = "labelSubmittedForPreDeposit";
        public static readonly string labelFailedForPreDeposit = "labelFailedForPreDeposit";

        // Veriff
        public static readonly string viewVerificationComplete = "viewVerificationComplete";
        public static readonly string labelVerificationComplete = "labelVerificationComplete";
        public static readonly string labelVerifiedParagraph1 = "labelVerifiedParagraph1";
        public static readonly string labelVerifiedParagraph2 = "labelVerifiedParagraph2";
        public static readonly string labelVerifiedParagraph3 = "labelVerifiedParagraph3";
        public static readonly string labelVerifiedParagraph4 = "labelVerifiedParagraph4";
        public static readonly string viewVerificationPending = "viewVerificationPending";
        public static readonly string labelVerificationPending = "labelVerificationPending";
        public static readonly string labelVerificationCompleteParagraph1 = "labelVerificationCompleteParagraph1";
        public static readonly string labelVerificationCompleteParagraph2 = "labelVerificationCompleteParagraph2";
        public static readonly string labelVerificationCompleteParagraph3 = "labelVerificationCompleteParagraph3";
        public static readonly string labelVerificationCompleteParagraph4 = "labelVerificationCompleteParagraph4";
        public static readonly string viewVerificationFailed = "viewVerificationFailed";
        public static readonly string labelVerificationFailed = "labelVerificationFailed";
        public static readonly string labelVerificationFailedParagraph1 = "labelVerificationFailedParagraph1";
        public static readonly string labelVerificationFailedParagraph3 = "labelVerificationFailedParagraph3";
        public static readonly string labelVerificationFailedParagraph4 = "labelVerificationFailedParagraph4";
        public static readonly string viewVerificationResubmit = "viewVerificationResubmit";
        public static readonly string labelVerificationResubmit = "labelVerificationResubmit";
        public static readonly string labelVerificationResubmitParagraph1 = "labelVerificationResubmitParagraph1";
        public static readonly string labelVerificationResubmitParagraph2 = "labelVerificationResubmitParagraph2";
        public static readonly string labelTempParagraph4 = "labelTempParagraph4";
        public static readonly string viewIdentityVerificationRequired = "viewIdentityVerificationRequired";
        public static readonly string labelIdentityVerificationRequired = "labelIdentityVerificationRequired";
        public static readonly string labelOurInvestProductsRequireUsToVerify = "labelOurInvestProductsRequireUsToVerify";
        public static readonly string labelWeAreUnableToOfferThisProductToUS1 = "labelWeAreUnableToOfferThisProductToUS1";
        public static readonly string labelHere = "labelHere";
        public static readonly string labelWeAreUnableToOfferThisProductToUS2 = "labelWeAreUnableToOfferThisProductToUS2";
        public static readonly string labelSelectBeginToStart = "labelSelectBeginToStart";
        public static readonly string labelForBestResults1 = "labelForBestResults1";
        public static readonly string buttonBegin = "buttonBegin";

        // Dashboard - viewMenu
        public static readonly string viewMenu = "viewMenu";
        public static readonly string labelIdentification = "labelIdentification";
        public static readonly string labelSignature = "labelSignature";
        public static readonly string labelPrivate = "labelPrivate";
        public static readonly string labelAppearance = "labelAppearance";
        public static readonly string labelLanguage = "labelLanguage";
        public static readonly string labelSandbox = "labelSandbox";
        public static readonly string labelAddSecurity = "labelAddSecurity";
        public static readonly string labelSupport = "labelSupport";
        public static readonly string labelUserGuide = "labelUserGuide";
        public static readonly string labelPrivacyAndTerms = "labelPrivacyAndTerms";
        public static readonly string labelGluwaSite = "labelGluwaSite";
        public static readonly string labelResetWallet = "labelResetWallet";

        // Dashboard - viewIdentification
        public static readonly string viewIdentification = "viewIdentification";
        public static readonly string labelUserIdentification = "labelUserIdentification";
        public static readonly string labelVerifyYourIdentify = "labelVerifyYourIdentify";
        public static readonly string buttonSignup = "buttonSignup";
        public static readonly string buttonLogin = "buttonLogin";
        public static readonly string buttonForgotPassword = "buttonForgotPassword";
        public static readonly string labelInvalidLoginPassword = "labelInvalidLoginPassword";
        public static readonly string labelEnterLoginId = "labelEnterLoginId";
        public static readonly string labelEnterLoginPassword = "labelEnterLoginPassword";
        public static readonly string labelTransactions = "labelTransactions";
        public static readonly string buttonDrawdowns = "buttonDrawdowns";

        // Dashboard - viewSignature
        public static readonly string labelEnterMessage = "labelEnterMessage";
        public static readonly string labelYouCanSign = "labelYouCanSign";
        public static readonly string buttonSign = "buttonSign";

        // Dashboard - viewMessageSigned
        public static readonly string viewSignatureMessageSigned = "viewSignatureMessageSigned";
        public static readonly string labelMessageSigned = "labelMessageSigned";
        public static readonly string labelShareMessage = "labelShareMessage";
        public static readonly string textCurrencyAddress = "textCurrencyAddress";
        public static readonly string textCurrentAddress = "textCurrentAddress";
        public static readonly string textMessage = "textMessage";
        public static readonly string textUserMessage = "textUserMessage";
        public static readonly string textCopyMessage = "textCopyMessage";
        public static readonly string textSignature = "textSignature";
        public static readonly string textSignatureAddress = "textSignatureAddress";
        public static readonly string textCopySignature = "textCopySignature";
        public static readonly string buttonCopyAddress = "buttonCopyAddress";

        // Dashboard - viewPrivateKeysAndAddress
        public static readonly string viewPrivateKey = "viewPrivateKey";
        public static readonly string labelCurrencyUSDCG = "labelCurrencyUSDC-G";
        public static readonly string labelTitlePrivateKeyUSDCG = "labelTitlePrivateKeyUSDC-G";
        public static readonly string labelPrivateKeyUSDCG = "labelPrivateKeyUSDC-G";
        public static readonly string labelTitleAddressUSDCG = "labelTitleAddressUSDC-G";
        public static readonly string labelAddressUSDCG = "labelAddressUSDC-G";

        public static readonly string labelCurrencysUSDCG = "labelCurrencysUSDC-G";
        public static readonly string labelTitlePrivateKeysUSDCG = "labelTitlePrivateKeysUSDC-G";
        public static readonly string labelPrivateKeysUSDCG = "labelPrivateKeysUSDC-G";
        public static readonly string labelTitleAddresssUSDCG = "labelTitleAddresssUSDC-G";
        public static readonly string labelAddresssUSDCG = "labelAddresssUSDC-G";

        public static readonly string labelCurrencyCTC = "labelCurrencyCTC";
        public static readonly string labelTitlePrivateKeyCTC = "labelTitlePrivateKeyCTC";
        public static readonly string labelPrivateKeyCTC = "labelPrivateKeyCTC";
        public static readonly string labelTitleAddressCTC = "labelTitleAddressCTC";
        public static readonly string labelAddressCTC = "labelAddressCTC";

        public static readonly string labelCurrencysNGNG = "labelCurrencysNGN-G";
        public static readonly string labelTitlePrivateKeysNGNG = "labelTitlePrivateKeysNGN-G";
        public static readonly string labelPrivateKeysNGNG = "labelPrivateKeysNGN-G";
        public static readonly string labelTitleAddresssNGNG = "labelTitleAddresssNGN-G";
        public static readonly string labelAddresssNGNG = "labelAddresssNGN-G";

        public static readonly string labelCurrencyBTC = "labelCurrencyBTC";
        public static readonly string labelTitlePrivateKeyBTC = "labelTitlePrivateKeyBTC";
        public static readonly string labelPrivateKeyBTC = "labelPrivateKeyBTC";
        public static readonly string labelTitleAddressBTC = "labelTitleAddressBTC";
        public static readonly string labelAddressBTC = "labelAddressBTC";

        public static readonly string labelTitleTestnetPrivateKeyBTC = "labelTitleTestnetPrivateKeyBTC";
        public static readonly string labelTestnetPrivateKeyBTC = "labelTestnetPrivateKeyBTC";
        public static readonly string labelTitleTestnetAddressBTC = "labelTitleTestnetAddressBTC";
        public static readonly string labelTestnetAddressBTC = "labelTestnetAddressBTC";

        // Dashboard - viewAppearance
        public static readonly string labelUseDarkMode = "labelUseDarkMode";
        public static readonly string toggleDarkMode = "toggleDarkMode";

        // Dashboard - viewLanguage
        public static readonly string labelEnglish = "labelEnglish";
        public static readonly string labelKorean = "labelKorean";
        public static readonly string labelLanguageDescription = "labelLanguageDescription";

        // Dashboard - viewSandboxMode
        public static readonly string labelUseSandboxMode = "labelUseSandboxMode";
        public static readonly string labelSandboxDescription = "labelSandboxDescription";
        public static readonly string toggleSandbox = "toggleSandbox";

        // Dashboard - viewAdditionalSecurity
        public static readonly string labelUsePasscodeLock = "labelUsePasscodeLock";
        public static readonly string labelUseBiometry = "labelUseBiometry";
        public static readonly string labelChangePasscode = "labelChangePasscode";
        public static readonly string togglePasscode = "togglePasscode";
        public static readonly string toggleBiometrics = "toggleBiometrics";

        // Dashboard - viewNotification
        public static readonly string labelSummary = "labelSummary";
        public static readonly string labelResult = "labelResult";

        // Dashboard - viewNotificationDetail
        public static readonly string labelSummaryResultlabelSummaryDetail = "labelSummaryDetail";
        public static readonly string textDate = "textDate";
        public static readonly string labelDateDetail = "labelDateDetail";

        // Veriff page - Automation IDs from Veriff
        // Let's get you verifed
        public static readonly string toolbar_btn_close = "toolbar_btn_close";
        public static readonly string intro_title = "intro_title";
        public static readonly string intro_instruction = "intro_instruction";
        public static readonly string progress_item_label = "progress_item_label";
        public static readonly string intro_privacy_policy = "intro_privacy_policy";
        public static readonly string button_text = "button_text";

        // Select Issuing Country
        public static readonly string country_title = "country_title";
        public static readonly string country_instruction = "country_instruction";
        public static readonly string country_selected = "country_selected";

        // Select ID type
        public static readonly string document_title = "document_title";
        public static readonly string document_instruction = "document_instruction";
        public static readonly string identification_method_title = "identification_method_title";

        // Take a picture
        public static readonly string camera_capture = "camera_capture";

        // Thank you page
        public static readonly string complete_title = "complete_title";
        public static readonly string upload_finished_title = "upload_finished_title";
        public static readonly string upload_finished_description = "upload_finished_description";

        // Referral is removed on the app
        public static readonly string labelTempParagraph1 = "labelTempParagraph1";
        public static readonly string labelTempParagraph2 = "labelTempParagraph2";
        public static readonly string labelTempParagraph3 = "labelTempParagraph3";
        public static readonly string labelTempParagraph5 = "labelTempParagraph5";
        public static readonly string labelMyReferralCode = "labelMyReferralCode";
        public static readonly string labelReferralCodeCopied = "labelReferralCodeCopied";
        public static readonly string labelReferralStatus = "labelReferralStatus";
        public static readonly string labelWhatIsKYC = "labelWhatIsKYC";
        public static readonly string labelEnterReferralCode = "labelEnterReferralCode";
        public static readonly string labelCreatingReferralCode = "labelCreatingReferralCode";
        public static readonly string labelMustBe6Alphanumeric = "labelMustBe6Alphanumeric";
    }
}