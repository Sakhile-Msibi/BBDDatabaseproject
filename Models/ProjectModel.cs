using System;
using System.ComponentModel.DataAnnotations;
namespace Website.Models
{
    public class ProjectModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int AdminId {get; set;}
        public string Description { get; set; }

        
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
    }
}