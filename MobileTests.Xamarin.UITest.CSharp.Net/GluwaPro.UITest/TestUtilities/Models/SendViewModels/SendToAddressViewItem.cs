using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    class SendToAddressViewItem
    {
        //To test localization
        public string TextEnterAddress { get; private set; }
        public string TextButtonNext { get; private set; }

        // To get values
        public bool IsButtonNextEnabled { get; private set; }

        public SendToAddressViewItem(
            string textEnterAddress,
            string textButtonNext)
        {
            TextEnterAddress = textEnterAddress;
            TextButtonNext = textButtonNext;
        }
    }
}
