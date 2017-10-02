﻿SET IDENTITY_INSERT [dbo].[Produits] ON
INSERT INTO [dbo].[Produits] ([ProduitID], [CategorieID], [DateDeCreation], [Description], [Disponible], [EtatID], [ImageName], [Nom], [Poids], [PrixDeVente], [PrixDemande], [Quantite]) VALUES (1, 3, N'2017-09-28 00:00:00', N'Une plante basse robuste. Évité les courant d''air froid', 1, 1, NULL, N'Agalonema', CAST(3.00 AS Decimal(18, 2)), CAST(20.99 AS Decimal(18, 2)), CAST(25.99 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[Produits] ([ProduitID], [CategorieID], [DateDeCreation], [Description], [Disponible], [EtatID], [ImageName], [Nom], [Poids], [PrixDeVente], [PrixDemande], [Quantite]) VALUES (1002, 1, N'2017-09-05 00:00:00', N'Un arbre avec tronc tressé qui aime le soleil', 1, 2, N'Aglaonema Jewel of India 10.jpg', N'Ficus benjamina', CAST(10.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), 23)
INSERT INTO [dbo].[Produits] ([ProduitID], [CategorieID], [DateDeCreation], [Description], [Disponible], [EtatID], [ImageName], [Nom], [Poids], [PrixDeVente], [PrixDemande], [Quantite]) VALUES (1003, 1, N'2017-09-30 00:00:00', N'Arbuste à grande fleurs', 1, 1, NULL, N'Hibiscus chinensis', CAST(5.00 AS Decimal(18, 2)), CAST(23.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), 4)
INSERT INTO [dbo].[Produits] ([ProduitID], [CategorieID], [DateDeCreation], [Description], [Disponible], [EtatID], [ImageName], [Nom], [Poids], [PrixDeVente], [PrixDemande], [Quantite]) VALUES (1005, 2, N'2017-09-30 18:29:54', N'Belle plante haute d''un vert très foncé', 1, 1, N'Dracaena marginata 10 ML.jpg', N'Dracaena marginata', CAST(8.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), 5)
SET IDENTITY_INSERT [dbo].[Produits] OFF