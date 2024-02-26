using HelixToolkit.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace import3D
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ModelImporter import = new ModelImporter();//导入模型的类对象

        public readonly string ModelForder = "3D_Models";//模型文件夹

        public MainWindow()
        {
            InitializeComponent();
            ObjReader myHelixObjReader = new ObjReader();
            //读入模型文件
            Model3DGroup MyModel = import.Load($@".\{ModelForder}\DinoRider.3ds");
            // Display the model
            model.Content = MyModel;
            helixControl.ZoomExtents();
        }

        private void F_16_btn_Click(object sender, RoutedEventArgs e)
        {
            string myModelPath = chooseModel();
            if (string.Empty != myModelPath)
            {
                var MyModel = import.Load(myModelPath);
                model.Content = MyModel;
                helixControl.ZoomExtents();
            }
        }

        private string chooseModel()
        {
            string path = string.Empty;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), ModelForder);
            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                path = dlg.FileName;
            }
            return path;
        }
    }
}