using HW08.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW08.Services
{
    public interface IDataProvider
    {
        bool Delete(IContact contact);

        List<IContact> GetAllContacts();

        IContact GetContactById(int id);

        bool Insert(IContact contact);

        bool Update(IContact contact);
    }
}
