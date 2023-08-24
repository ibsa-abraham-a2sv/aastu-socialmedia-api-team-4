using Domain.Common;

namespace Domain.Entities;

public class TagEntity: BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public List<PostEntity> Posts { get; set; } = null!;
}