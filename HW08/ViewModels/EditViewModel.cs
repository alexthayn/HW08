using HW08.EventArgs;
using HW08.Models;
using HW08.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HW08.ViewModels
{
    public class EditViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contact> _sampleData;

        public ObservableCollection<Contact> SampleData { get; set; }

        private Contact _currentContact;
        public Contact CurrentContact
        {
            get
            {
                return _currentContact;
            }
            set
            {
                _currentContact = value;
                OnPropertyChanged(nameof(CurrentContact));
            }
        }

        public IDataProvider DataProvider { get; }
        public IDialogService DialogService { get; set; }
        public RelayCommand SaveDataCommand { get; set; }
        protected OpenEditWindowArgs Args { get; }

        public EditViewModel(OpenEditWindowArgs args, IDataProvider dataProvider, IDialogService dialogService)
        {
            Args = args;
            DataProvider = dataProvider;
            DialogService = dialogService;

            var c1 = new Contact { Id = Guid.NewGuid().ToString(), FirstName = "SpongeBob", LastName = "Squarepants", Birthday = new System.DateTime(2000, 3, 23), Company = "Krusty Krab", JobTitle = "Fry Cook", Email = "spongebob@squarepants.com", MobilePhone = "1234546430", Address = "Bikini Bottom" };
            var c2 = new Contact { Id = Guid.NewGuid().ToString(), FirstName = "Patrick", LastName = "Star", Birthday = new DateTime(2010, 3, 14), Company = "Unemployed", JobTitle = "none", Email = "patrick@star.com", MobilePhone = "1234567891", Address = "Under a rock" };
            SampleData = new ObservableCollection<Contact>();
            SampleData.Add(c1);
            SampleData.Add(c2);

            switch (args.Type)
            {
                case ActionType.Add:
                    CurrentContact = new Contact { Id = Guid.NewGuid().ToString() };
                    break;
                case ActionType.Edit:
                    //Clone a new object
                    CurrentContact = new Contact
                    {
                        Id = args.Contact.Id,
                        FirstName = args.Contact.FirstName,
                        LastName = args.Contact.LastName,
                        Company = args.Contact.Company,
                        Email = args.Contact.Email,
                        Birthday = args.Contact.Birthday,
                        JobTitle = args.Contact.JobTitle,
                        Notes = args.Contact.Notes,
                        MobilePhone = args.Contact.MobilePhone,
                        Address = args.Contact.Address
                    };
                    break;
            }

            SaveDataCommand = new RelayCommand(SaveData);
        }

        private void SaveData()
        {
            if (string.IsNullOrWhiteSpace(CurrentContact.FirstName))
            {
                DialogService.Warning("First Name is required");
                return;
            }

            bool result = false;
            switch (Args.Type)
            {
                case ActionType.Add:
                    result = DataProvider.Insert(CurrentContact);
                    break;
                case ActionType.Edit:
                    result = DataProvider.Update(CurrentContact);
                    break;
            }

            if (!result)
            {
                DialogService.Warning($"Error occured, save data failed");
            }
            else
            {
                Messenger.Default.Send(new CloseWindowEventArgs());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
