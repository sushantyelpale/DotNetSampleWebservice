using System;
using System.Collections.Generic;

namespace ContactWebAPI.Models
{
    public partial class Contact
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
    }
}
