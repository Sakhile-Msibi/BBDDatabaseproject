using System;
using System.ComponentModel.DataAnnotations;
namespace Website.Models
{
    public class ProjectModel
    {
        [Key]
        public int projectId { get; set; }

        [Required]
        public string projectName { get; set; }

        public int AdminId {get; set;}
        public string projectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}