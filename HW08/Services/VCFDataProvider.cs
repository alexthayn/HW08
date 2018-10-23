using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HW08.Models;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;

namespace HW08.Services
{
    public class VCFDataProvider : IDataProvider
    {
        public bool Delete(IContact contact)
        {
            throw new NotImplementedException();
        }

        public List<IContact> GetAllContacts()
        {
            List<IContact> AllContacts = new List<IContact>();
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\Contacts";
            //if (Directory.Exists(path))
            //{
            //    string contents = File.ReadAllText(path, Encoding.UTF8);
            //    IEnumerable<VCard> AllVCards = Deserializer.GetVCards(contents);                

            //    foreach (VCard v in AllVCards)
            //    {
            //        var c = new Contact()
            //        {
            //            Id = v.UniqueIdentifier,
            //            FirstName = v?.FirstName,
            //            LastName = v?.LastName,
            //            Company = v?.Organization,
            //            JobTitle = v?.Title,
            //            MobilePhone = v?.Telephones.ToString(),
            //            Birthday = (DateTime)v?.BirthDay,
            //            Email = v?.Emails.ToString(),
            //            Address = v?.Addresses.ToString(),
            //            Notes = v?.Note
            //        };

            //        AllContacts.Add(c);
            //    }                
            //}
            return AllContacts;
        }

        public IContact GetContactById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(IContact contact)
        {
            throw new NotImplementedException();
        }

        public void Insert(IContact contact)
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V4,
                UniqueIdentifier = contact.Id,
                FormattedName = contact.FullName,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Organization = contact.Company,
                Title = contact.JobTitle,
                Telephones = new List<MixERP.Net.VCards.Models.Telephone>(){
                    new MixERP.Net.VCards.Models.Telephone()
                        {
                            Number = contact.MobilePhone,
                            Type = TelephoneType.Cell,
                            Preference = 0
                        }
                },
                BirthDay = contact.Birthday,
                Emails = new List<MixERP.Net.VCards.Models.Email>(){
                    new MixERP.Net.VCards.Models.Email(){
                        EmailAddress = contact.Email
                    }
                },
                Addresses = new List<MixERP.Net.VCards.Models.Address>(){
                    new MixERP.Net.VCards.Models.Address(){
                        Street = contact.Address
                    }
                },
                Note = contact.Notes
            };

            var contactPath = "\\Contacts";
            var otherPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + contactPath;
            bool exists = Directory.Exists(otherPath);
            
            if (!exists)
                Directory.CreateDirectory(otherPath);

            string Serialized = vcard.Serialize();
            string path = Path.Combine(otherPath, $"{contact.Id}.vcf");
            File.WriteAllText(path, Serialized);
        }        
    }
}
