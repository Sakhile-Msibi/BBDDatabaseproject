/* using Website.Models;
using System;
using System.Collections.Generic;
namespace Website.StaticData
{
    public class TaskData
    {
        public static List<TaskModel> People 
        {
            get
            {
                return listOfPeople;
            }
        }

        private static List<TaskModel> listOfPeople = new List<TaskModel>()
        {
            new TaskModel() { TaskId = 1, TaskName = "TaskA", TaskDescription = "Desc1", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 1, UserId = 1},
            new TaskModel() { TaskId = 2, TaskName = "TaskB", TaskDescription = "Desc2", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 1, UserId = 1},
            new TaskModel() { TaskId = 3, TaskName = "TaskC", TaskDescription = "Desc3", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 1, UserId = 1},

            new TaskModel() { TaskId = 4, TaskName = "TaskD", TaskDescription = "Desc4", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 1, UserId = 2},
            new TaskModel() { TaskId = 5, TaskName = "TaskE", TaskDescription = "Desc5", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 1, UserId = 2},
            new TaskModel() { TaskId = 6, TaskName = "TaskF", TaskDescription = "Desc6", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 1, UserId = 2},
            
            new TaskModel() { TaskId = 7, TaskName = "TaskG", TaskDescription = "Desc7", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 2, UserId = 3},
            new TaskModel() { TaskId = 8, TaskName = "TaskH", TaskDescription = "Desc8", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 2, UserId = 3},
            new TaskModel() { TaskId = 9, TaskName = "TaskI", TaskDescription = "Desc9", StartDate = new DateTime(1995, 1, 1),
            DueDate = new DateTime(1996, 1, 1), Progress = 50, Flags = "no", Comments = "lots o typing", ProjectId = 2, UserId = 3},
        };
    }
} */