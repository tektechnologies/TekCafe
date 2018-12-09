/* Check if database exists and if it does then delete it and rerun table creates. */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'tekCafeDbPM')
BEGIN
	DROP DATABASE [tekCafeDbPM]
	print '' print '*** Dropping database tekCafe_DbPM'
END
GO
/* Create the Database. */

print '' print '*** Creating database tekCafe_DbPM'
GO
CREATE DATABASE [tekCafeDbPM]
GO
/* Using DB. */

print '' print '*** Using database tekCafeDbPM'
GO
USE [tekCafeDbPM]
GO
/* Creates Employee Table. */

print '' print '*** Creating the Employee/Barista/Developer table password newdev'
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID]		[int] IDENTITY(100000, 1) 	NOT NULL,
	[FirstName]			[nvarchar](50)				NOT NULL,
	[LastName]			[nvarchar](100)				NOT NULL,
	[PhoneNumber]		[nvarchar](11)				NOT NULL,
	[Email]				[nvarchar](255)				NOT NULL,
	[Commission]		[decimal](5)				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL DEFAULT 
	'c3474dd290785b4948f65bae90c83f87',
	[Active]			[bit]						NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [fk_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** Inserting Employee  records'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [Commission])
	VALUES
	    ('Steve', 'Case', '13194200420', 'aolSteve@tekcafe.com', 20),
		('Jeff', 'Bozos', '13198675309', 'jeff@tekcafe.com', 20),
		('Steve', 'Jobs', '13195556789', 'steve@tekcafe.com', 20)
		
GO

/* Test for inactive employee */

print '' print '*** Insert In-active Employee test record'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [Commission], [Active])
	VALUES
		('Hacky', 'McHacker', '13195551234', 'hackme@tekcafe.com',  100, 0)
GO


print '' print '*** Creating Role Description Table that models the life cycle phase of the technology project.'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]				[nvarchar](50)			NOT NULL,
	[Description]			[nvarchar](200)			,
	
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** Inserting Web Employee Role into Records.'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
	    ('SellTek', 'Sell Technologies'),
		('CheckOut', 'Checks Out Technologies and coffee to Customers'),
		('Review', 'Checks In Project and Plans Them'),
		('HostSite', 'Prepares Project for Customers'),
		('Develops', 'Builds, Repairs and Maintains Projects'),
		('Developer', 'Manages Project Inventory'),
		('Admin', 'Manages Developer Assignment to Projects')
		
GO

print '' print '*** Creating EmployeeRole Table'
GO
CREATE TABLE [dbo].[EmployeeRole](
	[EmployeeID]			[int]					NOT NULL,
	[RoleID]				[nvarchar](50)			NOT NULL,	

	CONSTRAINT [pk_EmployeeID_RoleID] PRIMARY KEY([EmployeeID] ASC, [RoleID] ASC)
)
GO

print '' print '*** Inserting EmployeeRole records'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100000, 'SellTek'),
		(100000, 'CheckOut'),
		-- Basic Barista Service
		(100001, 'Reviews'),
		(100001, 'HostSite'),
        --  Technologist in Training
		(100002, 'Develops'),
		(100002, 'Developer'),
		(100002, 'Admin')
		-- The above Three are for Full Stack Developers who can also sell coffee
GO

print '' print '*** Adding EmployeeRole foreign keys'
GO
ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID])
	ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE CASCADE
GO






















































print '' print '*** Creating ProjectType Table for technology Products'
GO
CREATE TABLE [dbo].[ProjectType] 
(
	[ProjectTypeID]	[nvarchar](50)		NOT NULL,
	CONSTRAINT 	[pk_ProjectTypeID] PRIMARY KEY ([ProjectTypeID] ASC)
)
GO

print '' print '*** Inserting ProjectType into Records'
GO
INSERT INTO [dbo].[ProjectType]
	([ProjectTypeID])
VALUES
	('InternetCafe'),
	('WebSite'),
	('DataBase'),
	('FullStackApplication'),
	('HomeInterent'),
	('HardwareRepair'),
	('SoftwareDeveloper'),
	('Consultation'),
	('SHaaS')
GO

