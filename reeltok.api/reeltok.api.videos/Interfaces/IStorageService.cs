using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.Interfaces
{
    public interface IStorageService
    {
        Task UploadVideoToFileServer(IFormFile video);
    }
}
