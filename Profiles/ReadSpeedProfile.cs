using AutoMapper;
using NYSM.Dtos;
using NYSM.Models;

namespace NYSM.Profiles
{
    public class ReadSpeedProfile : Profile
    {
        public ReadSpeedProfile()
        {
            CreateMap<ReadSpeed,ReadSpeedDto>();
            CreateMap<ReadSpeedDto,ReadSpeed>();
        }
    }
}