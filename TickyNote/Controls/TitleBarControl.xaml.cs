using System.Windows;
using System.Windows.Controls;

namespace TickyNote.Controls
{
    public partial class TitleBarControl : UserControl
    {
        public TitleBarControl()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.ContextMenu != null)
            {
                button.ContextMenu.DataContext = DataContext;
                button.ContextMenu.IsOpen = true;
            }
        }
    }
}
