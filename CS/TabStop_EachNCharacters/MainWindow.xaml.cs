using System;
using System.Drawing;
using System.Windows;

namespace TabStop_EachNCharacters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ribbonControl1.SelectedPage = myCommands;
            richEditControl1.InitializeDocument += richEditControl1_InitializeDocument;
            richEditControl1.CreateNewDocument();
            richEditControl1.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
        }

        void richEditControl1_InitializeDocument(object sender, EventArgs e)
        {
            DevExpress.XtraRichEdit.API.Native.Document document = richEditControl1.Document;
            document.BeginUpdate();
            try
            {
                document.DefaultCharacterProperties.FontName = "Courier New";
                document.DefaultCharacterProperties.FontSize = 10;
                document.Sections[0].Page.Width = DevExpress.Office.Utils.Units.InchesToDocumentsF(100);
            }
            finally
            {
                document.EndUpdate();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            #region #measuresinglelinestring
            DevExpress.XtraRichEdit.API.Native.Document document = richEditControl1.Document;
            SizeF tabSize = richEditControl1.MeasureSingleLineString(new String('w', 4), document.DefaultCharacterProperties);
            DevExpress.XtraRichEdit.API.Native.TabInfoCollection tabs = document.Paragraphs[0].BeginUpdateTabs(true);
            try
            {
                for (int i = 1; i <= 30; i++)
                {
                    DevExpress.XtraRichEdit.API.Native.TabInfo tab = new DevExpress.XtraRichEdit.API.Native.TabInfo();
                    tab.Position = i * tabSize.Width;
                    tabs.Add(tab);
                }
            }
            finally
            {
                document.Paragraphs[0].EndUpdateTabs(tabs);
            }
            #endregion #measuresinglelinestring
        }
    }
}
