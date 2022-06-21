USE [ECommerceBD]
GO

INSERT INTO [dbo].[TypeProducts]
           ([Title]
           ,[Url])
     VALUES
           (N'Нижнее белье'
           ,'nizhneye-bele')
GO

INSERT INTO [dbo].[TypeProducts]
           ([Title]
           ,[Url])
     VALUES
           (N'Колготы'
           ,'kolgoty')
GO

INSERT INTO [dbo].[TypeProducts]
           ([Title]
           ,[Url])
     VALUES
           (N'Носочки'
           ,'nosochki')
GO

INSERT INTO [dbo].[TypeProducts]
           ([Title]
           ,[Url])
     VALUES
           (N'Топы'
           ,'topy')
GO

INSERT INTO [dbo].[TypeProducts]
           ([Title]
           ,[Url])
     VALUES
           (N'Пижамы с шортами'
           ,'pizhami-s-shortami')
GO




INSERT INTO [dbo].[Categories]
           ([Title]
           ,[Url])
     VALUES
           (N'Комплекты'
           ,'komplekty')
GO

INSERT INTO [dbo].[Categories]
           ([Title]
           ,[Url])
     VALUES
           (N'Бюстгальтеры push-up'
           ,'byustgaltery-push-up')
GO

INSERT INTO [dbo].[Categories]
           ([Title]
           ,[Url])
     VALUES
           (N'Трусы'
           ,'trusy')
GO

INSERT INTO [dbo].[Colors]
           ([Title])
     VALUES
           ('-')
GO


INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('-')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('XS')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('S')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('M')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('L')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('XL')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('XXL')
GO

INSERT INTO [dbo].[Sizes]
           ([Title])
     VALUES
           ('3XL')
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'-'
           ,0)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Автономная Республика Крым'
           ,0)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Волынская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Днепропетровская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Донецкая'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Житомирская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Закарпатская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Запорожская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Ивано-Франковская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Киевская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Кировоградская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Луганская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Львовская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Николаевская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Одесская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Полтавская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Ровненская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Сумская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Тернопольская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Харьковская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Херсонская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Хмельницкая'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Черкасская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Черниговская'
           ,1)
GO

INSERT INTO [dbo].[AddressAreas]
           ([Description]
           ,[IsVisible])
     VALUES
           (N'Черновицкая'
           ,1)
GO


INSERT INTO [dbo].[Products]
           ([Url]
           ,[Title]
           ,[Description]
           ,[Status]
           ,[Discount]
           ,[FakePrice]
           ,[Price]
           ,[PathToTitlePhoto]
           ,[ProductRating]
           ,[TotalRaters]
           ,[CountRatings]
           ,[Sex]
           ,[TypeProductID])
     VALUES
           (N'unionbay-park'
           ,N'Unionbay Park'
           ,N'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta voluptatibus quos ea dolore rem, molestias laudantium et explicabo assumenda fugiat deserunt in, facilis laborum excepturi aliquid nobis ipsam deleniti aut? Aliquid sit hic id velit qui fuga nemo suscipit obcaecati. Officia nisi quaerat minus nulla saepe aperiam sint possimus magni veniam provident.'
           ,1
           ,30
           ,68
           ,47.6
           ,N'/photos/01.jpg'
           ,5
           ,5
           ,1
           ,2
           ,1)
GO

