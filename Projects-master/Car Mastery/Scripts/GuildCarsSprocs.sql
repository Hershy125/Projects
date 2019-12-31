USE GuildCars;
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesSelectAll')
		DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT VehicleMakeId, VehicleMakeName, CreatedDate, UserId
	FROM VehicleMakes 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesItemsSelectAll')
		DROP PROCEDURE MakesItemsSelectAll
GO

CREATE PROCEDURE MakesItemsSelectAll AS
BEGIN
	SELECT v.VehicleMakeId, v.VehicleMakeName, v.CreatedDate, u.Email
	FROM VehicleMakes v
		INNER JOIN AspNetUsers u ON v.UserId = u.Id
	ORDER BY v.VehicleMakeName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesSelectById')
		DROP PROCEDURE MakesSelectById
GO

CREATE PROCEDURE MakesSelectById (
	@VehicleMakeId int
)
AS
BEGIN
	SELECT v.VehicleMakeId, v.VehicleMakeName, v.CreatedDate, u.Email
	FROM VehicleMakes v
		INNER JOIN AspNetUsers u ON v.UserId = u.Id
	WHERE v.VehicleMakeId = @VehicleMakeId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertVehicleMake')
		DROP PROCEDURE InsertVehicleMake
GO

CREATE PROCEDURE InsertVehicleMake (
	@VehicleMakeId int output,
	@VehicleMakeName nvarchar(20),
	@UserId nvarchar(128)
)
AS
BEGIN
	INSERT INTO VehicleMakes (VehicleMakeName, UserId)
	VALUES(@VehicleMakeName, @UserId)

	SET @VehicleMakeId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectAll')
		DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll AS
BEGIN
	SELECT VehicleMakeId, VehicleModelId, vehicleModelName,
	CreatedDate, UserId
	FROM VehicleModels 

END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsItemsSelectAll')
		DROP PROCEDURE ModelsItemsSelectAll
GO

CREATE PROCEDURE ModelsItemsSelectAll AS
BEGIN
	SELECT v.VehicleMakeId, m.VehicleMakeName, v.VehicleModelId, v.vehicleModelName,
	v.CreatedDate, u.Email
	FROM VehicleModels v
		INNER JOIN VehicleMakes m on v.VehicleMakeId = m.VehicleMakeId
		INNER JOIN AspNetUsers u ON v.UserId = u.Id
	ORDER BY m.VehicleMakeName, v.VehicleModelName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectById')
		DROP PROCEDURE ModelsSelectById
GO

CREATE PROCEDURE ModelsSelectById (
	@VehicleModelId int)
AS
BEGIN
	SELECT v.VehicleMakeId, m.VehicleMakeName, v.VehicleModelId, v.vehicleModelName,
	v.CreatedDate, u.Email
	FROM VehicleModels v
		INNER JOIN VehicleMakes m on v.VehicleMakeId = m.VehicleMakeId
		INNER JOIN AspNetUsers u ON v.UserId = u.Id
	WHERE v.VehicleModelId = @VehicleModelId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertVehicleModel')
		DROP PROCEDURE InsertVehicleModel
GO

CREATE PROCEDURE InsertVehicleModel (
	@VehicleModelId int output,
	@VehicleModelName nvarchar(20),
	@VehicleMakeId int,
	@UserId nvarchar(128)
)
AS
BEGIN
	INSERT INTO VehicleModels (VehicleModelName, VehicleMakeId, UserId)
	VALUES(@VehicleModelName, @VehicleMakeId, @UserId)

	SET @VehicleModelId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BodyStylesSelectAll')
		DROP PROCEDURE BodyStylesSelectAll
GO

CREATE PROCEDURE BodyStylesSelectAll AS
BEGIN
	SELECT BodyStyleId, BodyStyleName
	FROM BodyStyles
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ColorsSelectAll')
		DROP PROCEDURE ColorsSelectAll
GO

CREATE PROCEDURE ColorsSelectAll AS
BEGIN
	SELECT ColorId, ColorName
	FROM Colors
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehicleTypesSelectAll')
		DROP PROCEDURE VehicleTypesSelectAll
GO

CREATE PROCEDURE VehicleTypesSelectAll AS
BEGIN
	SELECT VehicleTypeId, VehicleTypeName
	FROM VehicleTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TransmissionTypesSelectAll')
		DROP PROCEDURE TransmissionTypesSelectAll
