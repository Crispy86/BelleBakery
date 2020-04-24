using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Subject { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "The message is too long!!!")]
        public String Message { get; set; }


    }
}
