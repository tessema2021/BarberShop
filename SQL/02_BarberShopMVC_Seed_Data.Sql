SET IDENTITY_INSERT [UserType] ON
INSERT INTO [UserType]
    ([Id], [Name])
VALUES
    (1, 'Admin'),
    (2, 'Author')
    
SET IDENTITY_INSERT [UserType] OFF


SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile]
    ([Id],[FirstName],[LastName], [DisplayName],[Email],[CreateDateTime], [UserTypeId], [FirebaseId])
VALUES
    (1, 'Tessema', 'Fuchere', 'Tessema Fuchere','tesse@gmail.com',SYSDATETIME(),1,'dTVg3HwYLRYZE3deSebNzsO4emM2'),
    (2, 'Gedion', 'Nina', 'Gedion Nina','gedi@gmail.com',SYSDATETIME(),2,'Nbu8STy65VcjMLjEWudpE2EEktj1'),
    (3, 'Babu', 'Miheret', 'Babu Miheret','babu@gmail.com',SYSDATETIME(),2,'Vp3LhsUW2lf7WXu6s5KnEdu0Lhr1');
SET IDENTITY_INSERT [UserProfile] OFF

SET IDENTITY_INSERT [Service] ON
INSERT INTO [Service]
    ([Id], [Name], [Cost])
VALUES
    (1, 'Hair Coloring', 15),
    (2, 'Mens and boys haircut', 24),
    (3, 'Bear Sculpting', 24)
SET IDENTITY_INSERT [Service] OFF


SET IDENTITY_INSERT [UserService] ON
INSERT INTO [UserService]
    ([Id], [UserProfileId], [ServiceId])
VALUES
    (1, 1,1),
    (2, 2,2),
    (3, 3, 3)
   
SET IDENTITY_INSERT [UserService] OFF


SET IDENTITY_INSERT [Customer] ON
INSERT INTO [Customer]
    ([Id], [FirstName], [Lastname], [PhoneNumber],[CreateDateTime],[Email],[Address],[UserProfileId])
VALUES
    (1, 'James','Bolt','123-345-5690', SYSDATETIME(),'james@gmail.com','301 main st,Nashville', 2),
    (2, 'Bekele','haile','456-335-8945', SYSDATETIME(),'bekele@gmail.com','234 madison st,Madison', 3)
SET IDENTITY_INSERT [Customer] OFF


SET IDENTITY_INSERT [CustomerService] ON
INSERT INTO [CustomerService]
    ([Id], [CustomerId], [ServiceId])
VALUES
    (1, 1,1),
    (2, 2,2)
   
SET IDENTITY_INSERT [CustomerService] OFF


SET IDENTITY_INSERT [Transaction] ON
INSERT INTO [Transaction]
    ([Id], [Comment], [CustomerId], [UserProfileId],[TransactionDate])
VALUES
    (1, 'It was great time ', 1, 2, SYSDATETIME()),
    (2, 'My five star customer', 2, 3, SYSDATETIME())
SET IDENTITY_INSERT [Transaction] OFF


SET IDENTITY_INSERT [TransactionService] ON
INSERT INTO [TransactionService]
    ([Id], [ServiceId], [TransactionId])
VALUES
    (1,  1, 1),
    (2,  2, 2)
SET IDENTITY_INSERT [TransactionService] OFF






