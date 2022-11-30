using Core.Entities.BlogEntities;
namespace Share.Models.BlogDtos;

public class BlogFilterDto : FilterBase
{
    /// <summary>
    /// ����
    /// </summary>
    [MaxLength(30)]
    public string? Title { get; set; }
    /// <summary>
    /// ��Ҫ
    /// </summary>
    [MaxLength(300)]
    public string? Summary { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    [MaxLength(5000)]
    public string? Content { get; set; }
    public Guid? SystemUserId { get; set; }
    
}
