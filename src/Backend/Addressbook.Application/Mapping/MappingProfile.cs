using Addressbook.Domain.Models;
using Addressbook.Models.Dtos;
using AutoMapper;

namespace Addressbook.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IpAddressBookDto, IpAddressBook>()
              .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Now))
              .ReverseMap();
        }
    }
}
