using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    public class EnterAddressViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextReceiverAddress { get; private set; }
        public string TextEnterAddress { get; private set; }
        public string TextButtonNext { get; private set; }

        // To get values
        public bool IsButtonNextEnabled { get; private set; }

        public EnterAddressViewItem(
            string textTitle,
            string textReceiverAddress,
            string textEnterAddress,
            string textButtonNext,

            bool isButtonNextEnabled)
        {
            TextTitle = textTitle;
            TextReceiverAddress = textReceiverAddress;
            TextEnterAddress = textEnterAddress;
            TextButtonNext = textButtonNext;

            IsButtonNextEnabled = isButtonNextEnabled;
        }
    }
}
