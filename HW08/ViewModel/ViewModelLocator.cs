using GalaSoft.MvvmLight.Ioc;
using HW08.ViewModels;
using System;

namespace HW08.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditViewModel>();
        }

        public EditViewModel EditViewModel => SimpleIoc.Default.GetInstance<EditViewModel>(Guid.NewGuid().ToString());
        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>(Guid.NewGuid().ToString());
    }
}
