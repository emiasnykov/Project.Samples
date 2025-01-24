using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class AccountSetupStepsCompletedViewItem
    {
        //To test localization
        public string TextAvailableToInvest { get; private set; }
        public string TextDescription { get; private set; }
        public string TextAccountSetupStepsCompleted { get; private set; }
        public string TextBond { get; private set; }
        public string TextSavings { get; private set; }

        public AccountSetupStepsCompletedViewItem(
            string textAvailableToInvest,
            string textDescription,
            string textAccountSetupStepsCompleted,
            string textBond,
            string textSavings
            )
        {
            TextAvailableToInvest = textAvailableToInvest;
            TextDescription = textDescription;
            TextAccountSetupStepsCompleted = textAccountSetupStepsCompleted;
            TextBond = textBond;
            TextSavings = textSavings;
        }
    }
}
