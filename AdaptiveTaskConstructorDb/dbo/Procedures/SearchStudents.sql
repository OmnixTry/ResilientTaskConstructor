CREATE PROCEDURE [dbo].[SearchStudents]
	@name NVARCHAR(MAX)
AS
	SELECT u.Id, u.FirstName, U.LastName, u.Email FROM AspNetUsers u
	LEFT JOIN AspNetUserRoles ur ON U.Id = ur.UserId
	LEFT JOIN AspNetRoles r ON ur.RoleId = r.Id
	WHERE r.[Name] = 'Student' AND U.Email LIKE '%' + @name + '%'
RETURN 0
