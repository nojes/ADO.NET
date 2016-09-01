USE ShopAdo;
GO

CREATE PROCEDURE dbo.InsertCategory
	@CategoryName nvarchar(100),
	@CategoryId int OUTPUT
AS

BEGIN

	INSERT INTO Category(CategoryName)
	VALUES (@CategoryName);
	
	SET @CategoryId = IDENT_CURRENT('dbo.Category') --SCOPE_IDENTITY();
END