USE [JamifyDB]
GO

/****** Object:  Table [dbo].[Following]    Script Date: 28.02.2023 19:56:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Following](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[FollowerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Following] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Following] ADD  CONSTRAINT [DF_Following_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Following] CHECK CONSTRAINT [FK_Following_User]
GO

ALTER TABLE [dbo].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_User1] FOREIGN KEY([FollowerId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Following] CHECK CONSTRAINT [FK_Following_User1]
GO

