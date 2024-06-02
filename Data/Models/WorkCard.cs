using System.ComponentModel.DataAnnotations;

namespace WorkTime.Data.Models
{
    public class WorkCard
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkTaskId { get; set; }
        public WorkTask WorkTask { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
