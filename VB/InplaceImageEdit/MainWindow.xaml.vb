Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Media.Imaging

Namespace InplaceImageEdit
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()
			Dim dataSet As New DataSet()
			Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
			dataSet.ReadXmlSchema(assembly.GetManifestResourceStream("NWindCategoriesSchema.xml"))
			dataSet.ReadXml(assembly.GetManifestResourceStream("NWindCategories.xml"))
			grid.DataSource = dataSet.Tables(0).DefaultView
		End Sub
		Private Sub ImageEditSettings_ConvertEditValue(ByVal sender As DependencyObject, ByVal e As DevExpress.Xpf.Editors.ConvertEditValueEventArgs)
			Using stream As New MemoryStream()
				Dim encoder As New JpegBitmapEncoder()
				encoder.Frames.Add(BitmapFrame.Create(CType(e.ImageSource, BitmapSource)))
				encoder.Save(stream)
				e.EditValue = stream.ToArray()
				e.Handled = True
			End Using
		End Sub
	End Class
End Namespace
