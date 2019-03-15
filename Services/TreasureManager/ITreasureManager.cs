using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BetterOwner.Services.TreasureManager
{
    public interface ITreasureManager
    {
        void Create(TreasureCreateDto creatDto, IFormFileCollection files);

        List<TreasureExploreDto> Query(TreasureQuery query);
    }
}
