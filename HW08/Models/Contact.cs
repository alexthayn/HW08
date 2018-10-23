using GalaSoft.MvvmLight;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HW08.Models
{
    public class Contact : ObservableObject, IContact, IDataErrorInfo, INotifyPropertyChanged
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private string _company;
        private string _jobTitle;
        string _mobilePhone;
        private DateTime _birthday;
        private string _email;
        private string _address;
        private string _notes;

        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        /// <summary>
        /// Get or set FirstName value
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                Set(ref _firstName, value);
                ValidateFirstName();
            }
        }

        /// <summary>
        /// Get or set LastName value
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                Set(ref _lastName, value);
                ValidateLastName();
            }
        }

        /// <summary>
        /// Get full name value from FirstName and LastName 
        /// </summary>
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        /// <summary>
        /// Get or set Company value
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { Set(ref _company, value); }
        }

        /// <summary>
        /// Get or set JobTitle value
        /// </summary>
        public string JobTitle
        {
            get { return _jobTitle; }
            set { Set(ref _jobTitle, value); }
        }

        /// <summary>
        /// Get or set MobilePhone value
        /// </summary>
        public string MobilePhone
        {
            get { return _mobilePhone; }
            set { Set(ref _mobilePhone, value); }
        }

        /// <summary>
        /// Get or set Birthday value
        /// </summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                Set(ref _birthday, value);
                ValidateBirthday();
            }
        }

        /// <summary>
        /// Get or set Email value
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                Set(ref _email, value);
                ValidateEmail();
            }
        }

        /// <summary>
        /// Get or set Address value
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { Set(ref _address, value); }
        }

        /// <summary>
        /// Get or set Notes value
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set { Set(ref _notes, value); }
        }

        //IDataErrorInfo Implementation
        public Dictionary<string, string> errors = new Dictionary<string, string>();
        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        //Validation methods
        private void ValidateFirstName()
        {
            if (FirstName == null || FirstName.Any(Char.IsWhiteSpace))
                errors[nameof(FirstName)] = "First name must be valid with no extra whitespace.";
            else
                errors[nameof(FirstName)] = null;

            OnPropertyChanged(nameof(FirstName));
        }

        private void ValidateLastName()
        {
            if (LastName == null || LastName.Any(Char.IsWhiteSpace))
                errors[nameof(LastName)] = "Last name must be valid with no extra whitespace.";
            else
                errors[nameof(LastName)] = null;
            OnPropertyChanged(nameof(LastName));
        }

        private void ValidateBirthday()
        {
            if (Birthday > System.DateTime.Today || Birthday < DateTime.Now.AddYears(-150))
                errors[nameof(Birthday)] = "Please enter a valid birthdate between now and the last 150 years.";
            else
                errors[nameof(Birthday)] = null;

            OnPropertyChanged(nameof(Birthday));
        }

        private void ValidateEmail()
        {
            if (!Email.Contains('@') && !Email.Contains('.'))
                errors[nameof(Email)] = "Please enter a valid email of the form myemail@email.com";
            else
                errors[nameof(Email)] = null;
            OnPropertyChanged(nameof(Email));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveVCardFormat()
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V4,
                FormattedName = FullName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Organization = this.Company,
                Title = JobTitle,
                Telephones = new List<MixERP.Net.VCards.Models.Telephone>(){
                    new MixERP.Net.VCards.Models.Telephone()
                        {
                            Number = MobilePhone,
                            Type = TelephoneType.Cell,
                            Preference = 0
                        }
                },
                BirthDay = Birthday,
                Emails = new List<MixERP.Net.VCards.Models.Email>(){
                    new MixERP.Net.VCards.Models.Email(){
                        EmailAddress = Email
                    }
                },
                Addresses = new List<MixERP.Net.VCards.Models.Address>(){
                    new MixERP.Net.VCards.Models.Address(){
                        Street = Address
                    }
                },
                Note = Notes
            };

            string Serialized = vcard.Serialize();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,$"{FullName}.vcf");
            File.WriteAllText(path, Serialized);
        }
    }
}
