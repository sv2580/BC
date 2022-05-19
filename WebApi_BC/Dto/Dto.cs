using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public bool Visibility { get; set; }

        public string Description { get; set; }
    }

    public class UpdateCourseDto
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public bool Visibility { get; set; }
        public string Description { get; set; }


    }

    public class CreateChapterDto
    {
        public string Name { get; set; }
        public int Id_course { get; set; }

    }

    public class UpdateChapterDto
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Id_course { get; set; }

    }


    public class CreateLessonDto
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Id_chapter { get; set; }
        public string Text { get; set; }

    }

    public class UpdateLessonDto
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Id_chapter { get; set; }
        public string Text { get; set; }


    }




    public class CreateUserPermissionDto
    {
        public string User_email { get; set; }
        public string Permission { get; set; }

    }

    public class UpdateUserPermissionDto
    {
        public string User_email { get; set; }
        public string Permission { get; set; }
    }


    public class CreateUserRoleDto
    {
        public string User_email { get; set; }
        public string Role { get; set; }

    }

    public class UpdateUserRoleDto
    {
        public string User_email { get; set; }
        public string Role { get; set; }

    }
    public class CreateUserDto
    {
        public string User_email { get; set; }
        public string User_password { get; set; }

    }

    public class UpdateUserDto
    {
        public string User_email { get; set; }
        public string User_password { get; set; }

    }

}
