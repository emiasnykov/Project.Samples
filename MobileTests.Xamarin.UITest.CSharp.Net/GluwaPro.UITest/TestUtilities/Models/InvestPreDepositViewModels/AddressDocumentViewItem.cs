namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class AddressDocumentViewItem
    {
        //To test localization
        public string TextAddressDocument { get; private set; }
        public string TextDescription { get; private set; }
        public string TextUpload { get; private set; }
        public string TextCountinue { get; private set; }

        public AddressDocumentViewItem(
            string textAddressDocument,
            string textDescription,
            string textUpload,
            string textCountinue
            )
        {
            TextAddressDocument = textAddressDocument;
            TextDescription = textDescription;
            TextUpload = textUpload;
            TextCountinue = textCountinue;
        }
    }
}
