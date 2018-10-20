USE [brach]
GO

/****** Object:  Table [dbo].[Pessoas]    Script Date: 20/10/2018 00:40:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pessoas](
	[pessoaID] [int] IDENTITY(1,1) NOT NULL,
	[tipoPessoa] [int] NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[fantasia] [varchar](20) NULL,
	[nascimento] [date] NULL,
	[telefone] [varchar](20) NULL,
	[email] [varchar](100) NULL,
	[rua] [varchar](30) NULL,
	[numero] [varchar](15) NULL,
	[cidade] [varchar](30) NULL,
	[estado] [varchar](30) NULL,
	[documento] [int] NULL,
	[dtCadastro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[pessoaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pessoas]  WITH CHECK ADD  CONSTRAINT [FK_TIPOPessoas_TIPOS] FOREIGN KEY([tipoPessoa])
REFERENCES [dbo].[TiposGerais] ([tipoID])
GO

ALTER TABLE [dbo].[Pessoas] CHECK CONSTRAINT [FK_TIPOPessoas_TIPOS]
GO


