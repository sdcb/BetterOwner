using Microsoft.AspNetCore.Http;

namespace BetterOwner.Services.TreasureManager
{
    public interface ITreasureManager
    {
        void Create(TreasureCreateDto creatDto, IFormFileCollection files);
    }
}
