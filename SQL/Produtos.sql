USE [brach]
GO

/****** Object:  Table [dbo].[Produtos]    Script Date: 20/10/2018 00:40:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Produtos](
	[produtoID] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](20) NOT NULL,
	[descricao] [varchar](50) NULL,
	[qntEstoque] [int] NOT NULL,
	[leadTime] [int] NOT NULL,
	[unidadeMedida] [int] NOT NULL,
	[estoqueSeguranca] [int] NULL,
	[politicaLote] [int] NULL,
	[unidadePoliticaLote] [int] NULL,
	[qntEstoqueReservado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[produtoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Produtos] ADD  DEFAULT ((0)) FOR [qntEstoque]
GO


