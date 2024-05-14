CREATE TABLE [dbo].[Movie] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Title]        VARCHAR (100) NOT NULL,
    [Description]  VARCHAR (500) NULL,
    [RealisatorId] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Movie_Real] FOREIGN KEY ([RealisatorId]) REFERENCES [dbo].[Person] ([Id])
);

