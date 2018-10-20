USE [brach]
GO

/****** Object:  Table [dbo].[Maquinarios]    Script Date: 20/10/2018 00:41:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Maquinarios](
	[maquinarioID] [int] IDENTITY(1,1) NOT NULL,
	[tipoMaquinario] [int] NULL,
	[statusMaquinario] [int] NULL,
	[dtOcupacao] [datetime] NULL,
	[dtDesocupacao] [datetime] NULL,
	[nome] [varchar](20) NOT NULL,
	[descricao] [nchar](35) NULL,
PRIMARY KEY CLUSTERED 
(
	[maquinarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Maquinarios]  WITH CHECK ADD FOREIGN KEY([tipoMaquinario])
REFERENCES [dbo].[TiposGerais] ([tipoID])
GO


