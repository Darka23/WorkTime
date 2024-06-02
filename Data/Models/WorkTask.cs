using System.ComponentModel.DataAnnotations;
using WorkTime.Data.Identity;

namespace WorkTime.Data.Models
{
    public class WorkTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        
        public string? CreatorId { get; set; }
       
        public ApplicationUser? Creator { get; set; }
        public List<ApplicationUser>? Workers { get; set; }
        public int? TotalHours { get; set; }
        public int? CurrentUsedHours { get; set; }
        public string Status { get; set; }
    }
}
