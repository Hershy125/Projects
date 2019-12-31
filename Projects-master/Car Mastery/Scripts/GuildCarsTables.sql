USE GuildCars;
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ContactRequests')
	DROP TABLE ContactRequests
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Purchases')
	DROP TABLE Purchases
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Customers')
	DROP TABLE Customers
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseTypes')
	DROP TABLE PurchaseTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleListings')
	DROP TABLE VehicleListings
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Colors')
	DROP TABLE Colors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleModels')
	DROP TABLE VehicleModels
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleMakes')
	DROP TABLE VehicleMakes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleTypes')
	DROP TABLE VehicleTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyles')
	DROP TABLE BodyStyles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='TransmissionTypes')
	DROP TABLE TransmissionTypes
GO

CREATE TABLE TransmissionTypes (
	TransmissionTypeId int identity(1,1) not null primary key,
	TransmissionTypeName varchar(10)
)

CREATE TABLE BodyStyles (
	BodyStyleId int identity(1,1) not null primary key,
	BodyStyleName nvarchar(15) not null
)

CREATE TABLE VehicleTypes (
	VehicleTypeId int identity(1,1) not null primary key,
	VehicleTypeName varchar(4) not null
)

CREATE TABLE VehicleMakes (
	VehicleMakeId int identity(1,1) not null primary key,
	VehicleMakeName nvarchar(20) not null,
	CreatedDate datetime2 not null default(getdate()),
	UserId nvarchar(128) not null
)

CREATE TABLE VehicleModels (
	VehicleModelId int identity(1,1) not null primary key,
	VehicleModelName nvarchar(20) not null,
	VehicleMakeId int not null foreign key references VehicleMakes(VehicleMakeId),
	CreatedDate datetime2 not null default(getdate()),
	UserId nvarchar(128) not null
)

CREATE TABLE Colors (
	ColorId int identity(1,1) not null primary key,
	ColorName nvarchar(30) not null
)

CREATE TABLE VehicleListings (
	VehicleListingId int identity(1,1) not null primary key,
	VehicleMakeId int not null foreign key references VehicleMakes(VehicleMakeId),
	VehicleModelId int not null foreign key references VehicleModels(VehicleModelId),
	VehicleTypeId int not null foreign key references VehicleTypes(VehicleTypeId),
	BodyStyleId int not null foreign key references BodyStyles(BodyStyleId),
	TransmissionTypeId int not null foreign key references TransmissionTypes(TransmissionTypeId),
	[Year] int not null,
	ColorId int not null foreign key references Colors(ColorId),
	InteriorColorId int not null foreign key references Colors(ColorId),
	Mileage int not null,
	VIN nvarchar(17) not null,
	MSRP decimal(9,2) not null,
	SalePrice decimal(9,2) not null,
	[Description] nvarchar(300) null,
	FeaturedVehicle bit not null default 0,
	ImageFileName varchar(50) null,
	Sold bit not null default 0,
	CreatedDate datetime2 not null default(getdate())
)

CREATE TABLE Specials (
	SpecialId int identity(1,1) not null primary key,
	SpecialTitle nvarchar(50),
	SpecialDetails nvarchar(300) not null,
)

CREATE TABLE PurchaseTypes (
	PurchaseTypeId int identity(1,1) not null primary key,
	PurchaseTypeName varchar(20) not null
)

CREATE TABLE Customers (
	CustomerId int identity(1,1) not null primary key,
	[Name] nvarchar(40) not null,
	Phone nvarchar(12) not null,
	Email nvarchar(50) not null,
	Street1 varchar(50) not null,
	Street2 varchar(50) null,
	City nvarchar(50) not null,
	[State] varchar(15) not null,
	ZipCode varchar(5) not null,
)

CREATE TABLE Purchases (
	PurchaseId int identity(1,1) not null primary key,
	VehicleListingId int not null foreign key references VehicleListings(VehicleListingId),
	CustomerId int not null foreign key references Customers(CustomerId),
	UserId nvarchar(128) not null,
	PurchasePrice decimal(9,2) not null,
	PurchaseTypeId int not null foreign key references PurchaseTypes(PurchaseTypeId),
	CreatedDate datetime2 not null default(getdate())
)

CREATE TABLE ContactRequests (
	ContactRequestId int identity(1,1) not null primary key,
	[Name] nvarchar(50) not null,
	Email nvarchar(50) null,
	Phone nvarchar(12) null,
	[Message] nvarchar(300) not null
)





	

	
