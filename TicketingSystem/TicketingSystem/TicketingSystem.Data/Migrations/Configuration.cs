namespace TicketingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Models.Enums;
    using Common;
    using System.Reflection;

    public sealed class Configuration : DbMigrationsConfiguration<TicketingSystemDbContext>
    {
        private UserManager<User> userManager;
        private IRandomDataGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = RandomDataGenerator.Instance;
        }

        protected override void Seed(TicketingSystemDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCategoriesWithTicketsAndComments(context);
        }

        private void SeedRoles(TicketingSystemDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.SaveChanges();
        }

        private void SeedUsers(TicketingSystemDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var user = new User()
                {
                    Email = $"{this.random.GetRandomString(7, 16)}@{this.random.GetRandomString(7, 16)}.com",
                    UserName = this.random.GetRandomString(7, 16)
                };

                this.userManager.Create(user, "123456");
            }

            var adminUser = new User()
            {
                Email = "admin@mysite.com",
                UserName = "Administrator"
            };

            this.userManager.Create(adminUser, "admin123456");

            this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
        }

        private void SeedCategoriesWithTicketsAndComments(TicketingSystemDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var image = this.GetSampleImage();
            var users = context.Users
                .Take(10)
                .ToList();

            for (int i = 0; i < 5; i++)
            {
                var category = new Category()
                {
                    Name = this.random.GetRandomString(5, 10)
                };

                for (int j = 0; j < 10; j++)
                {
                    var ticket = new Ticket()
                    {
                        Author = users[this.random.GetRandomNumber(0, users.Count - 1)],
                        Title = this.random.GetRandomString(5, 40),
                        Description = this.random.GetRandomString(100, 300),
                        Image = image,
                        Priority = (PriorityType)this.random.GetRandomNumber(0, 2)
                    };

                    for (int k = 0; k < 5; k++)
                    {
                        var comment = new Comment()
                        {
                            Author = users[this.random.GetRandomNumber(0, users.Count - 1)],
                            Content = this.random.GetRandomString(30, 200),
                        };

                        ticket.Comments.Add(comment);
                    }

                    category.Tickets.Add(ticket);
                }

                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        private Image GetSampleImage()
        {
            var directory = AssemblyHelpers.GetDirectoryForAssembly(Assembly.GetExecutingAssembly());

            var file = File.ReadAllBytes(directory + "/Migrations/Images/bug-vs-feature.jpg");
            var image = new Image()
            {
                Content = file,
                FileExtension = "jpg"
            };

            return image;
        }
    }
}
