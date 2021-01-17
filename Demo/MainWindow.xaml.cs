using System.ComponentModel;
using System.Windows;

namespace Demo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ConnectionStatus.ConnectionStatus connectionStatus;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public ConnectionStatus.ConnectionStatus ConnectionStatus
        {
            get
            {
                return connectionStatus;
            }
            set
            {
                if (connectionStatus != value)
                {
                    connectionStatus = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectionStatus)));
                }
            }
        }

        private void Disconnected_Click(object sender, RoutedEventArgs e)
        {
            ConnectionStatus = global::ConnectionStatus.ConnectionStatus.Disconnected;
        }

        private void Connecting_Click(object sender, RoutedEventArgs e)
        {
            ConnectionStatus = global::ConnectionStatus.ConnectionStatus.Connecting;
        }

        private void Verifying_Click(object sender, RoutedEventArgs e)
        {
            ConnectionStatus = global::ConnectionStatus.ConnectionStatus.Verifying;
        }

        private void Connected_Click(object sender, RoutedEventArgs e)
        {
            ConnectionStatus = global::ConnectionStatus.ConnectionStatus.Connected;
        }
    }
}
