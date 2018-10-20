USE [brach]
GO

/****** Object:  Table [dbo].[PedidosProdutos]    Script Date: 20/10/2018 00:41:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PedidosProdutos](
	[pedProdutosID] [int] IDENTITY(1,1) NOT NULL,
	[pedidoID] [int] NOT NULL,
	[produtoID] [int] NOT NULL,
	[qntPedido] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[pedProdutosID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PedidosProdutos]  WITH CHECK ADD FOREIGN KEY([pedidoID])
REFERENCES [dbo].[Pedidos] ([pedidoID])
GO

ALTER TABLE [dbo].[PedidosProdutos]  WITH CHECK ADD FOREIGN KEY([produtoID])
REFERENCES [dbo].[Produtos] ([produtoID])
GO


