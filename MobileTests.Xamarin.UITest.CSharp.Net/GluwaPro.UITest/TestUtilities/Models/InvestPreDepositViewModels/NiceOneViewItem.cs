namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class NiceOneViewItem
    {
        //To test localization
        public string TextNiceOne { get; private set; }
        public string TextDescription { get; private set; }
        public string TextDone { get; private set; }

        public NiceOneViewItem(
            string textNiceOne,
            string textDescription,
            string textDone
            )
        {
            TextNiceOne = textNiceOne;
            TextDescription = textDescription;
            TextDone = textDone;
        }
    }
}