print '' print '*** Creating Customer Life Cycle Phase Table'
CREATE TABLE [dbo].[Phase]
(
	[PhaseID]	[nvarchar](50)		NOT NULL,
	CONSTRAINT 	[pk_PhaseID] PRIMARY KEY ([PhaseID] ASC)
)
GO

print '' print '*** Inserting Project Phases into Records'
GO
INSERT INTO [dbo].[Phase]
	([PhaseID])
VALUES
	('Ready to InternetCafe'),
	('Ready to Check Out'),
	('Ready for Review'),
	('Ready to Host Site'),
	('Development'),
	('New'),
	('Out of Inventory')
GO



print '' print '*** Creating the Client table and the password is newdev'
GO
CREATE TABLE [dbo].[Client](
	[ClientID]		    [int] IDENTITY(100000, 1) 	NOT NULL,
	[FirstName]			[nvarchar](50)				NOT NULL,
	[LastName]			[nvarchar](100)				NOT NULL,
	[PhoneNumber]		[nvarchar](11)				NOT NULL,
	[ClientEmail]		[nvarchar](255)				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL DEFAULT 
	'c3474dd290785b4948f65bae90c83f87',
	[Active]			[bit]						NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_ClientID] PRIMARY KEY([ClientID] ASC),
	CONSTRAINT [fk_ClientEmail] UNIQUE([ClientEmail] ASC)
)
GO

print '' print '*** Inserting Client information and Email into  Records'
GO
INSERT INTO [dbo].[Client]
		([FirstName], [LastName], [PhoneNumber], [ClientEmail])
	VALUES
	    ('Tom', 'Reddit', '13194200420', 'tom@gmail.com'),--100000
		('Jeff', 'Diggcom', '13198675309', 'jeff@hotmail.com'),--100001
		('Steve', 'Macbook', '13195556789', 'steve@yahoo.com'),--100002
		('Rob', 'Bobber', '13194200420', 'bob@gmail.com'),--100003
		('Shaun', 'Tron', '13198675309', 'shaun@hotmail.com'),--100004
		('Earl', 'Sidenbalm', '13195556789', 'earl@yahoo.com'),--100005
		('Muttly', 'McRuder', '13194200420', 'muttly@gmail.com'),--100006
		('Geoff', 'Whahahaah', '13198675309', 'geoff@hotmail.com'),--100007
		('Alex', 'Jimmy', '13193456789', 'alex@yahoo.com'),--100008
		('Jon', 'Murphy', '11005556789', 'jon@yahoo.com'),--100009
		('Bacon', 'Applecakes', '13195557789', 'bacon@yahoo.com')--100010
GO


print '' print '*** Creating the Product  (Hardware and Software and Coffee) table.'
GO
CREATE TABLE [dbo].[Product](
	[ProductID]			[int] IDENTITY(100000, 1) 	NOT NULL,
	[ProductType]		[nvarchar](50)				NOT NULL,
	[Description]	    [nvarchar](100)				NOT NULL,
	[Image]		   	    [nvarchar](50)				NOT NULL,
	
	
	CONSTRAINT [pk_ProductID] PRIMARY KEY([ProductID] ASC),
	
)
GO

print '' print '*** Inserting Products into  Product Records Table'
GO
INSERT INTO [dbo].[Product]
		([ProductType], [Description], [Image])
	VALUES
	    ('Software', 'Adobe Design Computer', 'imageOne.png'),
		('Software', 'Net Framework PC', 'imageTwo.png'),
		('Software', 'Java Design Computer', 'imageThree.png'),
	    ('Hardware', 'Wireless Mesh Network', 'imageFour.png'),
		('Hardware', 'Hardware Repair', 'imageFive.png '),
		('Hardware', 'Internet Radio', 'imageSix.png'),
		('Hardware', 'Internet Camera', 'imageSeven.png'),
		('Consultation', 'Planning Session', 'imageEight.png'),
		('Internet', 'Cloud Storage', 'imageNine.png'),
		('Light', 'Good Morning.', 'imageLightRoast.png'),
		('Medium', 'Have a Nice Dat.','imageMediumRoast.png'),
		('Dark', 'Now your awake!', 'imageDarkRoast.png'),
	    ('Esspresso', 'Fast as the Flash!!!', 'imageEsspressoRoast.png')
		
GO





