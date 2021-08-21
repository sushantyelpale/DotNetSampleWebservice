using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactWebAPI
{
    public interface IContactRepository
    {
        IEnumerable<ContactModel> getAllContacts();
        ContactModel getContact(Guid guid);
        string addContact(ContactModel contact);
        bool editContact(ContactModel contact);
        bool deleteContact(Guid guid);
    }
}
