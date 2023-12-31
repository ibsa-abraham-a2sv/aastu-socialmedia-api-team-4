﻿using MediatR;

namespace Application.Features.Like.Commands.Delete_Like;

public class DeleteLikeCommand : IRequest<bool>
{
    public int PostId { get; set; }
    public int UserId { get; set; }
}