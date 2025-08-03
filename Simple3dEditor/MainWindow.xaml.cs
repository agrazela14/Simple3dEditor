using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simple3dEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // TODO: Create or find STL Reader library (Ideally quickly)
            //       Functionality to load STL files 
            //       Display the loaded file as a mesh via the System.Windows.Controls ViewPort3D
            //       Then
            //       Add rotation, move, and scale functionality
            //       Add plane slicing and merging functionality

            InitializeComponent();
        }

        private void FileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            // Load the STL Data from the STL Reader from the selected File
            (Vector3DCollection, Point3DCollection) geometry_data = STLReader.ReadSTLBinary(this.FileLocation.Text);
            Model3DGroup      load_group    = new Model3DGroup();
            GeometryModel3D   load_model    = new GeometryModel3D();
            ModelVisual3D     load_visual   = new ModelVisual3D();
            PerspectiveCamera load_camera   = new PerspectiveCamera();
            DirectionalLight  load_light    = new DirectionalLight();
            MeshGeometry3D    load_geometry = new MeshGeometry3D();
            DiffuseMaterial   load_material = new DiffuseMaterial();

            load_camera.Position = new Point3D(0, 0, 2);
            load_camera.LookDirection = new Vector3D(0, 0, -1);
            load_camera.FieldOfView = 60;

            this.main_view.Camera = load_camera;
            
            load_light.Color = Colors.White;
            load_light.Direction = new Vector3D(-0.61, -0.5, -0.61);

            load_group.Children.Add(load_light);

            load_geometry.Normals   = geometry_data.Item1 as Vector3DCollection;
            load_geometry.Positions = geometry_data.Item2 as Point3DCollection;
            load_model.Geometry     = load_geometry;

            load_material.Brush = new SolidColorBrush(Colors.LightSeaGreen);
            load_model.Material = load_material;

            load_group.Children.Add(load_model);
            load_visual.Content = load_group;
            
            this.main_view.Children.Add(load_visual);
        }
    }
}