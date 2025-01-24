using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    class QRCodeScannerViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextButtonBack { get; private set; }
        public string TextButtonImagePicker { get; private set; }

        public QRCodeScannerViewItem(
            string textHeaderBar,
            string textButtonBack,
            string textButtonImagePicker
            )
        {
            TextHeaderBar = textHeaderBar;
            TextButtonBack = textButtonBack;
            TextButtonImagePicker = textButtonImagePicker;
        }
    }
}
