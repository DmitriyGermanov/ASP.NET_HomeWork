using ASP.NET_Seminar3.Models;
using AutoMapper;

namespace ASP.NET_Seminar_3.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
