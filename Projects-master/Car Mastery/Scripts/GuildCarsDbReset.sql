Use GuildCars;
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM ContactRequests;
	DELETE FROM Purchases;
	DELETE FROM Customers;
	DELETE FROM PurchaseTypes;
	DELETE FROM Specials;
	DELETE FROM VehicleListings;
	DELETE FROM Colors;
	DELETE FROM VehicleModels;
	DELETE FROM VehicleMakes;
	DELETE FROM VehicleTypes;
	DELETE FROM BodyStyles;
	DELETE FROM TransmissionTypes;
	DELETE FROM AspNetUsers;

	DBCC CHECKIDENT ('VehicleListings', RESEED, 1);
	DBCC CHECKIDENT ('VehicleMakes', RESEED, 1);
	DBCC CHECKIDENT ('VehicleModels', RESEED, 1);
	DBCC CHECKIDENT ('Specials', RESEED, 1);
	DBCC CHECKIDENT ('Customers', RESEED, 1);
	DBCC CHECKIDENT ('Purchases', RESEED, 1);
	DBCC CHECKIDENT ('ContactRequests', RESEED, 1);

	SET IDENTITY_INSERT BodyStyles ON
	INSERT INTO BodyStyles(BodyStyleId, BodyStyleName)
	VALUES(1, 'Convertible'),
	(2, 'Hatchback'),
	(3, 'Minivan'),
	(4, 'Sedan'),
	(5, 'SUV')
	SET IDENTITY_INSERT BodyStyles OFF

	SET IDENTITY_INSERT TransmissionTypes ON
	INSERT INTO TransmissionTypes(TransmissionTypeId, TransmissionTypeName)
	VALUES(1, 'Automatic'),
	(2, 'Manual')
	SET IDENTITY_INSERT TransmissionTypes OFF

	SET IDENTITY_INSERT VehicleTypes ON
	INSERT INTO VehicleTypes(VehicleTypeId, VehicleTypeName)
	VALUES(1, 'New'),
	(2, 'Used')
	SET IDENTITY_INSERT VehicleTypes OFF

	INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, FirstName, LastName, 
	TwoFactorEnabled, LockOutEnabled, AccessFailedCount, UserName, [Role])
	VALUES('00000000-0000-0000-0000-000000000000', 0, 0, 'bob@cars.com', 'Bob', 'Smith', 0, 0, 0, 'test', 'Sales');

	SET IDENTITY_INSERT VehicleMakes ON
	INSERT INTO VehicleMakes(VehicleMakeId, VehicleMakeName, UserId)
	VALUES(1, 'Ford', '00000000-0000-0000-0000-000000000000'),
	(2, 'Kia', '00000000-0000-0000-0000-000000000000'),
	(3, 'BMW', '00000000-0000-0000-0000-000000000000'),
	(4, 'Dodge', '00000000-0000-0000-0000-000000000000'),
	(5, 'Toyota', '00000000-0000-0000-0000-000000000000')
	SET IDENTITY_INSERT VehicleMakes OFF

	SET IDENTITY_INSERT VehicleModels ON
	INSERT INTO VehicleModels(VehicleMakeId, VehicleModelId, VehicleModelName, UserId)
	VALUES(1, 1, 'Explorer', '00000000-0000-0000-0000-000000000000'),
	(2, 2, 'Soul', '00000000-0000-0000-0000-000000000000'),
	(3, 3, 'Z3', '00000000-0000-0000-0000-000000000000'),
	(4, 4, 'Ram 1500', '00000000-0000-0000-0000-000000000000'),
	(5, 5, 'Carolla', '00000000-0000-0000-0000-000000000000')
	SET IDENTITY_INSERT VehicleModels OFF

	SET IDENTITY_INSERT Colors ON
	INSERT INTO Colors(ColorId, ColorName)
	VALUES(1, 'Black'),
	(2, 'Blue'),
	(3, 'White'),
	(4, 'Beige'),
	(5, 'Gray')
	SET IDENTITY_INSERT Colors OFF

	SET IDENTITY_INSERT VehicleListings ON
	INSERT INTO VehicleListings(VehicleListingId, VehicleMakeId, VehicleModelId, VehicleTypeId, BodyStyleId,
	TransmissionTypeId, [Year], ColorId, InteriorColorId, Mileage, VIN, MSRP, SalePrice, [Description],
	FeaturedVehicle, ImageFileName, Sold)
	VALUES(1, 1, 1, 1, 5, 1, 2018, 2, 4, 1000, '1HGBH41JXMN109186', 39000, 36500, 'test description', 1, 'placeholder.png', 0),
	(2, 2, 2, 1, 5, 1, 2018, 2, 4, 1000, '1HGBH41JXMN109186', 19000, 16500, 'test car 2', 1, 'placeholder.png', 0),
	(3, 3, 3, 1, 5, 1, 2018, 2, 4, 1000, '1HGBH41JXMN109186', 29000, 26500, 'test car 3', 1, 'placeholder.png', 0),
	(4, 4, 4, 1, 5, 1, 2018, 2, 4, 100, '1HGBH41JXMN109186', 59000, 48500, 'sold test car', 1, 'placeholder.png', 1),
	(5, 1, 1, 1, 1, 1, 2014, 1, 2, 200, '1HGBH41JXMN109186', 39000, 36500, 'test car 5', 1, 'placeholder.png', 0),
	(6, 2, 2, 2, 2, 1, 2015, 2, 3, 2000, '1HGBH41JXMN109186', 19000, 16500, 'test car 6', 1, 'placeholder.png', 0),
	(7, 3, 3, 2, 3, 1, 2016, 3, 4, 3000, '1HGBH41JXMN109186', 29000, 26500, 'test car 7', 1, 'placeholder.png', 0),
	(8, 4, 4, 2, 4, 1, 2017, 4, 1, 4000, '1HGBH41JXMN109186', 59000, 48500, 'test car 8', 1, 'placeholder.png', 1)
	SET IDENTITY_INSERT VehicleListings OFF

	SET IDENTITY_INSERT Specials ON
	INSERT INTO Specials(SpecialId, SpecialTitle, SpecialDetails)
	VALUES(1, 'A Great Special', 'this special is really great'),
	(2, 'A pretty good special', 'this special is pretty good')
	SET IDENTITY_INSERT Specials OFF

	SET IDENTITY_INSERT PurchaseTypes ON
	INSERT INTO PurchaseTypes(PurchaseTypeId, PurchaseTypeName)
	VALUES(1, 'Dealer Financing'),
	(2, 'Bank Financing'),
	(3, 'Cash')
	SET IDENTITY_INSERT PurchaseTypes OFF

	SET IDENTITY_INSERT Customers ON
	INSERT INTO Customers(CustomerId, [Name], Phone, Email, Street1, Street2, City, [State], ZipCode)
	VALUES(1, 'Test Buyer', '111-111-1111', 'tester@test.com', '123 state street', 'apt 2', 'Union', 'NJ', '07930')
	SET IDENTITY_INSERT Customers OFF

	SET IDENTITY_INSERT Purchases ON
	INSERT INTO Purchases(PurchaseId, VehicleListingId, CustomerId, UserId, PurchasePrice, PurchaseTypeId)
	VALUES(1, 4, 1, '00000000-0000-0000-0000-000000000000', 45500, 2),
	(2, 3, 1, '00000000-0000-0000-0000-000000000000', 25500, 1)
	SET IDENTITY_INSERT Purchases OFF

	SET IDENTITY_INSERT ContactRequests ON
	INSERT INTO ContactRequests(ContactRequestId, [Name], Email, Phone, [Message])
	VALUES(1, 'Joe Bob', 'joe@bob.com', '111-111-1111', 'test message 1')
	SET IDENTITY_INSERT ContactRequests OFF

END

GO