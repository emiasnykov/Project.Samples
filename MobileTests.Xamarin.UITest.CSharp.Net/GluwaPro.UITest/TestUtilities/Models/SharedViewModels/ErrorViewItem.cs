namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class ErrorViewItem
    {
        //To test localization
        public string TextError { get; private set; }

        public ErrorViewItem(
            string textError)
        {
            TextError = textError;
        }
    }
}