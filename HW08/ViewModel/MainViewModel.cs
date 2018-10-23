using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HW08.Models;
using HW08.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HW08.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IEditWindowController EditWindowController { get; }
        public ObservableCollection<Contact> AllContacts { get; set; }
        private RelayCommand _addContactCommand;
        public ICommand AddContactCommand
        {
            get
            {
                if(_addContactCommand == null)
                {
                    _addContactCommand = new RelayCommand(AddContact);
                }
                return _addContactCommand;
            }
        }

        public Contact SelectedContact { get; set; }
        public IDataProvider DataProvider { get; }
        public bool CanAddContact { get { return true; } }
        public IDialogService DialogService { get; }

        public MainViewModel(IDataProvider dataProvider, IEditWindowController editWindowController, IDialogService dialogService)
        {
            DataProvider = dataProvider;
            EditWindowController = editWindowController;
            DialogService = dialogService;

        }

        private void AddContact()
        {
            var result = EditWindowController.ShowDialog(new EventArgs.OpenEditWindowArgs { Type = EventArgs.ActionType.Add });
            if(result.HasValue && result.Value)
            {
                AllContacts = new ObservableCollection<Contact>(DataProvider.GetAllContacts().OfType<Contact>());        
            }
        }
    }
}
