using AutoMapper;
using NYSM.Dtos;
using NYSM.Models;

namespace NYSM.Profiles
{
    public class AppFileProfile :Profile
    {
        public AppFileProfile()
        {
            CreateMap<AppFileCreateAlterDto,AppFile>();    
        }
    }
}