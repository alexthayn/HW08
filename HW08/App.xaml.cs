using GalaSoft.MvvmLight.Ioc;
using HW08.Services;
using HW08.Views;
using System.Windows;

namespace HW08
{
    public partial class App : Application
    {
        private void StartApplication(object sender, StartupEventArgs e)
        {
            SimpleIoc.Default.Register<IDataProvider, VCFDataProvider>();
            SimpleIoc.Default.Register<IEditWindowController, EditWindowController>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            App.Current.DispatcherUnhandledException += (s, args) =>
            {
                SimpleIoc.Default.GetInstance<IDialogService>().Exception(args.Exception);
                args.Handled = true;
            };

            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
