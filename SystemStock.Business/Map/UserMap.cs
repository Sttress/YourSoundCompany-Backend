using AutoMapper;
using SystemStock.Business.Model.User;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Map
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
