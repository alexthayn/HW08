using HW08.Models;
using System.Collections.Generic;

namespace HW08.Services
{
    public interface IDataProvider
    {
        bool Delete(IContact contact);

        List<IContact> GetAllContacts();

        IContact GetContactById(int id);

        void Insert(IContact contact);

        bool Update(IContact contact);
    }
}
