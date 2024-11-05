using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();

        static Repository()
        {
            _courses = new List<Course>()
            {
            new Course()  
            {   Id = 1, 
                Title = "ASP.NET Core MVC", 
                Description = "Description 1",
                Image = "image1.jpg", 
                Tags = new string[] { "C#", "ASP.NET", "MVC" }, 
                isActive = true, 
                isHome = true
            },
            new Course()  
            {
                Id = 2, 
                Title = "ASP.NET Core MVC 2", 
                Description = "Description 2", 
                Image = "image2.jpg", 
                Tags = new string[] { "C#", "ASP.NET", "MVC" }, 
                isActive = true, 
                isHome = true},
                
            new Course() 
             {  Id = 3, 
                Title = "ASP.NET Core MVC 3", 
                Description = "Description 3", 
                Image = "image3.jpg", 
                Tags = new string[] { "C#", "ASP.NET", "MVC" }, 
                isActive = true, 
                isHome = true},
            };

        }
        public static List<Course> Courses
        {
            get
            {
                return _courses;
            }
        }

        public static Course? GetById(int id)
        {
            return _courses.FirstOrDefault(x => x.Id == id);
        }
    }
}