use AssetManagementDB;

INSERT INTO Roles (RoleName) VALUES 
('Admin'),
('Manager'),
('Employee');

INSERT INTO Users (Username, PasswordHash, FullName, Gender, Email, PhoneNumber, Address, RoleId) VALUES 
('Santo', 'admin123', 'Santo T', 'Male', 'santo@admin.com', '9876543210', 'Chennai', 1),
('Viji', 'manager123', 'Viji M', 'Female', 'viji@manager.com', '9876543211', 'Coimbatore', 2),
('Shruthi', 'manager456', 'Shruthi C', 'Female', 'shruthi@manager.com', '9876543212', 'Madurai', 2),
('Nitish', 'pass123', 'Nitish S P', 'Male', 'nitish@emp.com', '9876543213', 'Trichy', 3),
('Roger', 'pass789', 'Roger A', 'Male', 'roger@emp.com', '9876543214', 'Salem', 3),
('Lokesh', 'manager456', 'Lokesh S', 'Male', 'lokesh@manager.com', '9574643237', 'Pondicherry', 2)

INSERT INTO Assets (AssetName,Status, Quantity) VALUES 
('Dell Latitude 5420','Available', 3),
('Samsung 24-inch Monitor', 'Available', 4),
('Logitech MX Master 3',  'Available', 2),
('Mechanical Keyboard',  'Available', 1), 
('HP LaserJet 1020', 'Out of Stock', 0);


INSERT INTO EmployeeAssets (UserId, AssetId, AssignedDate, ReturnDate, Status) VALUES
(2, 1, GETDATE(), NULL, 'Allocated')

INSERT INTO ServiceRequests (AssetId, UserId, RequestDate, IssueType, Description, Status, ResolvedDate) VALUES
(2, 3, GETDATE(), 'Repair', 'Battery issue', 'Pending', NULL),
(3, 4, GETDATE(), 'Repair', 'Loose screws', 'InProgress', NULL),
(4, 5, '2024-01-20', 'Malfunction', 'No display', 'Completed', '2024-01-25'),
(5, 3, '2024-03-10', 'Repair', 'Printing faded', 'Rejected', '2024-03-12'),
(1, 4, GETDATE(), 'Malfunction', 'Slow performance', 'Pending', NULL);

INSERT INTO AuditRequests (AssetId, UserId, RequestDate, Status, Comments, VerifiedDate) VALUES
(2, 3, GETDATE(), 'Pending', 'Awaiting check', NULL),
(3, 4, '2024-01-10', 'Verified', 'Good condition', '2024-01-12'),
(4, 5, '2024-02-15', 'Rejected', 'Display broken', '2024-02-16'),
(1, 4, GETDATE(), 'Pending', '', NULL),
(5, 3, '2024-03-01', 'Verified', 'Looks fine', '2024-03-02');


SELECT * FROM Roles;
SELECT * FROM Users;
SELECT * FROM Assets;
SELECT * FROM AssetRequests;
SELECT * FROM EmployeeAssets;
SELECT * FROM ServiceRequests;
SELECT * FROM AuditRequests;

-- Clear Users table and reset ID
DELETE FROM Users;
DBCC CHECKIDENT ('Users', RESEED, 0);

-- Clear Roles table and reset ID
DELETE FROM Roles;
DBCC CHECKIDENT ('Roles', RESEED, 0);

-- Clear Assets table and reset ID
DELETE FROM Assets;
DBCC CHECKIDENT ('Assets', RESEED, 0);

-- Clear EmployeeAssets table and reset ID
DELETE FROM EmployeeAssets;
DBCC CHECKIDENT ('EmployeeAssets', RESEED, 0);

-- Clear AssetRequests table and reset ID
DELETE FROM AssetRequests;
DBCC CHECKIDENT ('AssetRequests', RESEED, 0);

-- Clear AuditRequests table and reset ID
DELETE FROM AuditRequests;
DBCC CHECKIDENT ('AuditRequests', RESEED, 0);

-- Clear ServiceRequests table and reset ID
DELETE FROM ServiceRequests;
DBCC CHECKIDENT ('ServiceRequests', RESEED, 0);


ALTER TABLE Users
DROP COLUMN CreatedAt;

ALTER TABLE Users
DROP COLUMN IsActive;

ALTER TABLE Assets
DROP COLUMN AssetNo,
             AssetModel,
             ManufacturingDate,
             ExpiryDate,
             AssetValue; 
UPDATE Users
SET RoleId = 2
WHERE UserId = 3;
UPDATE Users
SET PasswordHash = 'manager789'
WHERE UserId = 6;
UPDATE Users
SET PasswordHash = 'manager789'
WHERE UserId = 5;
UPDATE Users
SET Email = 'shruthi@manager.com'
WHERE UserId = 5;

ALTER TABLE EmployeeAssets
ALTER COLUMN AssignedDate DATE;

ALTER TABLE EmployeeAssets
ALTER COLUMN ReturnDate DATE;

ALTER TABLE ServiceRequests
ALTER COLUMN RequestDate DATE;

ALTER TABLE ServiceRequests
ALTER COLUMN ResolvedDate DATE;

ALTER TABLE AuditRequests
ALTER COLUMN RequestDate DATE;

ALTER TABLE AuditRequests
ALTER COLUMN VerifiedDate DATE;

UPDATE Assets
SET Quantity = 5
WHERE AssetId = 10;