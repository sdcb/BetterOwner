using BetterOwner.Services.Database;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterOwner.Services.TreasureManager
{
    public class TreasureQuery
    {
        public string PageToken { get; set; }

        public List<TreasureExploreDto> DoQuery(ApplicationDbContext db, IDataProtector dataProtector, Func<Guid, string> idToUrl)
        {
            int? afterId = DecryptToken(PageToken, dataProtector);

            IQueryable<Treasure> query = db.Treasures
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.Id);

            if (afterId != null)
                query = query.Where(x => x.Id < afterId);

            return query
                .Select(x => new TreasureExploreDto
                {
                    Id = dataProtector.Protect(x.Id.ToString()),
                    Title = x.Title,
                    Price = x.Price,
                    PictureUrl = idToUrl(x.TreasurePictures.First().Id), 
                    PublishUser = x.CreateUser.UserName, 
                })
                .Take(20)
                .ToList();

            static int? DecryptToken(string token, IDataProtector dataProtector)
            {
                if (token == null) return null;
                return int.Parse(dataProtector.Unprotect(token));
            }
        }
    }
}
