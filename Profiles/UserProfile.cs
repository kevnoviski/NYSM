using AutoMapper;
using NYSM.Models;
using NYSM.Dtos;

namespace NYSM.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}