using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class InvestDashbaordWithReferralCodeViewItem
    {
        //To test localization
        public string TextAvailableToInvest { get; private set; }
        public string TextInvestDescription { get; private set; }
        public string TextIdentityVerification { get; private set; }
        public string TextPersonalInformatione { get; private set; }

        public InvestDashbaordWithReferralCodeViewItem(
            string textAvailableToInvest,
            string textInvestDescription,
            string textIdentityVerification,
            string textPersonalInformatione
            )
        {
            TextAvailableToInvest = textAvailableToInvest;
            TextInvestDescription = textInvestDescription;
            TextIdentityVerification = textIdentityVerification;
            TextPersonalInformatione = textPersonalInformatione;
        }
    }
}
