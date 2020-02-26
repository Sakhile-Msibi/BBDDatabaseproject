--Create Database
CREATE DATABASE TeamManagement;
GO

--Create Users Table
CREATE TABLE Users(
UserID int Identity(1,1) PRIMARY KEY NOT NULL,
Email varchar(100) NOT NULL,
HashedPassword varchar(50) NOT NULL,
FullName varchar(100) NOT NULL,
AdminUser bit NOT NULL
);
GO

--Create Projects Table
CREATE TABLE Projects(
ProjectID int Identity(1,1) PRIMARY KEY NOT NULL,
ProjectName varchar(100) NOT NULL,
ProjectDescription varchar(300) NULL,
StartDate DateTime NOT NULL,
DueDate DateTime NOT NULL,
AdminID int NOT NULL
);
GO

--Create Tasks Table
CREATE TABLE Tasks(
TaskID int Identity(1,1) PRIMARY KEY,
TasksName varchar(100) NOT NULL,
TasksDescription varchar(300) NOT NULL,
StartDate DateTime NOT NULL,
DueDate DateTime NOT NULL,
Progress int NOT NULL,
Comments varchar(200) NULL,
ProjectID int NOT NULL,
UserID int NOT NULL
);
GO

--Adding Foreign Key 
Alter Table Projects add constraint Projects_AdminID_FK FOREIGN KEY (AdminID) REFERENCES Users(UserID);
Alter Table Tasks add constraint Tasks_ProjectID_FK FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID);
Alter Table Tasks add constraint Tasks_UserID_FK FOREIGN KEY (UserID) REFERENCES Users(UserID);
GO

--Add Check constraints for project dates.
ALTER TABLE Projects ADD CONSTRAINT CK_Project_StartDate CHECK(StartDate > GetDate());
ALTER TABLE Projects ADD CONSTRAINT CK_Project_DueDate CHECK(DueDate > StartDate);
GO

--Make a Function that ensures that tasks dates are within range of its respective project start and due date.
CREATE FUNCTION udfCheckTasksDates(@projectID int,@param varchar(50)) 
RETURNS DateTime
AS
BEGIN 
DECLARE @Result datetime
IF(@param = 'StartDate')
SET @Result = (SELECT StartDate FROM Projects where projectID = @projectID)
ELSE IF(@param = 'DueDate')
SET @Result = (SELECT DueDate FROM Projects where projectID = @projectID)
RETURN @Result
END
GO

--Add check contraint for tasks dates.
ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_StartDate CHECK(StartDate > dbo.udfCheckTasksDates(projectID,'StartDate'));
ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_DueDate CHECK(DueDate < dbo.udfCheckTasksDates(projectID,'DueDate'));
GO

--Add CHeck constraint for progress in tasks
ALTER TABLE Tasks ADD CONSTRAINT CK_Tasks_Progress CHECK(Progress>=0 AND Progress <= 100)
GO

--Add a stored procedure for Tasks
CREATE PROCEDURE InsertTasks(@taskName varchar(100), @taskDescription varchar(200), @startDate datetime, @dueDate datetime,@progress int, @comments varchar(200), 
@projectId int, @userId int)
AS
BEGIN TRY
BEGIN TRANSACTION
INSERT INTO Tasks (TasksName,TasksDescription,StartDate,DueDate,Progress,Comments,ProjectID,UserID) VALUES ( @taskName,@taskDescription,@startDate,@dueDate,@progress,@comments,@projectId,@userId)
COMMIT TRANSACTION
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION
END CATCH;
IF @@TRANCOUNT > 0
	COMMIT TRANSACTION
GO

--Add a stored procedure for projects
CREATE PROCEDURE InsertProject(@projectName varchar(100), @projectDescription varchar(200), @startDate datetime, @dueDate datetime, @adminId int)
AS
BEGIN TRY
BEGIN TRANSACTION
INSERT INTO Projects (ProjectName,ProjectDescription,StartDate,DueDate,AdminID) VALUES (@projectName,@projectDescription,@startDate,@dueDate,@adminId)
COMMIT TRANSACTION
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION
END CATCH;
IF @@TRANCOUNT > 0
	COMMIT TRANSACTION
GO

--Add a stored procedure for user
CREATE PROCEDURE InsertUser(@email varchar(100), @password varchar(50), @fullName varchar(100), @admin bit)
AS
BEGIN TRY
BEGIN TRANSACTION
INSERT INTO Users (Email,HashedPassword,FullName,AdminUser) VALUES (@email,@password,@fullName,@admin)
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION
END CATCH;
IF @@TRANCOUNT > 0
	COMMIT TRANSACTION
GO

--Add unique constraint for emails
ALTER TABLE Users 
ADD CONSTRAINT  CheckIfEmailIsUnique UNIQUE (Email)
GO

--Function to return all completed tasks for specific project and users.
CREATE VIEW userCompletedTasks
AS 
SELECT TasksName 
FROM Tasks
WHERE Progress = 100
GO

--Creating a view to see incompleted tasks by users.
CREATE VIEW userIncompletedTasks
AS 
SELECT TasksName 
FROM Tasks
WHERE Progress != 100
GO



-- Aggregade function that conts all the tasks which are completed for a specific project
CREATE FUNCTION CountCompletedTasks (@ProjectID int, @UserID int)
RETURNS TABLE
AS
RETURN SELECT TasksName
FROM Tasks
WHERE Progress = 100  AND ProjectID = @ProjectID AND UserID = @UserID
GO

CREATE FUNCTION CountIncompleteTasks (@ProjectID int, @UserID int)
RETURNS TABLE
AS
RETURN SELECT TasksName
FROM Tasks
WHERE Progress != 100  AND ProjectID = @ProjectID AND UserID = @UserID
GO

SELECT  COUNT(*) FROM CountCompletedTasks(5, 8)
GO

