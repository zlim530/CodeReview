using Core.Models;

namespace Core.Entities.BlogEntities;
[NgPage("system","blog")]
public class Blog : EntityBase
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
    /// <summary>
    /// ������/����
    /// </summary>
    public required SystemUser SystemUser { get; set; }
}