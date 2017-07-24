using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CardRibbn.Models
{
    // Add profile data for application users by adding properties to the CardCraftUser class
    public class CardCraftUser : IdentityUser
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public int DepartmentId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Password = "dummy";
    }
}
