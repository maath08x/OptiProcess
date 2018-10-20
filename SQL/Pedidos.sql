USE [brach]
GO

/****** Object:  Table [dbo].[Pedidos]    Script Date: 20/10/2018 00:41:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pedidos](
	[pedidoID] [int] IDENTITY(1,1) NOT NULL,
	[pessoaID] [int] NOT NULL,
	[tipoPedido] [int] NOT NULL,
	[dtPedido] [datetime] NOT NULL,
	[dtPrevisao] [datetime] NULL,
	[finalizado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[pedidoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD FOREIGN KEY([pessoaID])
REFERENCES [dbo].[Pessoas] ([pessoaID])
GO

ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_TIPOPedido_TIPOS] FOREIGN KEY([tipoPedido])
REFERENCES [dbo].[TiposGerais] ([tipoID])
GO

ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_TIPOPedido_TIPOS]
GO


