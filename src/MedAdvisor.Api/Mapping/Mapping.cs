
using AutoMapper;
using MedAdvisor.Api.Dtos;
using MedAdvisor.Api.Models;
using MedAdvisor.Models;

namespace MedAdvisor.DataAccess.MySql
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<UpdateDocumentDto,Document>()
                .ReverseMap().ForMember(x => x.File, opt => opt.Ignore());
            CreateMap<FileUploadDto, Document>()
                .ReverseMap().ForMember(x => x.File, opt => opt.Ignore());
            CreateMap<AddProfileDto, UserProfile>().ReverseMap();

        }
    }
}
