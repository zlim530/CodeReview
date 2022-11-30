using Core.Entities.BlogEntities;
namespace Share.Models.BlogDtos;

public class BlogItemDto
{
    /// <summary>
    /// ����
    /// </summary>
    [MaxLength(30)]
    public required string Title { get; set; }
    /// <summary>
    /// ��Ҫ
    /// </summary>
    [MaxLength(300)]
    public required string Summary { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDeleted { get; set; }
    
}
