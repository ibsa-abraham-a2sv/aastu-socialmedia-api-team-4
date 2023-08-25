using Application.DTOs.Comment;
using Application.DTOs.Post;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Tag;
using Application.DTOs.User;
using Domain.Common;

namespace Application.Profiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping() 
        {
            CreateMap<BaseDomainEntity, BaseDto>().ReverseMap();
            CreateMap<CommentRequestDTO, CommentEntity>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            })); 
            
            CreateMap<CommentEntity, CommentResponseDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) =>
                {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            })); 
            
            CreateMap<UserEntity, UserResponseDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));

            CreateMap<UserEntity, UserRequestDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));

            CreateMap<PostEntity, PostResponseDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));

            CreateMap<PostEntity, PostRequestDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int and 0)
                {
                    return false;
                }
                return srcMember != null;
            }));
            
            CreateMap<TagEntity, TagResponseDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));
            
            CreateMap<TagEntity, TagRequestDto>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));
        }

    }
}
