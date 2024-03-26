using Backend.Data.Entities;
using Backend.Models.Base;

namespace Backend.Models
{
    public class ReviewDto : EntityDto
    {
        public bool IsPositive { get; set; }
        public string Content { get; set; }
    }
}