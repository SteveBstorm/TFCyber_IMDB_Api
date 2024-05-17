CREATE TABLE [dbo].[Person] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Lastname]   VARCHAR (50)  NOT NULL,
    [Firstname]  VARCHAR (50)  NOT NULL,
    [PictureURL] VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

