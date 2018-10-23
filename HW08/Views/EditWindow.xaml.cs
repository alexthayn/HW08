using GalaSoft.MvvmLight.Messaging;
using HW08.EventArgs;
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
using System.Windows.Shapes;

namespace HW08.Views
{
    /// <summary>
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            Unloaded += EditWindow_Unloaded;
            Messenger.Default.Register<CloseWindowEventArgs>(this, CloseWindow);
        }

        private void CloseWindow(CloseWindowEventArgs obj)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void EditWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<CloseWindowEventArgs>(this);
        }
    }
}
