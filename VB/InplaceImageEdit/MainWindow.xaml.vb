Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Media.Imaging

Namespace InplaceImageEdit

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Dim dataSet As DataSet = New DataSet()
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            dataSet.ReadXmlSchema(assembly.GetManifestResourceStream("InplaceImageEdit.NWindCategoriesSchema.xml"))
            dataSet.ReadXml(assembly.GetManifestResourceStream("InplaceImageEdit.NWindCategories.xml"))
            Me.grid.DataSource = dataSet.Tables(0).DefaultView
        End Sub

        Private Sub ImageEditSettings_ConvertEditValue(ByVal sender As DependencyObject, ByVal e As DevExpress.Xpf.Editors.ConvertEditValueEventArgs)
            Using stream As MemoryStream = New MemoryStream()
                Dim encoder As JpegBitmapEncoder = New JpegBitmapEncoder()
                encoder.Frames.Add(BitmapFrame.Create(CType(e.ImageSource, BitmapSource)))
                encoder.Save(stream)
                e.EditValue = stream.ToArray()
                e.Handled = True
            End Using
        End Sub
    End Class
End Namespace
