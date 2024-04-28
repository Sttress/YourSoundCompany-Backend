using AutoMapper;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.RelationalData.Entities;

namespace YourSoundCompnay.Business.Map
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<UserEntity, UserResponseModel > ().ReverseMap();
        }
    }
}
