USE [brach]
GO

/****** Object:  Table [dbo].[OrdemProducao]    Script Date: 20/10/2018 00:41:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrdemProducao](
	[ordemProducaoID] [int] IDENTITY(1,1) NOT NULL,
	[produtoID] [int] NOT NULL,
	[quantidade] [int] NOT NULL,
	[maquinarioID] [int] NOT NULL,
	[pedidoID] [int] NOT NULL,
	[dtOrdemProd] [datetime] NOT NULL,
	[dtPrevisao] [datetime] NULL,
	[dtConclusao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ordemProducaoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrdemProducao]  WITH CHECK ADD FOREIGN KEY([maquinarioID])
REFERENCES [dbo].[Maquinarios] ([maquinarioID])
GO

ALTER TABLE [dbo].[OrdemProducao]  WITH CHECK ADD FOREIGN KEY([produtoID])
REFERENCES [dbo].[Produtos] ([produtoID])
GO

ALTER TABLE [dbo].[OrdemProducao]  WITH CHECK ADD  CONSTRAINT [PK__OrdemPro__EAFBFFC5FE7D8201] FOREIGN KEY([pedidoID])
REFERENCES [dbo].[Pedidos] ([pedidoID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrdemProducao] CHECK CONSTRAINT [PK__OrdemPro__EAFBFFC5FE7D8201]
GO


