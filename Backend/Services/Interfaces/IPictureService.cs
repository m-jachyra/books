using Backend.Models;
using Backend.Services.Base;

namespace Backend.Services
{
    public interface IPictureService
    {
        public Task UpdatePicturePath(PictureDto model);
    }
}