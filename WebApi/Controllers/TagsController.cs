using Application.DTOs.Tag;
using Application.Features.Tag.Queries.GetAllTags;
using Application.Features.Tag.Queries.GetTagById;
using Application.Features.Tag.Queries.GetTagsByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;
        
    public TagsController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    [HttpGet("all")]
    public async Task<ActionResult<List<TagResponseDto>>> Get()
    {
        return await _mediator.Send(new GetAllTagsQuery());
    }
        
    [HttpGet("One/{id:int}")]
    public async Task<ActionResult<TagResponseDto>> Get(int id)
    {
        return await _mediator.Send(new GetTagByIdQuery
        {
            Id = id
        });
    }
    
    [HttpGet("SearchbyName/{name}")]
    public async Task<ActionResult<List<TagResponseDto>>> Get(string name)
    {
        return await _mediator.Send(new SearchTagsByNameQuery
        {
            Name = name
        });
    }
}