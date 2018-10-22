using System;

namespace HW08.Models
{
    public interface IContact
    {
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
        string Company { get; set; }
        string JobTitle { get; set; }
        string MobilePhone { get; set; }
        DateTime Birthday { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        string Notes { get; set; }
    }
}
