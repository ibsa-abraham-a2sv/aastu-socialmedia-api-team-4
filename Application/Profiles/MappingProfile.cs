using Application.DTOs.Comment;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping() 
        {
            CreateMap<CommentRequestDTO, CommentEntity>().ReverseMap();
        }
    }
}
