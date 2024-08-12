using ASP.NET_HomeWork.Models;
using ASP.NET_HomeWork.Models.DTOs;
using AutoMapper;

namespace ASP.NET_HomeWork.Repo
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
