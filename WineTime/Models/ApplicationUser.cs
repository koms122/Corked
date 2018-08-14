using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WineTime.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //EF requires two constructors
        public ApplicationUser() : base()
        {
            this.WineCart = new WineCart();
        }

        public ApplicationUser(string userName) : base(userName)

        {
            this.WineCart = new WineCart();
        }
        
        // associates a cart with each user and each user with a cart
        public WineCart WineCart { get; set; }
        public int WineCartID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
