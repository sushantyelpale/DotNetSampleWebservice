using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ContactWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public readonly IContactRepository _repo;

        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllContacts")]
        public async Task<IEnumerable<ContactModel>> GetAllContacts()
        {
            return await _repo.getAllContactsAsync();
        }

        [HttpGet("GetContact/{guid}")]
        public async Task<ContactModel> GetContact(Guid guid)
        {
            return await _repo.getContactAsync(guid);
        }

        [HttpPost("AddContact")]
        public async Task<string> AddContact([FromBody] ContactModel model)
        {
            if(!ModelState.IsValid)
            {
                throw new AppException(StringResources.InvalidData);
            }

            return await _repo.addContactAsync(model);
        }

        [HttpPost("EditContact")]
        public async Task<string> EditContact([FromBody] ContactModel model)
        {
            if(!ModelState.IsValid || !await _repo.editContactAsync(model))
            {
                throw new AppException(StringResources.InvalidData);
            }
            return StringResources.ContactEditedSuccessfully;
        }

        // DELETE api/values/5
        [HttpDelete("DeleteContact/{guid}")]
        public async Task<string> DeleteContact(Guid guid)
        {
            if(!await _repo.deleteContactAsync(guid))
            {
                throw new AppException(StringResources.InvalidData);
            }
            return StringResources.ContactDeletedSuccessfully;
        }
    }
}
