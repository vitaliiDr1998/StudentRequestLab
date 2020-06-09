using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication1;

namespace IntegrationTests
{
    public class TestingWebAppFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                var descriptor = services.SingleOrDefault(
                  d => d.ServiceType ==
                     typeof(DbContextOptions<DIContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var serviceProvider = new ServiceCollection()
                  .AddEntityFrameworkInMemoryDatabase()
                  .BuildServiceProvider();

                services.AddDbContext<DIContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    using (var dbContext = scope.ServiceProvider.GetRequiredService<DIContext>())
                    {
                        try
                        {
                            dbContext.Database.EnsureCreated();
                            if (!dbContext.Users.Any(u => u.Id == UserSettings.UserId))
                            {

                                var user = new IdentityUser();
                                user.ConcurrencyStamp = DateTime.Now.Ticks.ToString();
                                user.Email = UserSettings.UserEmail;
                                user.EmailConfirmed = true;
                                user.Id = UserSettings.UserId;
                                user.NormalizedEmail = user.Email;
                                user.NormalizedUserName = user.Email;
                                user.PasswordHash = Guid.NewGuid().ToString();
                                user.UserName = user.Email;

                                var role = new IdentityRole();
                                role.ConcurrencyStamp = DateTime.Now.Ticks.ToString();
                                role.Id = "Admin";
                                role.Name = "Admin";

                                var userRole = new IdentityUserRole<string>();
                                userRole.RoleId = "Admin";
                                userRole.UserId = user.Id;

                                dbContext.Users.Add(user);
                                dbContext.Roles.Add(role);
                                dbContext.UserRoles.Add(userRole);
                                dbContext.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            
                            throw;
                        }
                    }
                }
            });
        }
    }
}
