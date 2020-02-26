using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
namespace Website.Models
{
    public class DatabaseModel
    {
        private SqlConnection preSqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;");
        private SqlConnection sqlCon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=TeamManagement;Integrated Security=True;");
        
        private static string tables = "IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Users')) BEGIN CREATE TABLE Users(UserID int Identity(1,1) PRIMARY KEY NOT NULL,Email varchar(100) NOT NULL,HashedPassword varchar(500) NOT NULL,FullName varchar(100) NOT NULL,AdminUser bit NOT NULL); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Projects')) BEGIN CREATE TABLE Projects(ProjectID int Identity(1,1) PRIMARY KEY NOT NULL,ProjectName varchar(100) NOT NULL,ProjectDescription varchar(300) NULL,StartDate DateTime NOT NULL,DueDate DateTime NOT NULL,AdminID int NOT NULL); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Tasks')) BEGIN CREATE TABLE Tasks(TaskID int Identity(1,1) PRIMARY KEY,TasksName varchar(100) NOT NULL,TasksDescription varchar(300) NOT NULL,StartDate DateTime NOT NULL,DueDate DateTime NOT NULL,Progress int NOT NULL,Comments varchar(200) NULL,ProjectID int NOT NULL,UserID int NOT NULL); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'Projects_AdminID_FK')) BEGIN Alter Table Projects add constraint Projects_AdminID_FK FOREIGN KEY (AdminID) REFERENCES Users(UserID); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Tasks_DueDate')) BEGIN Alter Table Tasks add constraint Tasks_ProjectID_FK FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'Tasks_ProjectID_FK')) BEGIN Alter Table Tasks add constraint Tasks_UserID_FK FOREIGN KEY (UserID) REFERENCES Users(UserID); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Project_StartDate')) BEGIN ALTER TABLE Projects ADD CONSTRAINT CK_Project_StartDate CHECK(StartDate > GetDate()); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Project_DueDate')) BEGIN ALTER TABLE Projects ADD CONSTRAINT CK_Project_DueDate CHECK(DueDate > StartDate); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Tasks_StartDate0')) BEGIN ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_StartDate0 CHECK(StartDate > GetDate()); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Tasks_DueDate0')) BEGIN ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_DueDate0 CHECK(DueDate > StartDate); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Tasks_StartDate')) BEGIN ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_StartDate CHECK(StartDate > dbo.udfCheckTasksDates(projectID,'StartDate')); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Tasks_DueDate')) BEGIN ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_DueDate CHECK(DueDate < dbo.udfCheckTasksDates(projectID,'DueDate')); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CK_Tasks_Progress')) BEGIN ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_Progress CHECK(Progress>=0 AND Progress <= 100); END" +
                            " IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'CheckIfEmailIsUnique')) BEGIN ALTER TABLE Users ADD CONSTRAINT CheckIfEmailIsUnique UNIQUE (Email); END";
        private static string udfCheckTasksDates = "CREATE FUNCTION udfCheckTasksDates(@projectID int,@param varchar(50)) RETURNS DateTime AS BEGIN DECLARE @Result datetime IF(@param = 'StartDate') SET @Result = (SELECT StartDate FROM Projects where projectID = @projectID) ELSE IF(@param = 'DueDate') SET @Result = (SELECT DueDate FROM Projects where projectID = @projectID) RETURN @Result END";
        private static string insertTaskProcedure = "CREATE OR ALTER PROCEDURE InsertTasks(@taskName varchar(100), @taskDescription varchar(200), @startDate datetime, @dueDate datetime,@progress int, @comments varchar(200), @projectId int, @userId int) AS BEGIN TRY BEGIN TRANSACTION INSERT INTO Tasks (TasksName,TasksDescription,StartDate,DueDate,Progress,Comments,ProjectID,UserID) VALUES ( @taskName,@taskDescription,@startDate,@dueDate,@progress,@comments,@projectId,@userId) COMMIT TRANSACTION END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() AS ErrorState, ERROR_PROCEDURE() AS ErrorProcedure, ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage; IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION END CATCH; IF @@TRANCOUNT > 0 COMMIT TRANSACTION";
        private static string insertProjectProcedure = "CREATE OR ALTER PROCEDURE InsertProject(@projectName varchar(100), @projectDescription varchar(200), @startDate datetime, @dueDate datetime, @adminId int) AS BEGIN TRY BEGIN TRANSACTION INSERT INTO Projects (ProjectName,ProjectDescription,StartDate,DueDate,AdminID) VALUES (@projectName,@projectDescription,@startDate,@dueDate,@adminId) COMMIT TRANSACTION END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() AS ErrorState, ERROR_PROCEDURE() AS ErrorProcedure, ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage; IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION END CATCH; IF @@TRANCOUNT > 0 COMMIT TRANSACTION";
        private static string insertUserProcedure= "CREATE OR ALTER PROCEDURE InsertUser(@email varchar(100), @password varchar(500), @fullName varchar(100), @admin bit) AS BEGIN TRY BEGIN TRANSACTION INSERT INTO Users (Email,HashedPassword,FullName,AdminUser) VALUES (@email,@password,@fullName,@admin) COMMIT TRANSACTION END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() AS ErrorState, ERROR_PROCEDURE() AS ErrorProcedure, ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage; IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION END CATCH; IF @@TRANCOUNT > 0 COMMIT TRANSACTION";
        public DatabaseModel()
        {
            SqlCommand createDB = new SqlCommand("If(db_id(N'TeamManagement') IS NULL) BEGIN CREATE DATABASE TeamManagement; END;", preSqlCon);
            SqlCommand checkDates = new SqlCommand(udfCheckTasksDates, sqlCon);
            SqlCommand createTables = new SqlCommand(tables, sqlCon);
            SqlCommand createInsertTaskProcedure = new SqlCommand(insertTaskProcedure, sqlCon);
            SqlCommand createInsertProjectProcedure = new SqlCommand(insertProjectProcedure, sqlCon);
            SqlCommand createInsertUserProcedure = new SqlCommand(insertUserProcedure, sqlCon);

            preSqlCon.Open();
            createDB.ExecuteNonQuery();
            sqlCon.Open();
            try
            {
                checkDates.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            createTables.ExecuteNonQuery();
            createInsertTaskProcedure.ExecuteNonQuery();
            createInsertProjectProcedure.ExecuteNonQuery();
            createInsertUserProcedure.ExecuteNonQuery();
            
        }

        public void addUser(UserModel register)
        {
            SqlCommand cmdUser = new SqlCommand("dbo.InsertUser", sqlCon);
            cmdUser.CommandType = CommandType.StoredProcedure;
            cmdUser.Parameters.AddWithValue("@email",register.Email);
            cmdUser.Parameters.AddWithValue("@password",register.Password);
            cmdUser.Parameters.AddWithValue("@fullName",register.FullName);
            cmdUser.Parameters.AddWithValue("@admin",register.Admin);
            cmdUser.ExecuteScalar();
        }

        public void addTask(TaskModel newTask)
        {
            SqlCommand cmdTask = new SqlCommand("dbo.InsertTasks", sqlCon);
            cmdTask.CommandType = CommandType.StoredProcedure;
            cmdTask.Parameters.AddWithValue("@taskName",newTask.TaskName);
            cmdTask.Parameters.AddWithValue("@taskDescription",newTask.TaskDescription);
            cmdTask.Parameters.AddWithValue("@startDate",newTask.StartDate);
            cmdTask.Parameters.AddWithValue("@dueDate",newTask.DueDate);
            cmdTask.Parameters.AddWithValue("@progress",newTask.Progress);
            cmdTask.Parameters.AddWithValue("@comments",newTask.Comments);
            cmdTask.Parameters.AddWithValue("@projectid",newTask.ProjectId);
            cmdTask.Parameters.AddWithValue("@userid",newTask.UserId);
            cmdTask.ExecuteScalar();
        }

        public void addProject(ProjectModel newProject)
        {
            SqlCommand cmdProject = new SqlCommand("dbo.InsertProject", sqlCon);
            cmdProject.CommandType = CommandType.StoredProcedure;
            cmdProject.Parameters.AddWithValue("@projectName",newProject.Name);
            cmdProject.Parameters.AddWithValue("@projectDescription",newProject.Description);
            cmdProject.Parameters.AddWithValue("@startDate",newProject.StartDate);
            cmdProject.Parameters.AddWithValue("@dueDate",newProject.DueDate);
            cmdProject.Parameters.AddWithValue("@adminid",newProject.AdminId);
            cmdProject.ExecuteScalar();
        }

        public UserModel findUser(string email)
        {
            UserModel user = new UserModel();
            SqlCommand findByEmail = new SqlCommand("SELECT * FROM dbo.Users WHERE email = @email", sqlCon);
            findByEmail.Parameters.AddWithValue("@email",email);

            using (SqlDataReader reader = findByEmail.ExecuteReader())
            {
                // Call Read before accessing data.
                if (reader.HasRows)
                {
                    reader.Read();
                    user.UserId = reader.GetInt32(0);
                    user.Email = reader.GetString(1);
                    user.Password = reader.GetString(2);
                    user.FullName = reader.GetString(3);
                    user.Admin = reader.GetBoolean(4);
                }
                else
                    user = null;
                reader.Close();
            }
            return user;
        }

        public ProjectModel getProject(int i)
        {
            ProjectModel project = new ProjectModel();
            SqlCommand findByID = new SqlCommand("SELECT * FROM dbo.Projects WHERE projectID = @pid", sqlCon);
            findByID.Parameters.AddWithValue("@pid",i);

            using (SqlDataReader reader = findByID.ExecuteReader())
            {
                // Call Read before accessing data.
                if (reader.HasRows)
                {
                    reader.Read();
                    project.Id = reader.GetInt32(0);
                    project.Name = reader.GetString(1);
                    project.Description = reader.GetString(2);
                    project.StartDate = reader.GetDateTime(3);
                    project.DueDate = reader.GetDateTime(4);
                    project.AdminId = reader.GetInt32(5);
                }
                else
                    project = null;
                reader.Close();
            }
            return project;
        }

    }
}