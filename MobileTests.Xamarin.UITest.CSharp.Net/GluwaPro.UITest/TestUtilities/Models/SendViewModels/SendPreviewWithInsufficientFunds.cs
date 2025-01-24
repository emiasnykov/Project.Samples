﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    class SendPreviewWithInsufficientFunds
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextReceiverAddress { get; private set; }
        public string TextEnvironment { get; private set; }
        public string TextYouSendAmount { get; private set; }
        public string TextFeeAmount { get; private set; }
        public string TextButtonInfo { get; private set; }
        public string TextTotalAmount { get; private set; }
        public string TextError { get; private set; }
        public string TextButtonNext { get; private set; }

        // To get values
        public string ReceiverAddress { get; private set; }
        public string Environment { get; private set; }
        public string YouSendAmount { get; private set; }
        public string FeeAmount { get; private set; }
        public string TotalBalance { get; private set; }

        public bool IsButtonNextEnabled { get; private set; }

        public SendPreviewWithInsufficientFunds(
            string textTitle,
            string textReceiverAddress,
            string textEnvironment,
            string textYouSendAmount,
            string textFeeAmount,
            string textButtonInfo,
            string textTotalAmount,
            string textButtonNext,
            string textError,

            string receiverAddress,
            string environment,
            string youSendAmount,
            string feeAmount,
            string totalBalance,
            bool isButtonNextEnabled)
        {
            TextTitle = textTitle;
            TextReceiverAddress = textReceiverAddress;
            TextEnvironment = textEnvironment;
            TextYouSendAmount = textYouSendAmount;
            TextFeeAmount = textFeeAmount;
            TextButtonInfo = textButtonInfo;
            TextTotalAmount = textTotalAmount;
            TextButtonNext = textButtonNext;
            TextError = textError;

            ReceiverAddress = receiverAddress;
            Environment = environment;
            YouSendAmount = youSendAmount;
            FeeAmount = feeAmount;
            TotalBalance = totalBalance;
            IsButtonNextEnabled = isButtonNextEnabled;
        }
    }
}
