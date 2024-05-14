CREATE TABLE [dbo].[Movie_Person] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [PersonId] INT           NOT NULL,
    [MovieId]  INT           NOT NULL,
    [Role]     VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Movie] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

