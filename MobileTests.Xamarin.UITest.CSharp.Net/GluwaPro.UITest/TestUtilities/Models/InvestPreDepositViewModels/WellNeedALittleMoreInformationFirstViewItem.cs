using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class WellNeedALittleMoreInformationFirstViewItem
    {
        //To test localization
        public string TextWellNeedALittleMoreInformationFirst { get; private set; }
        public string TextYourInformationWillBeKeptPrivateAndStored { get; private set; }
        /* can not get default text from text field
        public string TextFirstName { get; private set; }
        public string TextLastName { get; private set; }
        public string TextBirthDate { get; private set; }
        public string TextAddressLine1 { get; private set; }
        public string TextAddressLine2 { get; private set; }
        public string TextCity { get; private set; }
        public string TextProvince { get; private set; }
        public string TextPostalCode { get; private set; }
        public string TextCountryIsoCode { get; private set; }
        public string TextNationality { get; private set; }
        public string TextBirthPlace { get; private set; }
        public string TextIdCardNo { get; private set; }
        public string TextOccupation { get; private set; }
        public string TextSourceOfFunds { get; private set; }
        public string TextContinue { get; private set; }
        */

        public WellNeedALittleMoreInformationFirstViewItem(
            string textWellNeedALittleMoreInformationFirst,
            string textYourInformationWillBeKeptPrivateAndStored
            /*,
            string textFirstName,
            string textLastName,
            string textBirthDate,
            string textAddressLine1,
            string textAddressLine2,
            string textCity,
            string textProvince,
            string textPostalCode,
            string textCountryIsoCode,
            string textNationality,
            string textBirthPlace,
            string textIdCardNo,
            string textOccupation,
            string textSourceOfFunds,
            string textContinue
            */
            )
        {
            TextWellNeedALittleMoreInformationFirst = textWellNeedALittleMoreInformationFirst;
            TextYourInformationWillBeKeptPrivateAndStored = textYourInformationWillBeKeptPrivateAndStored;
            /*
            TextFirstName = textFirstName;
            TextLastName = textLastName;
            TextBirthDate = textBirthDate;
            TextAddressLine1 = textAddressLine1;
            TextAddressLine2 = textAddressLine2;
            TextCity = textCity;
            TextProvince = textProvince;
            TextPostalCode = textPostalCode;
            TextCountryIsoCode = textCountryIsoCode;
            TextNationality = textNationality;
            TextBirthPlace = textBirthPlace;
            TextIdCardNo = textIdCardNo;
            TextOccupation = textOccupation;
            TextSourceOfFunds = textSourceOfFunds;
            TextContinue = textContinue;
            */
        }
    }
}