print '' print '*** Creating Tek Technologies Project Table'
CREATE TABLE [dbo].[Project]
(
	[ProjectID]		[nvarchar](20)		NOT NULL,
	[Name]			[nvarchar](25)		NOT NULL,
	[Description]	[nvarchar](255)		NOT NULL,
	[PurchaseDate]	[date]				NOT NULL,
	[WorkStation]   [int]				NOT NULL,
	[ProjectTypeID]	[nvarchar](50)		NOT NULL,
	[PhaseID]		[nvarchar](50)		NOT NULL,
 	[ClientID]		[int]				NOT NULL,
	[Active]		[bit]				NOT NULL 	DEFAULT 1,
	CONSTRAINT 		[pk_ProjectID] 		PRIMARY KEY ([ProjectID] ASC),

	

    CONSTRAINT		[fk_ProjectTypeID]	FOREIGN KEY ([ProjectTypeID])
		REFERENCES [dbo].[ProjectType]([ProjectTypeID]),

	CONSTRAINT		[fk_PhaseID]		FOREIGN KEY ([PhaseID])
		REFERENCES [dbo].[Phase]([PhaseID]),

	CONSTRAINT		[fk_ClientID]		FOREIGN KEY ([ClientID])
		REFERENCES [dbo].[Client]([ClientID])
	
 )
 GO


print '' print '*** Inserting Project Records'
GO
INSERT INTO [dbo].[Project]
	([ProjectID], [Name], [Description], [PurchaseDate], [WorkStation],
		[ProjectTypeID], [PhaseID], [ClientID])
VALUES
	('PROJECT100001', 'Team 1', 'Needs a web site',               '06-15-2018', 3,               'WebSite',     'Ready to InternetCafe', 100000),
	('PROJECT100002', 'Team 2', 'Needs a Full Stack Application', '06-15-2018', 2,             'FullStackApplication', 'Ready to InternetCafe', 100001),
	('PROJECT100003', 'Team 3', 'Internet Cafe Medium Roast',     '06-15-2018', 1,                'InternetCafe', 'Ready to InternetCafe', 100002),
	('PROJECT100004', 'Team 4', 'Home Internet Installation.',    '06-15-2018', 4,                'HomeInterent', 'Ready to InternetCafe', 100003),
	('PROJECT100005', 'Team 5', 'HardWare Repair iphone Screen',    '06-16-2018', 5,                'HardwareRepair', 'Ready to InternetCafe', 100004),
	('PROJECT100006', 'Team 6', 'Consultation for Database Product',  '06-17-2018', 6,         'Consultation',       'Ready to InternetCafe', 100005),
	('PROJECT100007', 'Team 7', 'Needs an JasperSoft Database Dashboard',  '06-18-2018', 7,  'DataBase',       'Ready to InternetCafe', 100006),
	('PROJECT100008', 'Team 8', 'Using SaaS for Adobe products on Cafe PC.',  '06-19-2018', 8,  'SHaaS',      'Ready to InternetCafe', 100007),
	('PROJECT100009', 'Team 9', 'Internet Cafe Medium Roast',                '06-20-2018', 1,  'InternetCafe', 'Ready to InternetCafe', 100008),	
	('PROJECT100010', 'Team 10', 'Internet Cafe Medium Roast',                 '07-15-2018', 1,    'InternetCafe', 'Ready to InternetCafe', 100009),
	('PROJECT100011', 'Team 11', 'Java Project',                         '07-17-2018', 6,    'SoftwareDeveloper', 'Ready to InternetCafe', 100010)
GO















print '' print '*** Creating ProjectProduct Table'
GO
CREATE TABLE [dbo].[ProjectProduct](
	[ProjectID]			[nvarchar](20)		    NOT NULL,
	[ProductID]			[int]		        	NOT NULL,	

	CONSTRAINT [pk_ProjectID_ProductID] PRIMARY KEY([ProjectID] ASC, [ProductID] ASC)
)
GO

print '' print '*** Inserting Project Product Table records'
GO
INSERT INTO [dbo].[ProjectProduct]
		([ProjectID], [ProductID])
	VALUES
		('PROJECT100001', 100000),
		('PROJECT100002', 100001),
		('PROJECT100003', 100010),
		('PROJECT100004', 100005),
		('PROJECT100005', 100004),
		('PROJECT100006', 100007),
		('PROJECT100007', 100007),
		('PROJECT100008', 100000),
		('PROJECT100009', 100010),
		('PROJECT100010', 100010),
		('PROJECT100011', 100002)
		