GO

CREATE PROCEDURE TransmissionTypesSelectAll AS
BEGIN
	SELECT TransmissionTypeId, TransmissionTypeName
	FROM TransmissionTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertContactRequest')
		DROP PROCEDURE InsertContactRequest
GO

CREATE PROCEDURE InsertContactRequest (
	@ContactRequestId int output,
	@Name nvarchar(50),
	@Email nvarchar(50),
	@Phone nvarchar(12),
	@Message nvarchar(300)
)
AS
BEGIN
	INSERT INTO ContactRequests ([Name], Email, Phone, [Message])
	VALUES(@Name, @Email, @Phone, @Message)

	SET @ContactRequestId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactRequestsSelectAll')
		DROP PROCEDURE ContactRequestsSelectAll
GO

CREATE PROCEDURE ContactRequestsSelectAll AS
BEGIN
	SELECT ContactRequestId, [Name], Email, Phone, [Message]
	FROM ContactRequests
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersSelectAll')
		DROP PROCEDURE CustomersSelectAll
GO

CREATE PROCEDURE CustomersSelectAll AS
BEGIN
	SELECT CustomerId, [Name], Phone, Email, Street1, Street2, City, [State], ZipCode
	FROM Customers
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertCustomer')
		DROP PROCEDURE InsertCustomer
GO

CREATE PROCEDURE InsertCustomer (
	@CustomerId int output,
	@Name nvarchar(50),
	@Email nvarchar(50),
	@Phone nvarchar(12),
	@Street1 nvarchar(50),
	@Street2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(15),
	@ZipCode int
)
AS
BEGIN
	INSERT INTO Customers ([Name], Email, Phone, Street1, Street2, City, [State], ZipCode)
	VALUES(@Name, @Email, @Phone, @Street1, @Street2, @City, @State, @ZipCode)

	SET @CustomerId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchasesSelectAll')
		DROP PROCEDURE PurchasesSelectAll
GO

CREATE PROCEDURE PurchasesSelectAll AS
BEGIN
	SELECT PurchaseId, VehicleListingId, CustomerId, UserId, PurchasePrice, PurchaseTypeId
	FROM Purchases
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchasesSelectByUser')
		DROP PROCEDURE PurchasesSelectByUser
GO

CREATE PROCEDURE PurchasesSelectByUser (
	@UserId nvarchar(128)
)
AS
BEGIN
	SELECT PurchaseId, VehicleListingId, CustomerId, UserId, PurchasePrice, PurchaseTypeId
	FROM Purchases
	WHERE UserId = @UserId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertPurchase')
		DROP PROCEDURE InsertPurchase
GO

CREATE PROCEDURE InsertPurchase (
	@PurchaseId int output,
	@VehicleListingId int,
	@CustomerId int,
	@UserId nvarchar(128),
	@PurchasePrice decimal(9,2),
	@PurchaseTypeId int

)
AS
BEGIN
	INSERT INTO Purchases (VehicleListingId, CustomerId, UserId, PurchasePrice, PurchaseTypeId)
	VALUES(@VehicleListingId, @CustomerId, @UserId, @PurchasePrice, @PurchaseTypeId)

	SET @PurchaseId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchaseTypesSelectAll')
		DROP PROCEDURE PurchaseTypesSelectAll
GO

CREATE PROCEDURE PurchaseTypesSelectAll AS
BEGIN
	SELECT PurchaseTypeId, PurchaseTypeName
	FROM PurchaseTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsSelectAll')
		DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT SpecialId, SpecialTitle, SpecialDetails
	FROM Specials
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertSpecial')
		DROP PROCEDURE InsertSpecial
GO

CREATE PROCEDURE InsertSpecial (
	@SpecialId int output,
	@SpecialTitle nvarchar(50),
	@SpecialDetails nvarchar(300)
)
AS
BEGIN
	INSERT INTO Specials (SpecialTitle, SpecialDetails)
	VALUES(@SpecialTitle, @SpecialDetails)

	SET @SpecialId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteSpecial')
		DROP PROCEDURE DeleteSpecial
GO

CREATE PROCEDURE DeleteSpecial (
	@SpecialId int
)
AS
BEGIN
	DELETE FROM Specials
	WHERE SpecialId = @SpecialId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteVehicleListing')
		DROP PROCEDURE DeleteVehicleListing
