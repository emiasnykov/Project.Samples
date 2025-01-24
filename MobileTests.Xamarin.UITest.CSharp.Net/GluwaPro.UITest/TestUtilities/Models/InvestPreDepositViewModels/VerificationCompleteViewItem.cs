using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class VerificationCompleteViewItem
    {
        //To test localization
        public string TextVerificationComplete { get; private set; }
        public string TextTempParagraph1 { get; private set; }
        public string TextTempParagraph2 { get; private set; }
        public string TextTempParagraph3 { get; private set; }
        public string TextVerifiedParagraph2 { get; private set; }
        public string TextVerifiedParagraph3 { get; private set; }
        public string TextVerifiedParagraph4 { get; private set; }
        public string TestDone { get; private set; }

        public VerificationCompleteViewItem(
            string textVerificationComplete,
            string textTempParagraph1,
            string textTempParagraph2,
            string textTempParagraph3,
            string textVerifiedParagraph2,
            string textVerifiedParagraph3,
            string textVerifiedParagraph4,
            string testDone
            )
        {
            TextVerificationComplete = textVerificationComplete;
            TextTempParagraph1 = textTempParagraph1;
            TextTempParagraph2 = textTempParagraph2;
            TextTempParagraph3 = textTempParagraph3;
            TextVerifiedParagraph3 = textVerifiedParagraph2;
            TextVerifiedParagraph3 = textVerifiedParagraph3;
            TextVerifiedParagraph4 = textVerifiedParagraph4;
            TestDone = testDone;
        }
    }
}
