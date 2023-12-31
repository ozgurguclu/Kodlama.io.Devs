﻿using Application.Features.Technologies.Commands;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();
            CreateMap<Technology, TechnologyListDto>().ReverseMap();
            CreateMap<Technology, TechnologyGetByIdDto>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
        }
    }
}
