USE [brach]
GO

/****** Object:  Table [dbo].[Logins]    Script Date: 20/10/2018 00:42:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Logins](
	[loginID] [int] IDENTITY(1,1) NOT NULL,
	[pessoaID] [int] NOT NULL,
	[login] [varchar](20) NOT NULL,
	[senha] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[loginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Logins]  WITH CHECK ADD FOREIGN KEY([pessoaID])
REFERENCES [dbo].[Pessoas] ([pessoaID])
GO