GO

CREATE PROCEDURE DeleteVehicleListing (
	@VehicleListingId int
)
AS
BEGIN
	DELETE FROM VehicleListings
	WHERE VehicleListingId = @VehicleListingId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingsSelectById')
		DROP PROCEDURE ListingsSelectById
GO

CREATE PROCEDURE ListingsSelectById (
	@VehicleListingId int
)
AS
BEGIN
	SELECT VehicleListingId, VehicleMakeId, VehicleModelId, VehicleTypeId, BodyStyleId, TransmissionTypeId, [Year], ColorId, InteriorColorId,
	Mileage, VIN, MSRP, SalePrice, [Description], FeaturedVehicle, ImageFileName, Sold
	FROM VehicleListings
	WHERE VehicleListingId = @VehicleListingId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InsertVehicleListing')
		DROP PROCEDURE InsertVehicleListing
GO

CREATE PROCEDURE InsertVehicleListing (
	@VehicleListingId int output,
	@VehicleMakeId int,
	@VehicleModelId int,
	@VehicleTypeId int,
	@BodyStyleId int,
	@TransmissionTypeId int,
	@Year int,
	@ColorId int,
	@InteriorColorId int,
	@Mileage int,
	@VIN nvarchar(17),
	@MSRP decimal(9,2),
	@SalePrice decimal(9,2),
	@Description nvarchar(300),
	@FeaturedVehicle bit,
	@ImageFileName varchar(50),
	@Sold bit
)
AS
BEGIN
	INSERT INTO VehicleListings (VehicleMakeId, VehicleModelId, VehicleTypeId, BodyStyleId, TransmissionTypeId, [Year], ColorId, InteriorColorId,
	Mileage, VIN, MSRP, SalePrice, [Description], FeaturedVehicle, ImageFileName, Sold)
	VALUES(@VehicleMakeId, @VehicleModelId, @VehicleTypeId, @BodyStyleId, @TransmissionTypeId, @Year, @ColorId, @InteriorColorId, @Mileage,
	@VIN, @MSRP, @SalePrice, @Description, @FeaturedVehicle, @ImageFileName, @Sold)

	SET @VehicleListingId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateVehicleListing')
		DROP PROCEDURE UpdateVehicleListing
GO

CREATE PROCEDURE UpdateVehicleListing (
	@VehicleListingId int,
	@VehicleMakeId int,
	@VehicleModelId int,
	@VehicleTypeId int,
	@BodyStyleId int,
	@TransmissionTypeId int,
	@Year int,
	@ColorId int,
	@InteriorColorId int,
	@Mileage int,
	@VIN nvarchar(17),
	@MSRP decimal(9,2),
	@SalePrice decimal(9,2),
	@Description nvarchar(300),
	@FeaturedVehicle bit,
	@ImageFileName varchar(50),
	@Sold bit
)
AS
BEGIN
	UPDATE VehicleListings SET 
		VehicleMakeId = @VehicleMakeId, 
		VehicleModelId = @VehicleModelId, 
		VehicleTypeId = @VehicleTypeId, 
		BodyStyleId = @BodyStyleId, 
		TransmissionTypeId = @TransmissionTypeId, 
		[Year] = @Year, 
		ColorId = @ColorId, 
		InteriorColorId = @InteriorColorId,
		Mileage = @Mileage, 
		VIN = @VIN, 
		MSRP = @MSRP, 
		SalePrice = @SalePrice, 
		[Description] = @Description, 
		FeaturedVehicle = @FeaturedVehicle, 
		ImageFileName = @ImageFileName, 
		Sold = @Sold
	WHERE VehicleListingId = @VehicleListingId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehicleDetailsById')
		DROP PROCEDURE VehicleDetailsById
GO

