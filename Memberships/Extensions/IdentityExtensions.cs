using Memberships.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Memberships.Extensions
{
    public static class IdentityExtensions
    {
        // Fetch User from AspNetUsers Table & return FirstName
        public static string GetUserFirstName(this IIdentity identity)
        {
            // Variable to hold ApplicationDbContext instance
            var db = ApplicationDbContext.Create();
            // Variable to hold Users from fetched database
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(identity.Name));

            // If User is not null, return FirstName property
            return user != null ? user.FirstName : String.Empty;
        }

        // Convert AspNetUsers from the table into an object of the type UserViewModel
        public static async Task GetUsers(this List<UserViewModel> users)
        {
            var db = ApplicationDbContext.Create();
            users.AddRange(await (from u in db.Users
                                  select new UserViewModel
                                  {
                                      Id = u.Id,
                                      Email = u.Email,
                                      FirstName = u.FirstName
                                  }).OrderBy(o => o.Email).ToListAsync());
        }
    }
}