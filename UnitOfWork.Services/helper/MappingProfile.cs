using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Models;
using UnitOfWork.Infrastructure.DTOs;

namespace UnitOfWork.Services.helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
        }
    }
}
