using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SharedViewModels
{
    class PageWithDescriptionAndOneButton
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextDescription { get; private set; }
        public string TextButton { get; private set; }

        public PageWithDescriptionAndOneButton(
            string textTitle,
            string textDescription,
            string textButton)
        {
            TextTitle = textTitle;
            TextDescription = textDescription;
            TextButton = textButton;
        }
    }
}
