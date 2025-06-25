use AssetManagementDB;

INSERT INTO Roles (RoleName) VALUES 
('Admin'),
('Manager'),
('Employee');

INSERT INTO Users (Username, PasswordHash, FullName, Gender, Email, PhoneNumber, Address, RoleId, CreatedAt, IsActive) VALUES 
('Santo', 'admin123', 'Santo T', 'Male', 'santo@admin.com', '9876543210', 'Chennai', 1, GETDATE(), 1),
('Viji', 'manager123', 'Viji M', 'Female', 'viji@manager.com', '9876543211', 'Coimbatore', 2, GETDATE(), 1),
('Shruthi', 'pass123', 'Shruthi C', 'Female', 'shruthi@emp.com', '9876543212', 'Madurai', 3, GETDATE(), 1),
('Nitish', 'pass456', 'Nitish S P', 'Male', 'nitish@emp.com', '9876543213', 'Trichy', 3, GETDATE(), 1),
('Roger', 'pass789', 'Roger A', 'Male', 'roger@emp.com', '9876543214', 'Salem', 3, GETDATE(), 0),
('Lokesh', 'manager456', 'Lokesh S', 'Male', 'lokesh@manager.com', '9574643237', 'Pondicherry', 2, GETDATE(), 0)


INSERT INTO Assets (AssetNo, AssetName, AssetModel, ManufacturingDate, ExpiryDate, AssetValue, Status) VALUES
('A001', 'Dell Laptop', 'Inspiron 15', '2022-01-01', '2025-01-01', 55000, 'Available'),
('A002', 'HP Laptop', 'Pavilion x360', '2021-06-01', '2024-06-01', 60000, 'Allocated'),
('A003', 'Office Chair', 'ErgoSeat', '2020-03-01', '2028-03-01', 5000, 'Available'),
('A004', 'Monitor', 'Samsung 24"', '2023-01-01', '2027-01-01', 12000, 'UnderService'),
('A005', 'Printer', 'HP LaserJet', '2020-10-01', '2026-10-01', 8000, 'Available');

INSERT INTO EmployeeAssets (UserId, AssetId, AssignedDate, ReturnDate, Status) VALUES
(3, 2, GETDATE(), NULL, 'Allocated'),
(4, 3, '2024-01-15', NULL, 'Allocated'),
(5, 4, '2024-02-20', NULL, 'InAudit'),
(3, 5, '2023-05-10', '2024-03-01', 'Returned'),
(4, 1, '2024-06-01', NULL, 'Allocated');

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
WHERE UserId = 5;
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