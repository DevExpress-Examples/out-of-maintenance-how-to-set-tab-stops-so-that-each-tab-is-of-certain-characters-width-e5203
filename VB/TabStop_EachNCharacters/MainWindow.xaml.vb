Imports System
Imports System.Drawing
Imports System.Windows

Namespace TabStop_EachNCharacters

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.ribbonControl1.SelectedPage = Me.myCommands
            AddHandler Me.richEditControl1.InitializeDocument, AddressOf Me.richEditControl1_InitializeDocument
            Me.richEditControl1.CreateNewDocument()
            Me.richEditControl1.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft
        End Sub

        Private Sub richEditControl1_InitializeDocument(ByVal sender As Object, ByVal e As EventArgs)
            Dim document As DevExpress.XtraRichEdit.API.Native.Document = Me.richEditControl1.Document
            document.BeginUpdate()
            Try
                document.DefaultCharacterProperties.FontName = "Courier New"
                document.DefaultCharacterProperties.FontSize = 10
                document.Sections(0).Page.Width = DevExpress.Office.Utils.Units.InchesToDocumentsF(100)
            Finally
                document.EndUpdate()
            End Try
        End Sub

        Private Sub barButtonItem1_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
'#Region "#measuresinglelinestring"
            Dim document As DevExpress.XtraRichEdit.API.Native.Document = Me.richEditControl1.Document
            Dim tabSize As SizeF = Me.richEditControl1.MeasureSingleLineString(New [String]("w"c, 4), document.DefaultCharacterProperties)
            Dim tabs As DevExpress.XtraRichEdit.API.Native.TabInfoCollection = document.Paragraphs(0).BeginUpdateTabs(True)
            Try
                For i As Integer = 1 To 30
                    Dim tab As DevExpress.XtraRichEdit.API.Native.TabInfo = New DevExpress.XtraRichEdit.API.Native.TabInfo()
                    tab.Position = i * tabSize.Width
                    tabs.Add(tab)
                Next
            Finally
                document.Paragraphs(0).EndUpdateTabs(tabs)
            End Try
'#End Region  ' #measuresinglelinestring
        End Sub
    End Class
End Namespace
