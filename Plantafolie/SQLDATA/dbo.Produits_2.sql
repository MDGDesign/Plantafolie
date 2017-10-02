CREATE TABLE [dbo].[Produits] (
    [ProduitID]      INT             IDENTITY (1, 1) NOT NULL,
    [CategorieID]    INT             NULL,
    [DateDeCreation] DATETIME2 (7)   NULL,
    [Description]    NVARCHAR (MAX)  NULL,
    [Disponible]     BIT             NULL,
    [EtatID]         INT             NULL,
    [ImageName]      NVARCHAR (MAX)  NULL,
    [Nom]            NVARCHAR (MAX)  ORDERBY [Nom] NULL,
    [Poids]          DECIMAL (18, 2) NULL,
    [PrixDeVente]    DECIMAL (18, 2) NULL,
    [PrixDemande]    DECIMAL (18, 2) NOT NULL,
    [Quantite]       INT             NULL,
    CONSTRAINT [PK_Produits] PRIMARY KEY CLUSTERED ([ProduitID] ASC),
    CONSTRAINT [FK_Produits_Categories_CategorieID] FOREIGN KEY ([CategorieID]) REFERENCES [dbo].[Categories] ([CategorieID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Produits_Etats_EtatID] FOREIGN KEY ([EtatID]) REFERENCES [dbo].[Etats] ([EtatID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Produits_CategorieID]
    ON [dbo].[Produits]([CategorieID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Produits_EtatID]
    ON [dbo].[Produits]([EtatID] ASC);

