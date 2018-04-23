using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InplaceImageEdit {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataSet dataSet = new DataSet();
            Assembly assembly = Assembly.GetExecutingAssembly();
            dataSet.ReadXmlSchema(assembly.GetManifestResourceStream("InplaceImageEdit.NWindCategoriesSchema.xml"));
            dataSet.ReadXml(assembly.GetManifestResourceStream("InplaceImageEdit.NWindCategories.xml"));
            grid.DataSource = dataSet.Tables[0].DefaultView;
        }
        private void ImageEditSettings_ConvertEditValue(DependencyObject sender, DevExpress.Xpf.Editors.ConvertEditValueEventArgs e) {
            using(MemoryStream stream = new MemoryStream()) {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)e.ImageSource));
                encoder.Save(stream);
                e.EditValue = stream.ToArray();
                e.Handled = true;
            }
        }
    }
}
