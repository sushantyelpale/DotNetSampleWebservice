using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactWebAPI
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactModel>> getAllContactsAsync();
        Task<ContactModel> getContactAsync(Guid guid);
        Task<string> addContactAsync(ContactModel contact);
        Task<bool> editContactAsync(ContactModel contact);
        Task<bool> deleteContactAsync(Guid guid);
    }
}
