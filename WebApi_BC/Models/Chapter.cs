using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Models
{
    public class Chapter
    {
        [Key]
        public int Id_chapter { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public int Rank { get; set; }
        [Required]
        public int Id_course { get; set; }

        public List<Chapter> ListModel = new List<Chapter>();


    }
}
