USE [JamifyDB]
GO

/****** Object:  Table [dbo].[Post]    Script Date: 05.03.2023 20:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Post](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](25) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[MediaId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_MediaData] FOREIGN KEY([MediaId])
REFERENCES [dbo].[MediaData] ([Id])
GO

ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_MediaData]
GO

ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO

