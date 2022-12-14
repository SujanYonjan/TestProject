USE [TestProject]
GO
/****** Object:  StoredProcedure [dbo].[spGetStudents]    Script Date: 8/14/2022 7:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spGetStudents]
(
	@StudentId int 
)
AS
BEGIN
	IF @StudentId>0
	BEGIN
		select * from Students where StudentId=@StudentId
	END
	ELSE
	BEGIN
	select * from Students
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spGetTeachers]    Script Date: 8/14/2022 7:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spGetTeachers]
(
	@TeacherId int 
)
AS
BEGIN
	IF @TeacherId>0
	BEGIN
		select * from Teachers where TeacherId=@TeacherId
	END
	ELSE
	BEGIN
	select * from Teachers
	END
END
GO
