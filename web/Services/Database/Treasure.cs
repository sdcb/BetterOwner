using System;
using System.Collections.Generic;

namespace BetterOwner.Services.Database
{
    public class Treasure
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int CreateUserId { get; set; }

        public User CreateUser { get; set; }

        public ICollection<TreasurePicture> TreasurePictures { get; set; }
    }
}
