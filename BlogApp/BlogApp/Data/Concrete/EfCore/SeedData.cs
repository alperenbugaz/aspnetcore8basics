using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var Context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if (Context !=  null)
            {
                if (Context.Database.GetPendingMigrations().Any())
                {
                    Context.Database.Migrate();
                }

                if (!Context.Tags.Any())
                {
                    Context.Tags.AddRange(
                        new Entity.Tag() { Text = "WEB Programlama" , Url = "web-programlama" ,TagColor = Entity.TagColors.warning},
                        new Entity.Tag() { Text = "Backend" , Url = "backend" , TagColor = Entity.TagColors.warning},
                        new Entity.Tag() { Text = "Frontend" , Url = "frontend" , TagColor = Entity.TagColors.success},
                        new Entity.Tag() { Text = "FullStack", Url = "fullstack", TagColor = Entity.TagColors.primary },
                        new Entity.Tag() { Text = "PHP" , Url = "php" , TagColor = Entity.TagColors.danger}

                    );
                    Context.SaveChanges();
                }

                if (!Context.Users.Any())
                {
                    Context.Users.AddRange(
                        new Entity.User() { UserName = "alperenbugaz",Name="Alperen Buğaz" ,PostImage="alperen.jpeg" , Email = "alperen@mail.com",Password = "123456"},
                        new Entity.User() { UserName = "ferhattopcuoglu",Name="Ferhat Topçuoğlu", PostImage = "ferhat.jpeg" , Email = "ferhat@mail.com", Password = "123456"},
                        new Entity.User() { UserName = "beyzasenay" ,Name="Beyza Nur Şenay", PostImage = "beyza.jpg", Email = "beyza@mail.com", Password = "123456"},
                        new Entity.User() { UserName = "Ahmet" },
                        new Entity.User() { UserName = "Mehmet" }
                    );
                    Context.SaveChanges();
                }

                if (!Context.Posts.Any())
                {
                    Context.Posts.AddRange(
                        new Entity.Post{ 
                            Title = ".NET 5.0", 
                            Description = "C# 9.0 ve .NET 5.0 ile gelen yenilikler", 
                            IsActive = true,
                            Tags = Context.Tags.Take(3).ToList(),
                            CreatedAt = DateTime.Now.AddDays(-5),
                            UserId = 1,
                            PostImage = "1.jpeg",
                            Url = "net-5-0",
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec nunc tincidunt ultricies. Nullam nec pur us nec nunc tincidunt ultricies. Nullam nec purus nec nunc tincidunt ultricies. Nullam nec pur us nec nunc tincidunt ultricies. Nullam nec purus nec nunc"

                            },                        
                            new Entity.Post{ 
                            Title = "PHP", 
                            Description = "PHP dersleri", 
                            IsActive = true,
                            Tags = Context.Tags.Take(3).ToList(),
                            CreatedAt = DateTime.Now.AddDays(-5),
                            UserId = 1,
                            PostImage = "2.png",
                            Url = "php",
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec nunc tincidunt ultricies. Nullam nec pur us nec nunc tincidunt ultricies. Nullam nec purus nec nunc tincidunt ultricies. Nullam nec pur us nec nunc tincidunt ultricies. Nullam nec purus nec nunc"
                            },
                            new Entity.Post{ 
                            Title = "DJANGO", 
                            Description = "Django dersleri", 
                            IsActive = true,
                            Tags = Context.Tags.Take(3).ToList(),
                            CreatedAt = DateTime.Now.AddDays(-5),
                            UserId = 1,
                            PostImage = "3.jpg",
                            Url = "django",
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec nunc tincidunt ultricies. Nullam nec pur us nec nunc tincidunt ultricies. Nullam nec purus nec nunc tincidunt ultricies. Nullam nec pur us nec nunc tincidunt ultricies. Nullam nec purus nec nunc"

                            }
                            
                    );
                    Context.SaveChanges();
                }
            }


        }
       
    
    }

}