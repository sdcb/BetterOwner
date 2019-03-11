using Microsoft.AspNetCore.Http;

namespace BetterOwner.Services.TreasureStore
{
    public interface ITreasureStore
    {
        void Create(TreasureCreatDto creatDto, IFormFileCollection files);
    }
}
