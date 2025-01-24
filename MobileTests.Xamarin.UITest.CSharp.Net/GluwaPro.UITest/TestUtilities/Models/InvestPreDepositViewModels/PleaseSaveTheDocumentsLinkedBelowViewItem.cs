namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class PleaseSaveTheDocumentsLinkedBelowViewItem
    {
        //To test localization
        public string TextPleaseSaveTheDocumentsLinkedBelow { get; private set; }
        public string TextDescription { get; private set; }
        public string TextIAgree { get; private set; }

        public PleaseSaveTheDocumentsLinkedBelowViewItem(
            string textPleaseSaveTheDocumentsLinkedBelow,
            string textDescription,
            string textIAgree
            )
        {
            TextPleaseSaveTheDocumentsLinkedBelow = textPleaseSaveTheDocumentsLinkedBelow;
            TextDescription = textDescription;
            TextIAgree = textIAgree;
        }
    }
}
