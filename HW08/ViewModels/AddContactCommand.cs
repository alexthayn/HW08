using HW08.Models;
using System;
using System.Windows.Input;

namespace HW08.ViewModels
{
    public class AddContactCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var NewContact = new Contact();

            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Hello");
        }
    }
}
