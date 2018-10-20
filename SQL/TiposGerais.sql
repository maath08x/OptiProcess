USE [brach]
GO

/****** Object:  Table [dbo].[TiposGerais]    Script Date: 20/10/2018 00:39:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TiposGerais](
	[tipoID] [int] IDENTITY(1,1) NOT NULL,
	[telaID] [int] NULL,
	[nome] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tipoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TiposGerais]  WITH CHECK ADD FOREIGN KEY([telaID])
REFERENCES [dbo].[TiposGerais] ([tipoID])
GO


