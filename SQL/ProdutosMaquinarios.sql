USE [brach]
GO

/****** Object:  Table [dbo].[ProdutosMaquinarios]    Script Date: 20/10/2018 00:39:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProdutosMaquinarios](
	[produtosMaquinariosID] [int] IDENTITY(1,1) NOT NULL,
	[produtoID] [int] NOT NULL,
	[tipoMaquinario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[produtosMaquinariosID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProdutosMaquinarios]  WITH CHECK ADD FOREIGN KEY([produtoID])
REFERENCES [dbo].[Produtos] ([produtoID])
GO

ALTER TABLE [dbo].[ProdutosMaquinarios]  WITH CHECK ADD FOREIGN KEY([tipoMaquinario])
REFERENCES [dbo].[TiposGerais] ([tipoID])
GO


