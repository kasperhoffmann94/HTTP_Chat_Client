using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HTTP_Chat_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller _controller;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateMessageReceivedTextBox(object sender, EventArgs e)
        {
            string message = sender.ToString();
            Dispatcher?.InvokeAsync(() => MessageReceived.AppendText(message));
            Dispatcher?.InvokeAsync(() => MessageReceived.AppendText(Environment.NewLine));
            Dispatcher?.InvokeAsync(MessageReceived.ScrollToEnd);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            string message = SendMessage.Text;

            _controller?.SendMessage(message);
        }

        private void ConnectButtonClick(object sender, RoutedEventArgs e)
        {
            string address = AddressBox.Text;
            int port = int.Parse(PortBox.Text);
            _controller = new Controller(address, port);

            _controller._responseEventHandler += UpdateMessageReceivedTextBox;

            _controller.SendMessage(UserNameBox.Text);

            SendButton.IsEnabled = true;
            ConnectButton.IsEnabled = false;
            UserNameBox.IsEnabled = false;
        }
    }
}