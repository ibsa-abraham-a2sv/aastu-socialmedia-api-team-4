using Application.DTOs.Comment;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.User;
using Application.DTOs.Post;

namespace Application.Profiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping() 
        {
            CreateMap<CommentRequestDTO, CommentEntity>().ReverseMap();
            
            CreateMap<UserEntity, UserResponseDto>().ReverseMap();
            CreateMap<UserEntity, UserRequestDto>().ReverseMap();

            CreateMap<PostEntity, PostResponseDto>().ReverseMap();
            CreateMap<PostEntity, PostRequestDto>().ReverseMap();
        }
    }
}
