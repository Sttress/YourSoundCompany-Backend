using AutoMapper;
using YourSoundCompany.Business.Model.User.DTO;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.RelationalData.Entities;

namespace YourSoundCompnay.Business.Map
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<UserEntity, UserResponseDTO > ().ReverseMap();
            CreateMap<UserModel, UserResponseDTO>().ReverseMap();
        }
    }
}
