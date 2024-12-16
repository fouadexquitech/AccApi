using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AutoMapper;

namespace AccApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TblOriginalBoqVd, OriginalBoqModel>().ReverseMap();
            CreateMap<TblBoqVd, BoqModel>().ReverseMap();
        }
    }
}