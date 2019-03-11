using BetterOwner.Services.TreasurePictureStore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BetterOwner.Services.FileStore
{
    public static class ServiceFileStoreExtensions
    {
        public static IServiceCollection AddFileStore(this IServiceCollection service, string fileStoreConfiguration)
        {
            switch (fileStoreConfiguration)
            {
                case null:
                    throw new Exception("FileStore has not been configured.");
                case "Native":
                    service.AddTransient<ITreasurePictureStore, NativeTreasurePictureStore>();
                    break;
                case "EF":
                    service.AddTransient<ITreasurePictureStore, EFTreasurePictureStore>();
                    break;
                default:
                    throw new Exception($"FileStore '{fileStoreConfiguration}' is not recognized as a valid file store.");
            }
            return service;
        }
    }
}
