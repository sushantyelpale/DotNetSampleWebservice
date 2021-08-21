using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactWebAPI
{
    public class ContactRepository : IContactRepository
    {
        private static List<ContactModel> contactList;
        public ContactRepository()
        {
            if(contactList == null)
                contactList = new List<ContactModel>();
        }

        public IEnumerable<ContactModel> getAllContacts()
        {
            return contactList.Where(e => e.status.Equals(phoneStatus.Active)).ToList();
        }

        public ContactModel getContact(Guid guid)
        {
            return contactList.Where(e => e.guid.Equals(guid)).FirstOrDefault();
        }

        public string addContact(ContactModel contact)
        {
            var selectedContacts = contactList.Where(e => e.phoneNumber.Equals(contact.phoneNumber) || e.email.Equals(contact.email)).ToList();
            if (selectedContacts.Any())
            {
                return StringResources.DuplicatePhoneOrEmail;
            }

            contact.guid = Guid.NewGuid();
            contact.status = phoneStatus.Active;

            contactList.Add(contact);

            return StringResources.ContactAddedSuccessfully;
        }

        public bool editContact(ContactModel contact)
        {
            contactList.Where(e => e.guid.Equals(contact.guid)).ToList().ForEach(e => {
                e.firstName = contact.firstName;
                e.lastNme = contact.lastNme;
                e.phoneNumber = contact.phoneNumber;
                e.email = contact.email;
                e.status = phoneStatus.Active;
            });
            return true;
        }

        public bool deleteContact(Guid guid)
        {
            var list = contactList.Where(e => e.guid.Equals(guid)).ToList();
            if(!list.Any())
            {
                return false;
            }
            list.ForEach(e => e.status = phoneStatus.InActive);
            return true;
        }
    }
}
