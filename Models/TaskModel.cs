using System;
using System.ComponentModel.DataAnnotations;
namespace Website.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
        [Required]
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "Progress")]
        [Range(1, 100, ErrorMessage = "The progress must be between 1 - 100")]
        public int Progress { get; set; } // 0 - 100 

        [Required]
        [Display(Name = "Status")]
        public string Flags { get; set; } //In Progess, Stuck,  Complete?

        [Required]
        public string Comments { get; set; } // Provides reasons for the flags

        [Required]
        [Display(Name = "Project Id")]
        public int ProjectId { get; set; }
        [Required]
        [Display(Name = "User Id")]
        public int UserId { get; set; }
    }
}