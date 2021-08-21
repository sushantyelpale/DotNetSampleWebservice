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
        public IEnumerable<ContactModel> GetAllContacts()
        {
            return _repo.getAllContacts();
        }

        [HttpGet("GetContact/{guid}")]
        public ContactModel GetContact(Guid guid)
        {
            return _repo.getContact(guid);
        }

        [HttpPost("AddContact")]
        public string AddContact([FromBody] ContactModel model)
        {
            if(!ModelState.IsValid)
            {
                throw new AppException(StringResources.InvalidData);
            }

            return _repo.addContact(model);
        }

        [HttpPost("EditContact")]
        public string EditContact([FromBody] ContactModel model)
        {
            if(!ModelState.IsValid || !_repo.editContact(model))
            {
                throw new AppException(StringResources.InvalidData);
            }
            return StringResources.ContactEditedSuccessfully;
        }

        // DELETE api/values/5
        [HttpDelete("DeleteContact/{guid}")]
        public string DeleteContact(Guid guid)
        {
            if(!_repo.deleteContact(guid))
            {
                throw new AppException(StringResources.InvalidData);
            }
            return StringResources.ContactDeletedSuccessfully;
        }
    }
}
