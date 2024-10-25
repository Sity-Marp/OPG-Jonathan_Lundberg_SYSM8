using FitTrack.ViewModel;
using FitTrack.View;
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
using FitTrack.Services;

namespace FitTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //create instance of windowservice
            var windowService = new WindowService();

            //set the data context for main window to new instance of the viewmodel passing windowservice as a dependency
            DataContext = new MainWindowViewModel(windowService);
        }

    }
}