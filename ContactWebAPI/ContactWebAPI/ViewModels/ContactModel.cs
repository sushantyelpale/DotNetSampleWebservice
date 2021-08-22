using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactWebAPI
{
    public class ContactModel
    {
        public Guid guid { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string email { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public phoneStatus status { get; set; }
    }
}
