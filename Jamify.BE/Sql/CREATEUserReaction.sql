USE [JamifyDB]
GO

/****** Object:  Table [dbo].[UserReaction]    Script Date: 28.02.2023 20:01:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserReaction](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[Reaction] [int] NOT NULL,
 CONSTRAINT [PK_Reaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserReaction] ADD  CONSTRAINT [DF_Reaction_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[UserReaction]  WITH CHECK ADD  CONSTRAINT [FK_UserReaction_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([Id])
GO

ALTER TABLE [dbo].[UserReaction] CHECK CONSTRAINT [FK_UserReaction_Post]
GO

ALTER TABLE [dbo].[UserReaction]  WITH CHECK ADD  CONSTRAINT [FK_UserReaction_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserReaction] CHECK CONSTRAINT [FK_UserReaction_User]
GO

