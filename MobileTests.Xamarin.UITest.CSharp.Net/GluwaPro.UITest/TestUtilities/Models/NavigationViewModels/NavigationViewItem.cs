using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class NavigationViewItem
    {
        //To test localization
        public string TextSend { get; private set; }
        public string TextAddress { get; private set; }
        public string TextHistory { get; private set; }
        public string TextExchange { get; private set; }
        public string TextMenu { get; private set; }

        public NavigationViewItem(
            string textSend,
            string textAddress,
            string textHistory,
            string textExchange,
            string textMenu)
        {
            TextSend = textSend;
            TextAddress = textAddress;
            TextHistory = textHistory;
            TextExchange = textExchange;
            TextMenu = textMenu;
        }
    }
}
