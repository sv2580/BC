using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Course
    {
//        [Key]
        public int Id { get; set; }

  //      [Required]
    //    [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        //[Required]
        //[Column(TypeName = "nvarchar(100)")]
        public string Subject { get; set; }

        public bool Visibility { get; set; }

        public string Description { get; set; }

    }
}

