if OBJECT_ID(N'dbo.SiteUser', N'U') is NULL
BEGIN
	create table dbo.SiteUser (
		id	int	IDENTITY(1,1)	primary key,
		login	nvarchar(64)	not null,
		password	nvarchar(64)	not null,
		userType	char(1)		not null,
		emailAddress	nvarchar(100)	not null,
		homeNumber	nvarchar(11),
		workNumber	nvarchar(11),
		mobileNumber	nvarchar(13),
		firstName	nvarchar(32),
		lastName	nvarchar(32),
		streetAddress	nvarchar(64),
		suburb		nvarchar(24),
		city		nvarchar(16),
		isDisabled	bit		DEFAULT	0
		);
END

if OBJECT_ID(N'dbo.CustomerOrder', N'U') is NULL
BEGIN
	create table dbo.CustomerOrder (
		id	int	IDENTITY(1,1)	primary key,
		userId	int	not null Foreign Key References dbo.SiteUser(id),
		status	nvarchar(7)	not null DEFAULT 'waiting'
		);
END

if OBJECT_ID(N'dbo.Supplier', N'U') is NULL
BEGIN
	create table dbo.Supplier (
		id	int	IDENTITY(1,1)	primary key,
		name	nvarchar(32)	not null,
		contactNumber	nvarchar(11)	not null,
		emailAddress	nvarchar(64)	not null
	);
END

if OBJECT_ID(N'dbo.Category', N'U') is NULL
BEGIN
	create table dbo.Category (
		id	int	IDENTITY(1,1)	primary key,
		name	nvarchar(40)	not null
	);
END

if OBJECT_ID(N'dbo.Colour', N'U') is NULL
BEGIN
	create table dbo.Colour (
		id	int	IDENTITY(1,1)	primary key,
		name	nvarchar(24)	not null
	);
END

if OBJECT_ID(N'dbo.Cap', N'U') is NULL
BEGIN
	create table dbo.Cap (
		id	int	IDENTITY(1,1)	primary key,
		name	nvarchar(40)	not null,
		price	money		not null,
		description	nvarchar(512)	not null,
		imageUrl	nvarchar(96)	not null,
		supplierId	int		not null	Foreign Key References dbo.Supplier(id),
		categoryId	int		not null	Foreign Key References dbo.Category(id)
	);
END

if OBJECT_ID(N'dbo.OrderItem', N'U') is NULL
BEGIN
	create table dbo.OrderItem (
		orderId		int		not null	Foreign Key References dbo.CustomerOrder(id),
		capId		int		not null	Foreign Key References dbo.Cap(id),
		colourId	int		not null	Foreign Key References dbo.Colour(id),
		quantity	int		not null,
		Constraint	orderItem_pk	Primary Key (colourId, capId, orderId)
	);
END

if (select count(id) from dbo.SiteUser) = 0
BEGIN
	insert into SiteUser (login, password, userType, emailAddress) Values ('AdminRolandAskew2016','password','A', 'AskewR04@myunitec.ac.nz');
END

if (select count(id) from dbo.Colour) = 0
BEGIN
	insert into colour (name) values ('Black'), ('White'), ('Blue'), ('Green'), ('Red'), ('Pink'), ('Yellow'), ('Orange'), ('Grey');
END

if (select count(id) from dbo.Category) = 0
BEGIN
	insert into category (name) values ('Business Caps'), ('Women''s Caps'), ('Men''s Caps'), ('Children''s Caps');
END