GO

print '' print '*** Adding ProjectProduct foreign keys'
GO
ALTER TABLE [dbo].[ProjectProduct] WITH NOCHECK
	ADD CONSTRAINT [fk_ProductID] FOREIGN KEY([ProductID])
		REFERENCES [dbo].[Product]([ProductID])
	ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[ProjectProduct] WITH NOCHECK
	ADD CONSTRAINT [fk_ProjectID] FOREIGN KEY([ProjectID])
		REFERENCES [dbo].[Project]([ProjectID])
	ON UPDATE CASCADE
GO


























print '' print '*** Creating sp_update_newemployee_email'
GO
CREATE PROCEDURE [dbo].[sp_update_newemployee_email]
	(
		@EmployeeID		[int],
		@Email			[nvarchar](255),
		@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [dbo].[Employee]
			SET [Email] = @Email
			WHERE [EmployeeID] = @EmployeeID
			  AND [PasswordHash] = @PasswordHash
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_authorize_employee'
GO
CREATE PROCEDURE [dbo].[sp_authorize_employee]
	(
		@Email				[nvarchar](255),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([EmployeeID])
		FROM [dbo].[Employee]
		WHERE [Email] = @Email
		AND [PasswordHash] = @PasswordHash
		AND [Active] = 1
	END
GO

print '' print '*** Creating sp_get_employee_roles'
GO
CREATE PROCEDURE sp_get_employee_roles
	(
		@Email		[nvarchar](255)
	)
AS
	BEGIN
		SELECT [RoleID]
		FROM [EmployeeRole] INNER JOIN [Employee] 
			 ON [EmployeeRole].[EmployeeID] = [Employee].[EmployeeID]
		WHERE [Employee].[Email] = @Email
	END
GO

print '' print '*** Creating sp_update_newpassword_hash'
GO
CREATE PROCEDURE sp_update_newpassword_hash
	(
		@Email				[nvarchar](255),
		@OldPasswordHash	[nvarchar](100),
		@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [PasswordHash] = @NewPasswordHash
		WHERE [Email] = @Email
		  AND [PasswordHash] = @OldPasswordHash
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_get_employee_info_by_email'
GO
CREATE PROCEDURE sp_get_employee_info_by_email
	(
		@Email 			[nvarchar](255)
	)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName]
		FROM [Employee]
		WHERE [Email] = @Email
	END
GO


print '' print '*** Creating sp_get_tekcafeprojects_by_phase'
GO
CREATE PROCEDURE sp_get_tekcafeprojects_by_phase
	(
		@PhaseID		[nvarchar](50)
	)
AS
	BEGIN	
		SELECT 	[ProjectID], [Name], [Description], [PurchaseDate], [WorkStation],
		        [ProjectTypeID], [PhaseID], [ClientID],[Active]
		FROM	[dbo].[Project]
		WHERE	[PhaseID] = @PhaseID
		  AND	[Active] = 1
	END
GO

print '' print '*** Creating sp_get_tekcafeprojects_by_term_in_description'
GO
CREATE PROCEDURE sp_get_tekcafeprojects_by_term_in_description
	(
		@SearchTerm		[nvarchar](100)
	)
AS
	BEGIN	
		SELECT 	[ProjectID], [Name], [Description], [PurchaseDate], [WorkStation],
		[ProjectTypeID], [PhaseID], [ClientID],[Active]
		FROM	[dbo].[Project]
		WHERE	[Description] LIKE '%' + @SearchTerm + '%'
		  AND	[Active] = 1
	END
GO



print '' print '*** Creating sp_get_tekcafeproject_by_id'
GO
CREATE PROCEDURE sp_get_tekcafeproject_by_id
	(
		@ProjectID		[nvarchar](17)
	)
AS
	BEGIN	
        SELECT 	[ProjectID], [Name], [Description], [PurchaseDate], [WorkStation],
		[ProjectTypeID], [PhaseID], [ClientID],[Active]
		FROM	[dbo].[Project]
		WHERE	[ProjectID] = @ProjectID
	END
GO





print '' print '*** Creating sp_activate_tekcafeproject_by_id'
GO
CREATE PROCEDURE sp_activate_tekcafeproject_by_id
	(
		@ProjectID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Project]
		SET 	[Active] = 1
		WHERE	[ProjectID] = @ProjectID
		  
		RETURN @@ROWCOUNT		
	END
GO



print '' print '*** Creating sp_insert_tekcafeproject'
GO
CREATE PROCEDURE sp_insert_tekcafeproject
	(	       
		@ProjectID		    [nvarchar](20),
		@Name				[nvarchar](25),
		@Description		[nvarchar](100),
		@PurchaseDate		[date],
		@WorkStation			[int],
		@ProjectTypeID		[nvarchar](50),
		@PhaseID			[nvarchar](50),
		@ClientID			[nvarchar](50)

	)
AS
	BEGIN
		INSERT INTO [dbo].[Project]
			([ProjectID], [Name], [Description], [PurchaseDate], [WorkStation],
		[ProjectTypeID], [PhaseID], [ClientID])
		VALUES
			(@ProjectID, @Name, @Description, @PurchaseDate, @WorkStation,
			 @ProjectTypeID, @PhaseID, @ClientID)
			
		RETURN @@ROWCOUNT
	END		
GO





print '' print '*** Creating sp_get_all_projectsphaseid'
GO
CREATE PROCEDURE sp_get_all_projectsphaseid
AS
	BEGIN
		SELECT 		[PhaseID]
		FROM		[Phase]
		ORDER BY 	[PhaseID]
	END
GO

print '' print '*** Creating sp_get_all_projecttypeid'
GO
CREATE PROCEDURE sp_get_all_projecttypeid
AS
	BEGIN
		SELECT 		[ProjectTypeID]
		FROM		[ProjectType]
		ORDER BY 	[ProjectTypeID]
	END
GO








print '' print '*** Creating sp_update_project_by_id'
GO
CREATE PROCEDURE sp_update_project_by_id
	(
		@ProjectID		    [nvarchar](20),
		@Name				[nvarchar](25),
		@Description		[nvarchar](100),
		@PurchaseDate		[date],
		@WorkStation		[int],
		@ProjectTypeID		[nvarchar](50),
		@PhaseID			[nvarchar](50),
		@ClientID			[nvarchar](50),
		@Active				[bit],
		
		@OldName			[nvarchar](25),
		@OldDescription		[nvarchar](100),
		@OldPurchaseDate	[date],
		@OldWorkStation		[int],
		@OldProjectTypeID   [nvarchar](50),
		@OldPhaseID		    [nvarchar](50),
		@OldClientID		[nvarchar](50),
		@OldActive			[bit]		
	)
AS
	BEGIN
		UPDATE	[Project]
		SET 	[Name] = @Name,
				[Description] = @Description,
				[PurchaseDate] = @PurchaseDate,
				[WorkStation] = @WorkStation,	
				[ProjectTypeID] = @ProjectTypeID,
				[PhaseID] = @PhaseID,
				[ClientID] = @ClientID,
				[Active] = @Active

		FROM	[dbo].[Project]
		WHERE	[ProjectID] = @ProjectID
		  AND	[Name] = @OldName
		  AND	[Description] = @OldDescription
		  AND	[PurchaseDate] = @OldPurchaseDate
		  AND	[WorkStation] = @OldWorkStation	
		  AND	[ProjectTypeID] = @OldProjectTypeID
		  AND	[PhaseID] = @OldPhaseID
		  AND	[ClientID] = @OldClientID
		  AND	[Active] = @OldActive
		  
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_tekcafeproject_by_id'
GO
CREATE PROCEDURE sp_deactivate_tekcafeproject_by_id
	(
		@ProjectID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Project]
		SET 	[Active] = 0
		WHERE	[ProjectID] = @ProjectID
		  
		RETURN @@ROWCOUNT		
	END
GO




print '' print '*** Creating sp_delete_tekcafeproject_by_id'
GO
CREATE PROCEDURE sp_delete_tekcafeproject_by_id
	(
		@ProjectID		[nvarchar](17)
	)
AS
	BEGIN
		DELETE 	
		FROM	[Project]
		WHERE	[ProjectID] = @ProjectID
		  AND	[Active] = 0
		  
		RETURN @@ROWCOUNT
	END
GO