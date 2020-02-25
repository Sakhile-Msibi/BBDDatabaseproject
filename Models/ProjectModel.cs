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
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}