CREATE PROCEDURE VehicleDetailsById (
	@VehicleListingId int
)
AS
BEGIN
	SELECT VehicleListingId, [Year], vma.VehicleMakeId, vma.VehicleMakeName, vmo.VehicleModelId, vmo.VehicleModelName, b.BodyStyleId, b.BodyStyleName, t.TransmissionTypeId, t.TransmissionTypeName,
		SalePrice, Mileage, MSRP, vl.InteriorColorId, ic.ColorName as InteriorColor, c.ColorId, c.ColorName, VIN, ImageFileName, [Description], Sold
	FROM VehicleListings vl
		INNER JOIN VehicleMakes vma ON vl.VehicleMakeId = vma.VehicleMakeId
		INNER JOIN VehicleModels vmo ON vl.VehicleModelId = vmo.VehicleModelId
		INNER JOIN BodyStyles b ON vl.BodyStyleId = b.BodyStyleId
		INNER JOIN TransmissionTypes t ON vl.TransmissionTypeId = t.TransmissionTypeId
		INNER JOIN Colors ic ON vl.InteriorColorId = ic.ColorId
		INNER JOIN Colors c ON vl.ColorId = c.ColorId
	WHERE VehicleListingId = @VehicleListingId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FeaturedSelect')
		DROP PROCEDURE FeaturedSelect
GO

CREATE PROCEDURE FeaturedSelect AS
BEGIN
	SELECT TOP 8 VehicleListingId, [Year], vma.VehicleMakeId, vma.VehicleMakeName, vmo.VehicleModelId, vmo.VehicleModelName,
		SalePrice, ImageFileName
	FROM VehicleListings vl
		INNER JOIN VehicleMakes vma ON vl.VehicleMakeId = vma.VehicleMakeId
		INNER JOIN VehicleModels vmo ON vl.VehicleModelId = vmo.VehicleModelId
	WHERE FeaturedVehicle = 1
	ORDER BY SalePrice DESC
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InventoryReport')
		DROP PROCEDURE InventoryReport
GO

CREATE PROCEDURE InventoryReport (
	@VehicleTypeId int
)
AS
BEGIN
	SELECT [Year], ma.VehicleMakeName, mo.VehicleModelName, COUNT(MSRP) as [Count], SUM(MSRP) as 'StockValue'
	FROM VehicleListings v
		INNER JOIN VehicleMakes ma ON v.VehicleMakeId = ma.VehicleMakeId
		INNER JOIN VehicleModels mo ON v.VehicleModelId = mo.VehicleModelId
	WHERE VehicleTypeId = @VehicleTypeId AND Sold = 0
	GROUP BY ma.VehicleMakeName, mo.VehicleModelName, [Year]
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectByMakeId')
		DROP PROCEDURE ModelsSelectByMakeId
GO

CREATE PROCEDURE ModelsSelectByMakeId (
	@VehicleMakeId int)
AS
BEGIN
	SELECT v.VehicleMakeId, m.VehicleMakeName, v.VehicleModelId, v.vehicleModelName,
	v.CreatedDate, u.Email
	FROM VehicleModels v
		INNER JOIN VehicleMakes m on v.VehicleMakeId = m.VehicleMakeId
		INNER JOIN AspNetUsers u ON v.UserId = u.Id
	WHERE v.VehicleMakeId = @VehicleMakeId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersSelectByEmail')
		DROP PROCEDURE CustomersSelectByEmail
GO

CREATE PROCEDURE CustomersSelectByEmail (
	@Email nvarchar(50))
AS
BEGIN
	SELECT CustomerId, [Name], Phone, Email, Street1, Street2, City, [State], ZipCode
	FROM Customers
	WHERE @Email = Email
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UsersSelectAll')
		DROP PROCEDURE UsersSelectAll
GO

CREATE PROCEDURE UsersSelectAll AS
BEGIN
	SELECT LastName, FirstName, Email, [Role], Id as UserId
	FROM AspNetUsers
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UsersSelectById')
		DROP PROCEDURE UsersSelectById
GO

CREATE PROCEDURE UsersSelectById (
	@UserId nvarchar(128) )
AS
BEGIN
	SELECT LastName, FirstName, Email, [Role], Id as UserId
	FROM AspNetUsers
	WHERE @UserId = Id
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'UpdateUser')
		DROP PROCEDURE UpdateUser
GO

CREATE PROCEDURE UpdateUser (
	@UserId nvarchar(128),
	@FirstName nvarchar(max),
	@LastName nvarchar(max),
	@Email nvarchar(256),
	@Role nvarchar(max)
)
AS
BEGIN
	UPDATE AspNetUsers SET 
		FirstName = @FirstName, 
		LastName = @LastName, 
		Email = @Email, 
		[Role] = @Role 
	WHERE Id = @UserId
END

GO