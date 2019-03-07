﻿using System;

namespace BetterOwner.Services.Database
{
    public class Treasure
    {
        public int Id { get; set; }

        public string Titile { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int CreateUserId { get; set; }

        public User CreateUser { get; set; }
    }
}
