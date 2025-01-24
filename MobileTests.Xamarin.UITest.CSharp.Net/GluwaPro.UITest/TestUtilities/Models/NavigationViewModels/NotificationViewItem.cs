using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class NotificationViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextAmount { get; private set; }
        public string TextSummary { get; private set; }
        public string TextAddress { get; private set; }
        public string TextDateTime { get; private set; }
        public string TextResult { get; private set; }

        public NotificationViewItem(
            string textHeaderBar,
            string textAmount,
            string textSummary,
            string textAddress,
            string textDateTime,
            string textResult)
        {
            TextHeaderBar = textHeaderBar;
            TextAmount = textAmount;
            TextSummary = textSummary;
            TextAddress = textAddress;            
            TextDateTime = textDateTime;
            TextResult = textResult;
        }
    }
}
