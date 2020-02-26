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
            new ProjectModel() { projectId = 1, projectName = "TeamMan", projectDescription = "Project ONE", StartDate = new DateTime(1995, 1, 1), DueDate = new DateTime(1995, 1, 1) },
            new ProjectModel() { projectId = 2, projectName = "ManTeam", projectDescription = "Project 2", StartDate = new DateTime(1995, 1, 1), DueDate = new DateTime(1995, 1, 1) },
        };
    }
}