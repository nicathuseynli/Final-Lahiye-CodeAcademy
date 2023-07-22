using Final_Lahiye.Membership;

namespace Final_Lahiye.Models
{
    public class Comment : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;

        public int? ParentId { get; set; }
        public Comment Parent { get; set; }
        public virtual ICollection<Comment> Children { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual MUser User { get; set; }
    }
}
