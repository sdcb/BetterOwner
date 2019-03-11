using BetterOwner.Services.CurrentUser;
using BetterOwner.Services.Database;
using BetterOwner.Services.TreasurePictureStore;
using Microsoft.AspNetCore.Http;

namespace BetterOwner.Services.TreasureManager
{
    public class EFTreasureManager : ITreasureManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ITreasurePictureStore _treasurePictureStore;
        private readonly ICurrentUser _user;

        public EFTreasureManager(
            ApplicationDbContext db, 
            ITreasurePictureStore treasurePictureStore, 
            ICurrentUser user)
        {
            _db = db;
            _treasurePictureStore = treasurePictureStore;
            _user = user;
        }

        public void Create(TreasureCreateDto createDto, IFormFileCollection files)
        {
            Treasure treasure = createDto.ToTreasure();
            treasure.CreateUserId = _user.Id;
            _db.Entry(treasure).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();

            _treasurePictureStore.Upload(treasure.Id, files);
        }
    }
}
