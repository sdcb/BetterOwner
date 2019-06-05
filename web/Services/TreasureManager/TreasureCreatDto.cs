using BetterOwner.Services.Database;
using System;
using System.ComponentModel.DataAnnotations;

namespace BetterOwner.Services.TreasureManager
{
    public class TreasureCreateDto
    {
        [Required]
        public string Title { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public Treasure ToTreasure()
        {
            return new Treasure
            {
                Title = Title, 
                Price = Price, 
                Description = Description, 
                CreateTime = DateTime.Now, 
                UpdateTime = DateTime.Now, 
                IsPublic = true, 
            };
        }
    }
}