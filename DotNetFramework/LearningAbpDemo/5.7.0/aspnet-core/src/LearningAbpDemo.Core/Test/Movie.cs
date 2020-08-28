using Abp.Domain.Entities;

namespace LearningAbpDemo.Test
{
    public class Movie:Entity<int>
    {
        public string Name { get; set; }

        public string Starts { get; set; }
    }
}
