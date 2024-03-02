using System;
using System.Collections.Generic;
using HelixToolkit.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;

namespace import3D
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        private readonly ModelImporter import = new ModelImporter();//导入模型的类对象

        public readonly string ModelForder = "3D_Models";//模型文件夹

        // 创建一个字典来存储模型和它们的文件路径
        private Dictionary<Model3D, string> modelPaths = new Dictionary<Model3D, string>();

        public MainWindow()
        {
            InitializeComponent();
            ObjReader myHelixObjReader = new ObjReader();

            dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            //dlg.FileName = "DinoRider.3ds"; // Default file name
            //dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), ModelForder);
            dlg.RestoreDirectory = true;//记住上次打开的文件夹

            //读入模型文件
            //Model3DGroup MyModel = import.Load($@".\{ModelForder}\DinoRider.3ds");
            //// Display the model
            //model.Content = MyModel;
            //helixControl.ZoomExtents();
        }

        private void btnAdd3DModel_Click(object sender, RoutedEventArgs e)
        {
            var myModelPath = chooseModel();
            if (myModelPath.Count > 0)
            {
                foreach (var path in myModelPath)
                {
                    try
                    {
                        var MyModel = import.Load(path);
                        foreach (GeometryModel3D geometryModel in MyModel.Children)
                        {
                            // 创建一个新的ModelVisual3D对象，并将其Content属性设置为新加载的模型
                            ModelVisual3D modelVisual3D = new ModelVisual3D();
                            modelVisual3D.Content = geometryModel;

                            // 将新的ModelVisual3D对象添加到ModelVisual3D容器中
                            model.Children.Add(modelVisual3D);

                            // 将模型和它的文件路径添加到字典中
                            modelPaths[geometryModel] = path;
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"报错模型文件路径：{path},报错信息：{exception.Message}");
                    }
                }
                helixControl.ZoomExtents();
            }
        }

        private List<string> chooseModel()
        {
            List<string> filesPath = new List<string>();

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true && dlg.FileNames.Length > 0)
            {
                // Open document
                filesPath.AddRange(dlg.FileNames);
            }
            return filesPath;
        }

        private void HelixControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(helixControl);
            PointHitTestParameters hitParams = new PointHitTestParameters(mousePos);
            VisualTreeHelper.HitTest(helixControl, null, ResultCallback, hitParams);
        }

        private HitTestResultBehavior ResultCallback(HitTestResult result)
        {
            RayHitTestResult rayResult = result as RayHitTestResult;
            if (rayResult != null)
            {
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                if (rayMeshResult != null && modelPaths.ContainsKey(rayMeshResult.ModelHit as GeometryModel3D))
                {
                    string modelPath = modelPaths[rayMeshResult.ModelHit as GeometryModel3D];
                    string modelName = System.IO.Path.GetFileName(modelPath);
                    MessageBox.Show($"模型文件名：{modelName}");
                }
            }
            return HitTestResultBehavior.Stop;
        }

        private void btnClear3DModel_Click(object sender, RoutedEventArgs e)
        {
            model.Children.Clear();
            modelPaths.Clear();
        }
    }
}