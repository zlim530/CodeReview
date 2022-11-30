using Core.Entities.BlogEntities;
namespace Share.Models.BlogDtos;

public class BlogAddDto
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
    /// <summary>
    /// ����
    /// </summary>
    [MaxLength(5000)]
    public required string Content { get; set; }
    public Guid SystemUserId { get; set; }
    
}
