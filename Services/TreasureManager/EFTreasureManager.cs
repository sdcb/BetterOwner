using BetterOwner.Services.CurrentUser;
using BetterOwner.Services.Database;
using BetterOwner.Services.TreasurePictureStore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BetterOwner.Services.TreasureManager
{
    public class EFTreasureManager : ITreasureManager
    {
        private readonly ApplicationDbContext db;
        private readonly ITreasurePictureStore treasurePictureStore;
        private readonly ICurrentUser user;
        private readonly IDataProtector dataProtector;

        public EFTreasureManager(
            ApplicationDbContext db, 
            ITreasurePictureStore treasurePictureStore, 
            ICurrentUser user, 
            IDataProtectionProvider dataProtectionProvider)
        {
            this.db = db;
            this.treasurePictureStore = treasurePictureStore;
            this.user = user;
            this.dataProtector = dataProtectionProvider.CreateProtector(nameof(ITreasureManager));
        }

        public void Create(TreasureCreateDto createDto, IFormFileCollection files)
        {
            Treasure treasure = createDto.ToTreasure();
            treasure.CreateUserId = user.Id;
            db.Entry(treasure).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            db.SaveChanges();

            treasurePictureStore.Upload(treasure.Id, files);
        }

        public List<TreasureExploreDto> Query(TreasureQuery query)
        {
            return query.DoQuery(db, dataProtector, PictureIdToUrl);
        }

        private static string PictureIdToUrl(Guid id)
        {
            return $"/explore/picture/{id}";
        }
    }
}
