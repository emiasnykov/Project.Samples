using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class EachMenuViewItem
    {
        //To test localization
        public string TextTitleMenu { get; private set; }

        public EachMenuViewItem(string textTitleMenu)
        {
            TextTitleMenu = textTitleMenu;
        }
    }
}
