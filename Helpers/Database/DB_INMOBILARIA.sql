DROP DATABASE IF EXISTS DB_INMOBILIARIA
GO
CREATE DATABASE DB_INMOBILIARIA
GO
USE [DB_INMOBILIARIA]
GO
/****** Object:  Table [dbo].[DEPARTAMENTO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEPARTAMENTO](
	[id_departamento] [int] IDENTITY(1,1) NOT NULL,
	[piso] [numeric](10, 0) NULL,
	[numero] [numeric](10, 0) NULL,
	[metros_cuadrado] [numeric](10, 0) NULL,
	[dormitorio] [numeric](2, 0) NULL,
	[banio] [numeric](2, 0) NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_torre] [int] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_DEPARTAMENTO] PRIMARY KEY CLUSTERED 
(
	[id_departamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DEPARTAMENTO_FILE]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEPARTAMENTO_FILE](
	[id_departamento_file] [int] IDENTITY(1,1) NOT NULL,
	[url_imagen] [varchar](300) NULL,
	[fecha_creacion] [datetime] NULL,
	[id_departamento] [int] NULL,
 CONSTRAINT [PK_DEPARTAMENTO_FILE] PRIMARY KEY CLUSTERED 
(
	[id_departamento_file] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ESTADO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADO](
	[id_estado] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NULL,
	[unidad] [varchar](30) NULL,
 CONSTRAINT [PK_ESTADO] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INCIDENTE]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INCIDENTE](
	[id_incidente] [int] IDENTITY(1,1) NOT NULL,
	[fecha_incidente] [datetime] NULL,
	[descripcion] [varchar](200) NULL,
	[nombre_reportado] [varchar](100) NULL,
	[tipo_documento] [varchar](1) NULL,
	[nro_documento] [varchar](20) NULL,
	[fecha_registro] [datetime] NULL,
	[id_departamento] [int] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_INCIDENTE] PRIMARY KEY CLUSTERED 
(
	[id_incidente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INCIDENTE_FILE]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INCIDENTE_FILE](
	[id_incidente_file] [int] IDENTITY(1,1) NOT NULL,
	[fecha_registro] [datetime] NULL,
	[url_imagen] [varchar](300) NULL,
	[id_incidente] [int] NULL,
 CONSTRAINT [PK_INCIDENTE_FILE] PRIMARY KEY CLUSTERED 
(
	[id_incidente_file] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INCIDENTE_HISTORIAL]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INCIDENTE_HISTORIAL](
	[id_incidente_historial] [int] IDENTITY(1,1) NOT NULL,
	[acciones] [varchar](300) NULL,
	[fecha_historial] [date] NULL,
	[id_incidente] [int] NULL,
 CONSTRAINT [PK_INCIDENTE_HISTORIAL] PRIMARY KEY CLUSTERED 
(
	[id_incidente_historial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERFIL]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERFIL](
	[id_perfil] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NULL,
	[primer_apellido] [varchar](30) NULL,
	[segundo_apellido] [varchar](30) NULL,
	[fecha_nacimiento] [datetime] NULL,
	[tipo_documento] [varchar](1) NULL,
	[nro_documento] [varchar](20) NULL,
	[genero] [varchar](1) NULL,
	[nacionalidad] [varchar](1) NULL,
	[direccion] [varchar](200) NULL,
 CONSTRAINT [PK_PERFIL] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROPIETARIO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROPIETARIO](
	[id_propietario] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NULL,
	[primer_apellido] [varchar](30) NULL,
	[segundo_apellido] [varchar](30) NULL,
	[tipo_documento] [varchar](1) NULL,
	[nro_documento] [varchar](20) NULL,
	[nacionalidad] [varchar](1) NULL,
	[fecha_registro] [datetime] NULL,
	[estado] [bit] NULL,
	[id_departamento] [int] NULL,
	[id_tipo] [int] NULL,
 CONSTRAINT [PK_PROPIETARIO] PRIMARY KEY CLUSTERED 
(
	[id_propietario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RECIBO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RECIBO](
	[id_recibo] [int] IDENTITY(1,1) NOT NULL,
	[id_servicio] [int] NULL,
	[monto] [decimal](10, 2) NULL,
	[estado] [bit] NULL,
	[fecha_pago] [datetime] NULL,
	[fecha_vencimiento] [datetime] NULL,
	[fecha_registro] [datetime] NULL,
 CONSTRAINT [PK_RECIBO] PRIMARY KEY CLUSTERED 
(
	[id_recibo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROL]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROL](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_ROL] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SECTOR]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SECTOR](
	[id_sector] [int] IDENTITY(1,1) NOT NULL,
	[nombre_sector] [varchar](50) NULL,
	[fecha_creacion] [datetime] NULL,
	[id_sucursal] [int] NULL,
 CONSTRAINT [PK_SECTOR] PRIMARY KEY CLUSTERED 
(
	[id_sector] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SERVICIO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICIO](
	[id_servicio] [int] IDENTITY(1,1) NOT NULL,
	[id_tipo] [int] NULL,
	[id_departamento] [int] NULL,
	[nombre] [varchar](50) NULL,
	[fecha_registro] [datetime] NULL,
 CONSTRAINT [PK_SERVICIO] PRIMARY KEY CLUSTERED 
(
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SUCURSAL]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SUCURSAL](
	[id_sucursal] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](100) NULL,
	[fecha_creacion] [datetime] NULL,
 CONSTRAINT [PK_SUCURSAL] PRIMARY KEY CLUSTERED 
(
	[id_sucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPO](
	[id_tipo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[unidad] [varchar](20) NULL,
 CONSTRAINT [PK_TIPO] PRIMARY KEY CLUSTERED 
(
	[id_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TORRE]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TORRE](
	[id_torre] [int] IDENTITY(1,1) NOT NULL,
	[numero] [decimal](10, 0) NULL,
	[fecha_creacion] [datetime] NULL,
	[id_sector] [int] NULL,
 CONSTRAINT [PK_TORRE] PRIMARY KEY CLUSTERED 
(
	[id_torre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](30) NULL,
	[clave] [varchar](50) NULL,
	[fecha_registro] [datetime] NULL,
	[id_rol] [int] NULL,
	[id_perfil] [int] NULL,
	[id_estado] [int] NULL,
 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VISITA_REGISTRO]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VISITA_REGISTRO](
	[id_visita_registro] [int] IDENTITY(1,1) NOT NULL,
	[fecha_ingreso] [datetime] NULL,
	[fecha_salida] [datetime] NULL,
	[id_departamento] [int] NULL,
	[id_visitante] [int] NULL,
 CONSTRAINT [PK_VISITA_REGISTRO] PRIMARY KEY CLUSTERED 
(
	[id_visita_registro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VISITANTE]    Script Date: 19/4/2022 18:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VISITANTE](
	[id_visitante] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NULL,
	[apellidos] [varchar](50) NULL,
	[tipo_documento] [varchar](1) NULL,
	[nro_documento] [varchar](20) NULL,
	[genero] [varchar](1) NULL,
	[fecha_creacion] [datetime] NULL,
 CONSTRAINT [PK_VISITANTE] PRIMARY KEY CLUSTERED 
(
	[id_visitante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DEPARTAMENTO] ADD  CONSTRAINT [DF_DEPARTAMENTO_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[DEPARTAMENTO_FILE] ADD  CONSTRAINT [DF_DEPARTAMENTO_FILE_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[INCIDENTE] ADD  CONSTRAINT [DF_INCIDENTE_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[INCIDENTE_FILE] ADD  CONSTRAINT [DF_INCIDENTE_FILE_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[PROPIETARIO] ADD  CONSTRAINT [DF_PROPIETARIO_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[RECIBO] ADD  CONSTRAINT [DF_RECIBO_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[SECTOR] ADD  CONSTRAINT [DF_SECTOR_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[SERVICIO] ADD  CONSTRAINT [DF_SERVICIO_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[SUCURSAL] ADD  CONSTRAINT [DF_SUCURSAL_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[TORRE] ADD  CONSTRAINT [DF_TORRE_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[USUARIO] ADD  CONSTRAINT [DF_USUARIO_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[VISITA_REGISTRO] ADD  CONSTRAINT [DF_VISITA_REGISTRO_fecha_ingreso]  DEFAULT (getdate()) FOR [fecha_ingreso]
GO
ALTER TABLE [dbo].[VISITANTE] ADD  CONSTRAINT [DF_VISITANTE_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[DEPARTAMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTAMENTO_TORRE] FOREIGN KEY([id_torre])
REFERENCES [dbo].[TORRE] ([id_torre])
GO
ALTER TABLE [dbo].[DEPARTAMENTO] CHECK CONSTRAINT [FK_DEPARTAMENTO_TORRE]
GO
ALTER TABLE [dbo].[DEPARTAMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTAMENTO_USUARIO] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[USUARIO] ([id_usuario])
GO
ALTER TABLE [dbo].[DEPARTAMENTO] CHECK CONSTRAINT [FK_DEPARTAMENTO_USUARIO]
GO
ALTER TABLE [dbo].[DEPARTAMENTO_FILE]  WITH CHECK ADD  CONSTRAINT [FK_FILE_DEPARTAMENTO] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[DEPARTAMENTO] ([id_departamento])
GO
ALTER TABLE [dbo].[DEPARTAMENTO_FILE] CHECK CONSTRAINT [FK_FILE_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[INCIDENTE]  WITH CHECK ADD  CONSTRAINT [FK_INCIDENTE_DEPARTAMENTO] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[DEPARTAMENTO] ([id_departamento])
GO
ALTER TABLE [dbo].[INCIDENTE] CHECK CONSTRAINT [FK_INCIDENTE_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[INCIDENTE]  WITH CHECK ADD  CONSTRAINT [FK_INCIDENTE_USUARIO] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[USUARIO] ([id_usuario])
GO
ALTER TABLE [dbo].[INCIDENTE] CHECK CONSTRAINT [FK_INCIDENTE_USUARIO]
GO
ALTER TABLE [dbo].[INCIDENTE_FILE]  WITH CHECK ADD  CONSTRAINT [FK_INCIDENTE_FILE_INCIDENTE] FOREIGN KEY([id_incidente])
REFERENCES [dbo].[INCIDENTE] ([id_incidente])
GO
ALTER TABLE [dbo].[INCIDENTE_FILE] CHECK CONSTRAINT [FK_INCIDENTE_FILE_INCIDENTE]
GO
ALTER TABLE [dbo].[INCIDENTE_HISTORIAL]  WITH CHECK ADD  CONSTRAINT [FK_INCIDENTE_HISTORIAL_INCIDENTE] FOREIGN KEY([id_incidente])
REFERENCES [dbo].[INCIDENTE] ([id_incidente])
GO
ALTER TABLE [dbo].[INCIDENTE_HISTORIAL] CHECK CONSTRAINT [FK_INCIDENTE_HISTORIAL_INCIDENTE]
GO
ALTER TABLE [dbo].[PROPIETARIO]  WITH CHECK ADD  CONSTRAINT [FK_PROPIETARIO_DEPARTAMENTO] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[DEPARTAMENTO] ([id_departamento])
GO
ALTER TABLE [dbo].[PROPIETARIO] CHECK CONSTRAINT [FK_PROPIETARIO_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[PROPIETARIO]  WITH CHECK ADD  CONSTRAINT [FK_PROPIETARIO_TIPO] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[TIPO] ([id_tipo])
GO
ALTER TABLE [dbo].[PROPIETARIO] CHECK CONSTRAINT [FK_PROPIETARIO_TIPO]
GO
ALTER TABLE [dbo].[RECIBO]  WITH CHECK ADD  CONSTRAINT [FK_RECIBO_SERVICIO] FOREIGN KEY([id_servicio])
REFERENCES [dbo].[SERVICIO] ([id_servicio])
GO
ALTER TABLE [dbo].[RECIBO] CHECK CONSTRAINT [FK_RECIBO_SERVICIO]
GO
ALTER TABLE [dbo].[SECTOR]  WITH CHECK ADD  CONSTRAINT [FK_SECTOR_SUCURSAL] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[SUCURSAL] ([id_sucursal])
GO
ALTER TABLE [dbo].[SECTOR] CHECK CONSTRAINT [FK_SECTOR_SUCURSAL]
GO
ALTER TABLE [dbo].[SERVICIO]  WITH CHECK ADD  CONSTRAINT [FK_SERVICIO_DEPARTAMENTO] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[DEPARTAMENTO] ([id_departamento])
GO
ALTER TABLE [dbo].[SERVICIO] CHECK CONSTRAINT [FK_SERVICIO_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[SERVICIO]  WITH CHECK ADD  CONSTRAINT [FK_SERVICIO_TIPO] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[TIPO] ([id_tipo])
GO
ALTER TABLE [dbo].[SERVICIO] CHECK CONSTRAINT [FK_SERVICIO_TIPO]
GO
ALTER TABLE [dbo].[TORRE]  WITH CHECK ADD  CONSTRAINT [FK_TORRE_SECTOR] FOREIGN KEY([id_sector])
REFERENCES [dbo].[SECTOR] ([id_sector])
GO
ALTER TABLE [dbo].[TORRE] CHECK CONSTRAINT [FK_TORRE_SECTOR]
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_ESTADO] FOREIGN KEY([id_estado])
REFERENCES [dbo].[ESTADO] ([id_estado])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_ESTADO]
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_PERFIL] FOREIGN KEY([id_perfil])
REFERENCES [dbo].[PERFIL] ([id_perfil])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_PERFIL]
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_ROL] FOREIGN KEY([id_rol])
REFERENCES [dbo].[ROL] ([id_rol])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_ROL]
GO
ALTER TABLE [dbo].[VISITA_REGISTRO]  WITH CHECK ADD  CONSTRAINT [FK_VISITA_REGISTRO_DEPARTAMENTO] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[DEPARTAMENTO] ([id_departamento])
GO
ALTER TABLE [dbo].[VISITA_REGISTRO] CHECK CONSTRAINT [FK_VISITA_REGISTRO_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[VISITA_REGISTRO]  WITH CHECK ADD  CONSTRAINT [FK_VISITA_REGISTRO_VISITANTE] FOREIGN KEY([id_visitante])
REFERENCES [dbo].[VISITANTE] ([id_visitante])
GO
ALTER TABLE [dbo].[VISITA_REGISTRO] CHECK CONSTRAINT [FK_VISITA_REGISTRO_VISITANTE]
GO





USE [DB_INMOBILIARIA]
GO
SET IDENTITY_INSERT [dbo].[ESTADO] ON 

INSERT [dbo].[ESTADO] ([id_estado], [nombre], [unidad]) VALUES (1, 'Activo', 'USUARIO')
INSERT [dbo].[ESTADO] ([id_estado], [nombre], [unidad]) VALUES (2, 'Bloqueado', 'USUARIO')
INSERT [dbo].[ESTADO] ([id_estado], [nombre], [unidad]) VALUES (3, 'Pendiente', 'USUARIO')
SET IDENTITY_INSERT [dbo].[ESTADO] OFF
GO
SET IDENTITY_INSERT [dbo].[ROL] ON 

INSERT [dbo].[ROL] ([id_rol], [nombre], [descripcion]) VALUES (1, 'Administrador', 'Acceso general del sistema')
INSERT [dbo].[ROL] ([id_rol], [nombre], [descripcion]) VALUES (2, 'Agente de sistema', 'Acceso general excepto seguridad')
INSERT [dbo].[ROL] ([id_rol], [nombre], [descripcion]) VALUES (3, 'Agente de visitas', 'Acceso a registro visitas entre otros')
INSERT [dbo].[ROL] ([id_rol], [nombre], [descripcion]) VALUES (4, 'Agente ingresante', 'Nuevo agente a evaluar')
SET IDENTITY_INSERT [dbo].[ROL] OFF
GO
SET IDENTITY_INSERT [dbo].[TIPO] ON 

INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (1, 'DUEÑO', 'PROPIETARIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (2, 'FAMILIAR', 'PROPIETARIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (3, 'MASCOTA', 'PROPIETARIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (4, 'DNI', 'DOCUMENTO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (5, 'CARNET EXTRANJERIA', 'DOCUMENTO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (6, 'PASAPORTE', 'DOCUMENTO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (7, 'ENEL', 'SERVICIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (8, 'SEDAPAL', 'SERVICIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (9, 'MOVISTAR', 'SERVICIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (10, 'CLARO', 'SERVICIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (11, 'DIRECTV', 'SERVICIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (12, 'CALIDA', 'SERVICIO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (13, 'MASCULINO', 'GENERO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (14, 'FEMENINO', 'GENERO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (15, 'SIN ESPECIFICAR', 'GENERO')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (16, 'PERUANO', 'NACIONALIDAD')
INSERT [dbo].[TIPO] ([id_tipo], [nombre], [unidad]) VALUES (17, 'EXTRANJERO', 'NACIONALIDAD')
SET IDENTITY_INSERT [dbo].[TIPO] OFF
GO
