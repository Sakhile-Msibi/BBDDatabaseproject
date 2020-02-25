using System.Collections.Generic;
using System;
using Website.Models;

namespace Website.StaticData
{
    public class ProjectData
    {   
        public static List<ProjectModel> People 
        {
            get
            {
                return listOfProjects;
            }
        }

        private static List<ProjectModel> listOfProjects = new List<ProjectModel>()
        {
            new ProjectModel() { Id = 1, Name = "TeamMan", Description = "Project ONE", StartDate = new DateTime(1995, 1, 1), DueDate = new DateTime(1995, 1, 1) },
            new ProjectModel() { Id = 2, Name = "ManTeam", Description = "Project 2", StartDate = new DateTime(1995, 1, 1), DueDate = new DateTime(1995, 1, 1) },
        };
    }
}