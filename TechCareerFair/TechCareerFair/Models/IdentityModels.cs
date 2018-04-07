using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TechCareerFair.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<applicant> Applicants { get; set; }
        public virtual ICollection<business> Businesses { get; set; }

        public ApplicationUser()
        {
           
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CareerFair", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TechCareerFair.Models.gallery> galleries { get; set; }

        public System.Data.Entity.DbSet<TechCareerFair.Models.user2applicant> positions { get; set; }

        public System.Data.Entity.DbSet<TechCareerFair.Models.applicant> applicants { get; set; }

        public System.Data.Entity.DbSet<TechCareerFair.Models.business> businesses { get; set; }
    }
}