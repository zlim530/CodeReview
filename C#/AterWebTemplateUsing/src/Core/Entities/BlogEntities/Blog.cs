using Core.Models;

namespace Core.Entities.BlogEntities;
[NgPage("system","blog")]
public class Blog : EntityBase
{
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(30)]
    public required string Title { get; set; }
    /// <summary>
    /// 概要
    /// </summary>
    [MaxLength(300)]
    public required string Summary { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [MaxLength(5000)]
    public required string Content { get; set; }
    /// <summary>
    /// 发布人/作者
    /// </summary>
    public required SystemUser SystemUser { get; set; }
}