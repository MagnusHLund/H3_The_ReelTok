using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Tests
{
    public class StorageServiceTests : IStorageService
    {
        public Task UploadVideoToFileServer(IFormFile video)
        {
            throw new NotImplementedException();
        }
    }
}
