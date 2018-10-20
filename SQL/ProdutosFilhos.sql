USE [brach]
GO

/****** Object:  Table [dbo].[ProdutosFilhos]    Script Date: 20/10/2018 00:40:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProdutosFilhos](
	[produtosFilhosID] [int] IDENTITY(1,1) NOT NULL,
	[produtoID] [int] NOT NULL,
	[filhoID] [int] NOT NULL,
	[quantidade] [int] NULL,
	[unidadeQuantidade] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[produtosFilhosID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProdutosFilhos]  WITH CHECK ADD FOREIGN KEY([filhoID])
REFERENCES [dbo].[Produtos] ([produtoID])
GO

ALTER TABLE [dbo].[ProdutosFilhos]  WITH CHECK ADD FOREIGN KEY([produtoID])
REFERENCES [dbo].[Produtos] ([produtoID])
GO


