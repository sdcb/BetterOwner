using Microsoft.Extensions.DependencyInjection;
using System;

namespace BetterOwner.Services.FileStore
{
    public static class ServiceFileStoreExtensions
    {
        public static void AddFileStore(this IServiceCollection service, string fileStoreConfiguration)
        {
            switch (fileStoreConfiguration)
            {
                case null:
                    throw new Exception("FileStore has not been configured.");
                case "Native":
                    service.AddTransient<IFileStore, NativeFileStore>();
                    break;
                case "EF":
                    service.AddTransient<IFileStore, EFFileStore>();
                    break;
                default:
                    throw new Exception($"FileStore '{fileStoreConfiguration}' is not recognized as a valid file store.");
            }
        }
    }
}
