using HelixToolkit.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Media;
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

            // 创建一个ModelImporter对象
            var importer = new ModelImporter();

            // 使用ModelImporter对象加载.obj文件
            Model3D model3D_1 = importer.Load(@"D:\Workspace\Src\WPF\3D\Helix-Toolkit\Models\obj\tour.obj");
            Model3D model3D_2 = importer.Load(@"D:\Workspace\Src\WPF\3D\Helix-Toolkit\Models\obj\bunny\bunny.obj");
            Model3D model3D_3 = importer.Load(@"D:\Workspace\Src\WPF\3D\Helix-Toolkit\Models\obj\CornellBox-Glossy\CornellBox-Glossy.obj");
            Model3D model3D_4 = importer.Load(@"D:\Workspace\Src\WPF\3D\Helix-Toolkit\Models\obj\CornellBox-Glossy\Test\CornellBox-Glossy.obj");

            // 创建一个ModelVisual3D对象，并将其Content属性设置为加载的模型
            ModelVisual3D modelVisual3D = new ModelVisual3D();
            modelVisual3D.Content = model3D_4;

            ModelVisual3D modelVisual3D_1 = new ModelVisual3D();
            modelVisual3D_1.Content = model3D_2;

            // 将ModelVisual3D对象添加到ModelVisual3D容器中

            model.Children.Add(modelVisual3D);
            model.Children.Add(modelVisual3D_1);

            //// 创建一个3D模型组
            //Model3DGroup model3DGroup = new Model3DGroup();

            //// 创建一个3D模型。这里我们创建一个简单的立方体作为示例。
            //// 在实际应用中，你可能需要从文件中加载3D模型。
            //GeometryModel3D cube = new GeometryModel3D();
            //cube.Geometry = new MeshGeometry3D
            //{
            //    Positions = new Point3DCollection
            //    {
            //        new Point3D(-0.5, -0.5, 0.5),
            //        new Point3D(0.5, -0.5, 0.5),
            //        new Point3D(0.5, 0.5, 0.5),
            //        new Point3D(-0.5, 0.5, 0.5),
            //        new Point3D(-0.5, -0.5, -0.5),
            //        new Point3D(0.5, -0.5, -0.5),
            //        new Point3D(0.5, 0.5, -0.5),
            //        new Point3D(-0.5, 0.5, -0.5)
            //    },
            //    TriangleIndices = new Int32Collection
            //    {
            //        0, 1, 2, 2, 3, 0,
            //        4, 7, 6, 6, 5, 4,
            //        0, 4, 5, 5, 1, 0,
            //        1, 5, 6, 6, 2, 1,
            //        2, 6, 7, 7, 3, 2,
            //        3, 7, 4, 4, 0, 3
            //    }
            //};
            //cube.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red));

            //// 将3D模型添加到模型组中
            //model3DGroup.Children.Add(cube);

            //// 创建一个ModelVisual3D对象，并将其Content属性设置为模型组
            //ModelVisual3D modelVisual3D = new ModelVisual3D();
            //modelVisual3D.Content = model3DGroup;

            //// 将ModelVisual3D对象添加到ModelVisual3D容器中
            //model.Children.Add(modelVisual3D);

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