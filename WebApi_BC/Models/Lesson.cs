using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Lesson
    {
        [Key]
        public int Id_lesson { get; set; }
        public String Name { get; set; }
        public int Rank { get; set; }
        public int Id_chapter { get; set; }
        public string Text { get; set; }

    }
}
