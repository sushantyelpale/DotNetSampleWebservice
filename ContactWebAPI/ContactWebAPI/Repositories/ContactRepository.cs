using ContactWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContactWebAPI
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsDbContext _context;

        public ContactRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactModel>> getAllContactsAsync()
        {
            var data = await _context.Contact.Where(e => e.Status.Equals((int)phoneStatus.Active))
                .Select(e=> new ContactModel {
                    email = e.Email,
                    firstName = e.FirstName,
                    guid = e.Guid,
                    lastNme = e.LastName,
                    phoneNumber = e.PhoneNumber,
                    status = (phoneStatus)e.Status
                }).ToListAsync();

            return data;
        }

        public async Task<ContactModel> getContactAsync(Guid guid)
        {
            return await _context.Contact.Where(e => e.Guid.Equals(guid))
                .Select(e => new ContactModel
                {
                    email = e.Email,
                    firstName = e.FirstName,
                    guid = e.Guid,
                    lastNme = e.LastName,
                    phoneNumber = e.PhoneNumber,
                    status = (phoneStatus)e.Status
                }).FirstOrDefaultAsync();
        }

        public async Task<string> addContactAsync(ContactModel contact)
        {
            var selectedContacts = await _context.Contact
                .Where(e => e.PhoneNumber.Equals(contact.phoneNumber) || e.Email.Equals(contact.email))
                .ToListAsync();
            if (selectedContacts.Any())
            {
                return StringResources.DuplicatePhoneOrEmail;
            }
            
            Contact ct = new Contact()
            {
                Email = contact.email,
                FirstName = contact.firstName,
                LastName = contact.lastNme,
                Guid = Guid.NewGuid(),
                PhoneNumber = contact.phoneNumber,
                Status = (int)phoneStatus.Active
            };
            _context.Add(ct);
            await _context.SaveChangesAsync();

            return StringResources.ContactAddedSuccessfully;
        }

        public async Task<bool> editContactAsync(ContactModel contact)
        {
            _context.Contact.Where(e => e.Guid.Equals(contact.guid)).ToList().ForEach(e => {
                e.FirstName = contact.firstName;
                e.LastName = contact.lastNme;
                e.PhoneNumber = contact.phoneNumber;
                e.Email = contact.email;
                e.Status = (int)phoneStatus.Active;
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteContactAsync(Guid guid)
        {
            var list = await _context.Contact.Where(e => e.Guid.Equals(guid)).ToListAsync();
            if(!list.Any())
            {
                return false;
            }
            list.ForEach(e => e.Status = (int)phoneStatus.InActive);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
