USE [JamifyDB]
GO

/****** Object:  Table [dbo].[HashtagMapping]    Script Date: 28.02.2023 20:00:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HashtagMapping](
	[Id] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[HashtagId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_HashtagMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HashtagMapping] ADD  CONSTRAINT [DF_HashtagMapping_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[HashtagMapping]  WITH CHECK ADD  CONSTRAINT [FK_HashtagMapping_Hashtag] FOREIGN KEY([HashtagId])
REFERENCES [dbo].[Hashtag] ([Id])
GO

ALTER TABLE [dbo].[HashtagMapping] CHECK CONSTRAINT [FK_HashtagMapping_Hashtag]
GO

ALTER TABLE [dbo].[HashtagMapping]  WITH CHECK ADD  CONSTRAINT [FK_HashtagMapping_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([Id])
GO

ALTER TABLE [dbo].[HashtagMapping] CHECK CONSTRAINT [FK_HashtagMapping_Post]
GO

