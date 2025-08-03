using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
    }
}