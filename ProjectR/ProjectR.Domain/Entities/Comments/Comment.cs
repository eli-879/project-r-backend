using ProjectR.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectR.Domain.Entities;
public sealed class Comment : Entity
{
    public Comment(Guid id) : base(id) { }

    public Guid UserId { get; set; }

    public string Message { get; set; } = string.Empty;

    [ForeignKey("ParentId")]
    public Comment Parent { get; set; }
    public Guid? ParentId { get; set; }



}
