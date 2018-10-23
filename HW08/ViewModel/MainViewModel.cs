using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HW08.EventArgs;
using HW08.Models;
using HW08.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace HW08.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IEditWindowController EditWindowController { get; }
        public ObservableCollection<Contact> AllContacts { get; set; }
        public RelayCommand AddContactCommand { get; set; }
        public RelayCommand<Contact> EditContactCommand { get; set; }
        public RelayCommand<Contact> DeleteContactCommand { get; set; }
        public RelayCommand ExitProgramCommand { get; set; }
        public RelayCommand ImportContactCommand { get; set; }

        public Contact SelectedContact { get; set; }
        public IDataProvider DataProvider { get; }
        public bool CanAddContact { get { return true; } }
        public IDialogService DialogService { get; }

        public MainViewModel(IDataProvider dataProvider, IEditWindowController editWindowController, IDialogService dialogService)
        {
            DataProvider = dataProvider;
            EditWindowController = editWindowController;
            DialogService = dialogService;

            AddContactCommand = new RelayCommand(AddContact);
            EditContactCommand = new RelayCommand<Contact>(EditContact, contact => SelectedContact != null);
            DeleteContactCommand = new RelayCommand<Contact>(DeleteContact, contact => SelectedContact != null);
            ExitProgramCommand = new RelayCommand(ExitProgram);
            ImportContactCommand = new RelayCommand(ImportContact);

            AllContacts = new ObservableCollection<Contact>(dataProvider.GetAllContacts().OfType<Contact>());
        }

        private void AddContact()
        {
            var result = EditWindowController.ShowDialog(new OpenEditWindowArgs { Type = ActionType.Add });
            if (result.HasValue && result.Value)
                AllContacts = new ObservableCollection<Contact>(DataProvider.GetAllContacts().OfType<Contact>());
        }

        private void DeleteContact(Contact contact)
        {
            AllContacts.Remove(contact);
            DataProvider.Delete(contact);
        }

        private void EditContact(Contact contact)
        {
            var result = EditWindowController.ShowDialog(new OpenEditWindowArgs { Type = ActionType.Edit, Contact = SelectedContact });
            if (result.HasValue && result.Value)
            {
                //remember the users selection
                int index = AllContacts.IndexOf(SelectedContact);
                AllContacts = new ObservableCollection<Contact>(DataProvider.GetAllContacts().OfType<Contact>());

                //re-selected the original item
                SelectedContact = AllContacts[index];
            }
        }

        private void ImportContact()
        {

        }

        private void ExitProgram()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
