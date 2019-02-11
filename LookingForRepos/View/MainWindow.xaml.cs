using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace LookingForRepos.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new ViewModel.ViewModelMain();
            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
            viewModel.M_ExitEvent += ViewModelOnMExitEvent;
            ContentRendered += viewModel.OnContentRendered;
        }

        private void ViewModelOnMExitEvent()
        {
            Close();
        }

        private void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink)e.OriginalSource).NavigateUri;
            Trace.WriteLine("Browsing to " + destination);

            using (Process browser = new Process())
            {
                browser.StartInfo = new ProcessStartInfo
                {
                    FileName = destination.ToString(),
                    UseShellExecute = true,
                    ErrorDialog = true
                };
                browser.Start();
            }
        }
    }
}
