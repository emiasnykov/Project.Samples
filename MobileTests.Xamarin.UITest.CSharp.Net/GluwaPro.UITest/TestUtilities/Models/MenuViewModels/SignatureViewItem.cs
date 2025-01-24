using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class SignatureViewItem
    {
        //To test localization
        public string TextMessageSigned { get; private set; }
        public string TextShareMessage { get; private set; }
        public string TextCurrencyAddress { get; private set; }
        public string TextCurrentAddress { get; private set; }
        public string TextButtonCopyAddress { get; private set; }
        public string TextMessage { get; private set; }
        public string TextCopyMessage { get; private set; }
        public string TextSignature { get; private set; }
        public string TextCopySignature { get; private set; }
        public string TextButtonDone { get; private set; }

        public SignatureViewItem(
            string textMessageSigned,
            string textShareMessage,
            string textCurrencyAddress,
            string textCurrentAddress,
            string textButtonCopyAddress,
            string textMessage,
            string textCopyMessage,
            string textSignature,
            string textCopySignature,
            string textButtonDone)
        {
            TextMessageSigned = textMessageSigned;
            TextShareMessage = textShareMessage;
            TextCurrencyAddress = textCurrencyAddress;
            TextCurrentAddress = textCurrentAddress;
            TextButtonCopyAddress = textButtonCopyAddress;
            TextMessage = textMessage;
            TextCopyMessage = textCopyMessage;
            TextSignature = textSignature;
            TextCopySignature = textCopySignature;
            TextButtonDone = textButtonDone;
        }
    }
}
