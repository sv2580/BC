using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserPermission
    {
        [Key]
        public string User_email { get; set; }

        [Required]
        public string Permission { get; set; }
    }
}
