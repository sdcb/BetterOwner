using System;
using System.ComponentModel.DataAnnotations;

namespace BetterOwner.Services.Database
{
    public class OAUser
    {
        [Key]
        public int UserId { get; set; }

        public string JobId { get; set; }

        public Sex Sex { get; set; }

        public User User { get; set; }
    }

    public enum Sex
    {
        Male = 1, 
        Female = 2, 
    }
}
