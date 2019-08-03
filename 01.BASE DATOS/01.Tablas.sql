USE POSADMIN

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Paises')) --ISO 3166-1
BEGIN 
   
   CREATE TABLE POSAD_Paises (
      PAIS_Interno INT NOT NULL,
	   PAIS_Nombre VARCHAR(100) NOT NULL,
      PAIS_CodigoNumerico VARCHAR(5) NOT NULL,
      PAIS_CodigoAlfa2 CHAR(2) NOT NULL,
      PAIS_CodigoAlfa3 CHAR(3) NOT NULL,
      PAIS_Continente VARCHAR(50) NULL,
	   PAIS_Descripcion VARCHAR(250) NULL,
	   PAIS_Activo BIT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Paises 
      ADD CONSTRAINT PK_Paises PRIMARY KEY CLUSTERED (PAIS_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Ubigeos'))
BEGIN 
   
   CREATE TABLE POSAD_Ubigeos (
      UBIG_Interno INT NOT NULL,
      PAIS_Interno INT NOT NULL,
	   UBIG_Nombre VARCHAR(100) NOT NULL,
	   UBIG_Nomenclatura VARCHAR(25) NULL,
	   UBIG_Descripcion VARCHAR(250) NULL,
      UBIG_Codigo1 VARCHAR(50) NULL,
      UBIG_Codigo2 VARCHAR(50) NULL,
      UBIG_Codigo3 VARCHAR(50) NULL,
	   UBIG_Activo BIT NOT NULL,
	   UBIG_InternoPadre INT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Ubigeos 
      ADD CONSTRAINT PK_Ubigeos PRIMARY KEY CLUSTERED (UBIG_Interno)
   
   ALTER TABLE POSAD_Ubigeos
      ADD CONSTRAINT FK_Ubigeos_Ubigeos FOREIGN KEY (UBIG_InternoPadre)
      REFERENCES POSAD_Ubigeos (UBIG_Interno)

   ALTER TABLE POSAD_Ubigeos
      ADD CONSTRAINT FK_Ubigoes_Paises FOREIGN KEY (PAIS_Interno)
      REFERENCES POSAD_Paises (PAIS_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Empresas'))
BEGIN 
   
   CREATE TABLE POSAD_Empresas (
      EMPR_Interno BIGINT NOT NULL,
      PAIS_Interno INT NOT NULL,
      EMPR_Id VARCHAR(50) NOT NULL,
      EMPR_RazonSocial VARCHAR(250) NOT NULL,
      EMPR_Direccion VARCHAR(200) NOT NULL,
      EMPR_NombreComercial VARCHAR(250) NULL,
      UBIG_Interno INT NULL,
      EMPR_Correos VARCHAR(100) NULL,
      EMPR_Telefonos VARCHAR(100) NULL,
      EMPR_Web VARCHAR(100) NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   CREATE CLUSTERED INDEX IX_Empresas_Clustered
      ON POSAD_Empresas (PAIS_Interno,EMPR_Id)   

   ALTER TABLE POSAD_Empresas 
      ADD CONSTRAINT PK_Empresas PRIMARY KEY NONCLUSTERED (EMPR_Interno)
   
   ALTER TABLE POSAD_Empresas
      ADD CONSTRAINT FK_Empresas_Paises FOREIGN KEY (PAIS_Interno)
      REFERENCES POSAD_Paises (PAIS_Interno)

   ALTER TABLE POSAD_Empresas
      ADD CONSTRAINT FK_Empresas_Ubigeos FOREIGN KEY (UBIG_Interno)
      REFERENCES POSAD_Ubigeos (UBIG_Interno)

END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Sucursales'))
BEGIN 
   
   CREATE TABLE POSAD_Sucursales (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      SUCU_Nombre VARCHAR(250) NOT NULL,
      SUCU_Direccion VARCHAR(200) NOT NULL,
      SUCU_Correo VARCHAR(100) NULL,
      SUCU_Telefono VARCHAR(100) NULL,
      SUCU_Web VARCHAR(100) NULL,
      SUCU_Principal BIT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   CREATE CLUSTERED INDEX IX_Sucursales_Clustered
      ON POSAD_Sucursales (SUCU_Nombre)   

   ALTER TABLE POSAD_Sucursales 
      ADD CONSTRAINT PK_Sucursales PRIMARY KEY NONCLUSTERED (EMPR_Interno, SUCU_Interno)

   ALTER TABLE POSAD_Sucursales 
      ADD CONSTRAINT FK_Sucursales_Empresas FOREIGN KEY (EMPR_Interno)
      REFERENCES POSAD_Empresas (EMPR_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Tablas'))
BEGIN 
   
   CREATE TABLE POSAD_Tablas (
      EMPR_Interno BIGINT NOT NULL,
      TABL_Tabla CHAR(3) NOT NULL,
      TABL_Interno CHAR(4) NOT NULL, 
      TABL_Nombre VARCHAR(250) NOT NULL,
      TABL_Nomenclatura VARCHAR(25) NULL,
	   TABL_Descripcion VARCHAR(250) NULL,
      TABL_CodigoInternacional VARCHAR(50) NULL,
      TABL_Codigo1 VARCHAR(50) NULL,
      TABL_Codigo2 VARCHAR(50) NULL,
      TABL_Codigo3 VARCHAR(50) NULL,
      TABL_Numero1 INT NULL,
      TABL_Numero2 DECIMAL(20,2) NULL,
      TABL_Numero3 DECIMAL(20,8) NULL,
      TABL_Activo BIT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Tablas 
      ADD CONSTRAINT PK_Tablas PRIMARY KEY CLUSTERED (EMPR_Interno, TABL_Tabla, TABL_Interno)

   ALTER TABLE POSAD_Tablas 
      ADD CONSTRAINT FK_Tablas_Empresas FOREIGN KEY (EMPR_Interno)
      REFERENCES POSAD_Empresas (EMPR_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Parametros'))
BEGIN 
   
   CREATE TABLE POSAD_Parametros (
      EMPR_Interno BIGINT NOT NULL,
      PARA_Llave CHAR(5) NOT NULL,
      PARA_Valor VARCHAR(50) NOT NULL,
      PARA_Descripcion VARCHAR(250) NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Parametros 
      ADD CONSTRAINT PK_Parametros PRIMARY KEY CLUSTERED (EMPR_Interno, PARA_Llave)

   ALTER TABLE POSAD_Parametros 
      ADD CONSTRAINT FK_Parametros_Empresas FOREIGN KEY (EMPR_Interno)
      REFERENCES POSAD_Empresas (EMPR_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Entidades'))
BEGIN 
   
   CREATE TABLE POSAD_Entidades (
      EMPR_Interno BIGINT NOT NULL,
      ENTI_Interno BIGINT NOT NULL,
      TABL_TablaTDI CHAR(3) NOT NULL,
      TABL_InternoTDI CHAR(4) NOT NULL, 
      ENTI_Id VARCHAR(50) NOT NULL,
      ENTI_NombreCompleto VARCHAR(250) NOT NULL,
      ENTI_Direccion VARCHAR(250) NULL,
      ENTI_Sexo CHAR(1) NULL,
      PAIS_Interno INT NULL,
      UBIG_Interno INT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   CREATE CLUSTERED INDEX IX_Entidades_Clustered
      ON POSAD_Entidades (TABL_TablaTDI,TABL_InternoTDI,ENTI_Id) 

   ALTER TABLE POSAD_Entidades 
      ADD CONSTRAINT PK_Entidades PRIMARY KEY NONCLUSTERED (EMPR_Interno, ENTI_Interno)

   ALTER TABLE POSAD_Entidades 
      ADD CONSTRAINT FK_Entidades_Empresas FOREIGN KEY (EMPR_Interno)
      REFERENCES POSAD_Empresas (EMPR_Interno)

   ALTER TABLE POSAD_Entidades 
      ADD CONSTRAINT FK_Entidades_TablasTDI FOREIGN KEY (EMPR_Interno,TABL_TablaTDI, TABL_InternoTDI)
      REFERENCES POSAD_Tablas (EMPR_Interno,TABL_Tabla, TABL_Interno)

   ALTER TABLE POSAD_Entidades
      ADD CONSTRAINT FK_Entidades_Paises FOREIGN KEY (PAIS_Interno)
      REFERENCES POSAD_Paises (PAIS_Interno)

   ALTER TABLE POSAD_Entidades
      ADD CONSTRAINT FK_Entidades_Ubigeos FOREIGN KEY (UBIG_Interno)
      REFERENCES POSAD_Ubigeos (UBIG_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_FuncionesEntidades'))
BEGIN 
   
   CREATE TABLE POSAD_FuncionesEntidades (
      EMPR_Interno BIGINT NOT NULL,
      TABL_TablaTEN CHAR(3) NOT NULL,
      TABL_InternoTEN CHAR(4) NOT NULL, 
      ENTI_Interno BIGINT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_FuncionesEntidades 
      ADD CONSTRAINT PK_FuncionesEntidades PRIMARY KEY CLUSTERED (EMPR_Interno, TABL_TablaTEN, TABL_InternoTEN, ENTI_Interno)
   
   ALTER TABLE POSAD_FuncionesEntidades 
      ADD CONSTRAINT FK_FuncionesEntidades_TablasTEN FOREIGN KEY (EMPR_Interno,TABL_TablaTEN, TABL_InternoTEN)
      REFERENCES POSAD_Tablas (EMPR_Interno,TABL_Tabla, TABL_Interno)

   ALTER TABLE POSAD_FuncionesEntidades 
      ADD CONSTRAINT FK_FuncionesEntidades_Entidades FOREIGN KEY (EMPR_Interno, ENTI_Interno)
      REFERENCES POSAD_Entidades (EMPR_Interno, ENTI_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Habitaciones'))
BEGIN 
   
   CREATE TABLE POSAD_Habitaciones (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      HABI_Interno BIGINT NOT NULL,
      TABL_TablaPIS CHAR(3) NOT NULL,
      TABL_InternoPIS CHAR(4) NOT NULL,
      TABL_TablaTHA CHAR(3) NOT NULL,
      TABL_InternoTHA CHAR(4) NOT NULL,
      HABI_Numero CHAR(3) NOT NULL,
      HABI_Estado CHAR(1) NOT NULL,--Ocupado / Libre / Reservado
      HABI_Limpio BIT NOT NULL,
      HABI_Activo BIT NOT NULL,
      HABI_Descripcion VARCHAR(250) NULL,
      HABI_PrecioDia DECIMAL(20,8) NOT NULL,
      HABI_PrecioHora DECIMAL(20,8) NOT NULL,
      HABI_PrecioMinimo DECIMAL(20,8) NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Habitaciones 
      ADD CONSTRAINT PK_Habitaciones PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, HABI_Interno)

   ALTER TABLE POSAD_Habitaciones 
      ADD CONSTRAINT FK_Habitaciones_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)

   ALTER TABLE POSAD_Habitaciones 
      ADD CONSTRAINT FK_Habitaciones_TablasPIS FOREIGN KEY (EMPR_Interno,TABL_TablaPIS, TABL_InternoPIS)
      REFERENCES POSAD_Tablas (EMPR_Interno,TABL_Tabla, TABL_Interno)

   ALTER TABLE POSAD_Habitaciones 
      ADD CONSTRAINT FK_Habitaciones_TablasTHA FOREIGN KEY (EMPR_Interno,TABL_TablaTHA, TABL_InternoTHA)
      REFERENCES POSAD_Tablas (EMPR_Interno,TABL_Tabla, TABL_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Roles'))
BEGIN 
   
   CREATE TABLE POSAD_Roles (
      ROLE_Interno INT NOT NULL,
      ROLE_Nombre VARCHAR(25) NOT NULL,
      ROLE_Descripcion VARCHAR(250) NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Roles 
      ADD CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED (ROLE_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Usuarios'))
BEGIN 
   
   CREATE TABLE POSAD_Usuarios (
      USUA_Interno BIGINT NOT NULL,
      ROLE_Interno INT NOT NULL,
      USUA_NombreCompleto VARCHAR(250) NOT NULL,
      USUA_Usuario VARCHAR(25) NOT NULL,
      USUA_Contrasena VARCHAR(MAX) NOT NULL,
      USUA_Correo VARCHAR(150) NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Usuarios 
      ADD CONSTRAINT PK_Usuarios PRIMARY KEY CLUSTERED (USUA_Interno)

   ALTER TABLE POSAD_Usuarios 
      ADD CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (ROLE_Interno)
      REFERENCES POSAD_Roles (ROLE_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_EmpresasUsuarios'))
BEGIN 
   
   CREATE TABLE POSAD_EmpresasUsuarios (
      EMPR_Interno BIGINT NOT NULL,
      USUA_Interno BIGINT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_EmpresasUsuarios 
      ADD CONSTRAINT PK_EmpresasUsuarios PRIMARY KEY CLUSTERED (EMPR_Interno, USUA_Interno)
   
   ALTER TABLE POSAD_EmpresasUsuarios 
      ADD CONSTRAINT FK_EmpresasUsuarios_Empresas FOREIGN KEY (EMPR_Interno)
      REFERENCES POSAD_Empresas (EMPR_Interno)

   ALTER TABLE POSAD_EmpresasUsuarios 
      ADD CONSTRAINT FK_EmpresasUsuarios_Usuarios FOREIGN KEY (USUA_Interno)
      REFERENCES POSAD_Usuarios (USUA_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Opciones'))
BEGIN 
   
   CREATE TABLE POSAD_Opciones (
      OPCI_Interno INT NOT NULL,
	   OPCI_Nombre VARCHAR(100) NOT NULL,
	   OPCI_Nomenclatura VARCHAR(25) NULL,
	   OPCI_Descripcion VARCHAR(250) NULL,
	   OPCI_InternoPadre INT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Opciones 
      ADD CONSTRAINT PK_Opciones PRIMARY KEY CLUSTERED (OPCI_Interno)
   
   ALTER TABLE POSAD_Opciones
      ADD CONSTRAINT FK_Opciones_Opciones FOREIGN KEY (OPCI_InternoPadre)
      REFERENCES POSAD_Opciones (OPCI_Interno)

END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_RolesOpciones'))
BEGIN 
   
   CREATE TABLE POSAD_RolesOpciones (
      ROLE_Interno INT NOT NULL,
      OPCI_Interno INT NOT NULL,
	   ROPC_Activo BIT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_RolesOpciones
      ADD CONSTRAINT PK_RolesOpciones PRIMARY KEY CLUSTERED (ROLE_Interno,OPCI_Interno)
   
   ALTER TABLE POSAD_RolesOpciones
      ADD CONSTRAINT FK_RolesOpciones_Roles FOREIGN KEY (ROLE_Interno)
      REFERENCES POSAD_Roles (ROLE_Interno)

   ALTER TABLE POSAD_RolesOpciones
      ADD CONSTRAINT FK_RolesOpciones_Opciones FOREIGN KEY (OPCI_Interno)
      REFERENCES POSAD_Opciones (OPCI_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Procesos'))
BEGIN 
   
   CREATE TABLE POSAD_Procesos (
      PROC_Interno INT NOT NULL,
	   PROC_Llave VARCHAR(25) NOT NULL,
	   PROC_Descripcion VARCHAR(250) NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Procesos 
      ADD CONSTRAINT PK_Procesos PRIMARY KEY CLUSTERED (PROC_Interno)

END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_RolesProcesos'))
BEGIN 
   
   CREATE TABLE POSAD_RolesProcesos (
      ROLE_Interno INT NOT NULL,
      PROC_Interno INT NOT NULL,
	   ROPR_Activo BIT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_RolesProcesos
      ADD CONSTRAINT PK_RolesProcesos PRIMARY KEY CLUSTERED (ROLE_Interno,PROC_Interno)
   
   ALTER TABLE POSAD_RolesProcesos
      ADD CONSTRAINT FK_RolesProcesos_Roles FOREIGN KEY (ROLE_Interno)
      REFERENCES POSAD_Roles (ROLE_Interno)

   ALTER TABLE POSAD_RolesProcesos
      ADD CONSTRAINT FK_RolesProcesos_Opciones FOREIGN KEY (PROC_Interno)
      REFERENCES POSAD_Procesos (PROC_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Configuraciones'))
BEGIN 
   
   CREATE TABLE POSAD_Configuraciones (
      EMPR_Interno BIGINT NOT NULL,
      USUA_Interno BIGINT NOT NULL,
      CONF_Llave CHAR(5) NOT NULL,
      CONF_Valor VARCHAR(50) NOT NULL,
      CONF_Descripcion VARCHAR(250) NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Configuraciones 
      ADD CONSTRAINT PK_Configuraciones PRIMARY KEY CLUSTERED (EMPR_Interno, USUA_Interno, CONF_Llave)

   ALTER TABLE POSAD_Configuraciones 
      ADD CONSTRAINT FK_Configuraciones_Empresas FOREIGN KEY (EMPR_Interno)
      REFERENCES POSAD_Empresas (EMPR_Interno)

   ALTER TABLE POSAD_Configuraciones 
      ADD CONSTRAINT FK_Configuraciones_Usuarios FOREIGN KEY (USUA_Interno)
      REFERENCES POSAD_Usuarios (USUA_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Turnos'))
BEGIN 
   
   CREATE TABLE POSAD_Turnos (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      TURN_Interno BIGINT NOT NULL,
      TURN_Nominacion VARCHAR(50) NOT NULL,
      TURN_HoraInicio TIME NOT NULL,
      TURN_HoraFin TIME NOT NULL,
      TURN_Descripcion VARCHAR(250) NULL,
      TURN_Color VARCHAR(20) NOT NULL,
      TURN_Activo BIT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Turnos 
      ADD CONSTRAINT PK_Turnos PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, TURN_Interno)

   ALTER TABLE POSAD_Turnos 
      ADD CONSTRAINT FK_Turnos_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)

END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Reservaciones'))
BEGIN 

   CREATE TABLE POSAD_Reservaciones (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      RESE_Interno BIGINT NOT NULL,
      RESE_Estado CHAR(1) NOT NULL, -- A : Activo , X : Anulado, E: Entregado
      HABI_Interno BIGINT NOT NULL,
      RESE_FechaInicio DATE NOT NULL,
      RESE_FechaFin DATE NOT NULL,
      RESE_FechaHoraRegistro DATETIME NOT NULL,
      RESE_HuespedId VARCHAR(50) NOT NULL,
      RESE_HuespedNombreCompleto VARCHAR(250) NOT NULL,
      RESE_HuespedDireccion VARCHAR(250) NULL,
      RESE_Descripcion VARCHAR(250) NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Reservaciones 
      ADD CONSTRAINT PK_Reservaciones PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, RESE_Interno)

   ALTER TABLE POSAD_Reservaciones 
      ADD CONSTRAINT FK_Reservaciones_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)

   ALTER TABLE POSAD_Reservaciones 
      ADD CONSTRAINT FK_Reservaciones_Habitaciones FOREIGN KEY (EMPR_Interno, SUCU_Interno, HABI_Interno)
      REFERENCES POSAD_Habitaciones (EMPR_Interno, SUCU_Interno, HABI_Interno)
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Cierres'))
BEGIN 

   CREATE TABLE POSAD_Cierres (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      CIER_Interno BIGINT NOT NULL,
      CIER_FechaHoraRegistro DATETIME NOT NULL,
      CIER_MontoReal DECIMAL(20,8) NOT NULL,
      CIER_MontoExtra DECIMAL(20,8) NOT NULL,
      CIER_MontoDeuda DECIMAL(20,8) NOT NULL,
      CIER_MontoDemas DECIMAL(20,8) NOT NULL,
      USUA_Interno BIGINT NOT NULL,
      AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Cierres 
      ADD CONSTRAINT PK_Cierres PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, CIER_Interno)

   ALTER TABLE POSAD_Cierres 
      ADD CONSTRAINT FK_Cierres_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)

   ALTER TABLE POSAD_Cierres 
      ADD CONSTRAINT FK_Cierres_Usuarios FOREIGN KEY (USUA_Interno)
      REFERENCES POSAD_Usuarios (USUA_Interno)
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Retiros'))
BEGIN 

   CREATE TABLE POSAD_Retiros (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      RETI_Interno BIGINT NOT NULL,
      RETI_FechaHoraRegistro DATETIME NOT NULL,
      RETI_MontoEntregado DECIMAL(20,8) NOT NULL,
      RETI_MontoExtra DECIMAL(20,8) NOT NULL,
      USUA_Interno BIGINT NOT NULL,
      AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Retiros 
      ADD CONSTRAINT PK_Retiros PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, RETI_Interno)

   ALTER TABLE POSAD_Retiros 
      ADD CONSTRAINT FK_Retiros_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)

   ALTER TABLE POSAD_Retiros 
      ADD CONSTRAINT FK_Retiros_Usuarios FOREIGN KEY (USUA_Interno)
      REFERENCES POSAD_Usuarios (USUA_Interno)
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Registros'))
BEGIN 
   
   CREATE TABLE POSAD_Registros (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      REGI_Interno BIGINT NOT NULL,
      HABI_Interno BIGINT NOT NULL,
      REGI_Estado CHAR(1) NOT NULL, --A:Activo - X:Anulado - C:Cancelado(Pago)
      REGI_Tramos CHAR(1) NOT NULL, --Dias o Horas
      REGI_Cantidad INT NOT NULL,
      REGI_FechaHoraEntrada DATETIME NOT NULL,
      REGI_FechaHoraSalida DATETIME NOT NULL,
      REGI_FechaHoraSalidaReal DATETIME NULL,
      TABL_TablaTDI CHAR(3) NOT NULL,
      TABL_InternoTDI CHAR(4) NOT NULL, 
      ENTI_Interno BIGINT NOT NULL,
      REGI_HuespedId VARCHAR(50) NOT NULL,
      REGI_HuespedNombreCompleto VARCHAR(250) NOT NULL,
      REGI_HuespedDireccion VARCHAR(250) NULL,
      REGI_PrecioSugerido DECIMAL(20,8) NOT NULL,
      REGI_PrecioCobrado DECIMAL(20,8) NOT NULL,
      REGI_MotivoAnulacion VARCHAR(250) NULL,
      REGI_FechaHoraAnulacion DATETIME NULL,
      TURN_Interno BIGINT NOT NULL,
      USUA_Interno BIGINT NOT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT PK_Registros PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, REGI_Interno)

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT FK_Registros_Usuarios FOREIGN KEY (USUA_Interno)
      REFERENCES POSAD_Usuarios (USUA_Interno)

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT FK_Registros_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT FK_Registros_Turnos FOREIGN KEY (EMPR_Interno, SUCU_Interno, TURN_Interno)
      REFERENCES POSAD_Turnos (EMPR_Interno, SUCU_Interno, TURN_Interno)

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT FK_Registros_Habitaciones FOREIGN KEY (EMPR_Interno, SUCU_Interno, HABI_Interno)
      REFERENCES POSAD_Habitaciones (EMPR_Interno, SUCU_Interno, HABI_Interno)

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT FK_Registros_Entidades FOREIGN KEY (EMPR_Interno, ENTI_Interno)
      REFERENCES POSAD_Entidades (EMPR_Interno, ENTI_Interno)

   ALTER TABLE POSAD_Registros 
      ADD CONSTRAINT FK_Registros_TablasTDI FOREIGN KEY (EMPR_Interno,TABL_TablaTDI, TABL_InternoTDI)
      REFERENCES POSAD_Tablas (EMPR_Interno,TABL_Tabla, TABL_Interno)
END 
GO 


IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_DetallesPagosRegistros'))
BEGIN 
   
   CREATE TABLE POSAD_DetallesPagosRegistros (
      EMPR_Interno BIGINT NOT NULL,
      SUCU_Interno BIGINT NOT NULL,
      REGI_Interno BIGINT NOT NULL,
      PAGO_Item INT NOT NULL,
      PAGO_Tipo CHAR(1) NOT NULL, --(E:Entrada, S:Salida) : Pago - A:Monto Adicional
      PAGO_MontoCancelado DECIMAL(20,8) NOT NULL,
      PAGO_FechaHoraRegistro DATETIME NOT NULL,
      USUA_Interno BIGINT NOT NULL,
      RETI_Interno BIGINT NULL,
      CIER_Interno BIGINT NULL,
	   AUDI_UsuarioCreacion VARCHAR(25) NOT NULL,
	   AUDI_FechaCreacion DATETIME NOT NULL,
	   AUDI_UsuarioModificacion VARCHAR(25) NULL,
	   AUDI_FechaModificacion DATETIME NULL,
   )

   ALTER TABLE POSAD_DetallesPagosRegistros 
      ADD CONSTRAINT PK_DetallesPagosRegistros PRIMARY KEY CLUSTERED (EMPR_Interno, SUCU_Interno, REGI_Interno, PAGO_Item)

   ALTER TABLE POSAD_DetallesPagosRegistros 
      ADD CONSTRAINT FK_DetallesPagosRegistros_Registros FOREIGN KEY (EMPR_Interno, SUCU_Interno, REGI_Interno)
      REFERENCES POSAD_Registros (EMPR_Interno, SUCU_Interno, REGI_Interno)

   ALTER TABLE POSAD_DetallesPagosRegistros 
      ADD CONSTRAINT FK_DetallesPagosRegistros_Usuarios FOREIGN KEY (USUA_Interno)
      REFERENCES POSAD_Usuarios (USUA_Interno)

   ALTER TABLE POSAD_DetallesPagosRegistros 
      ADD CONSTRAINT FK_DetallesPagosRegistros_Sucursales FOREIGN KEY (EMPR_Interno, SUCU_Interno)
      REFERENCES POSAD_Sucursales (EMPR_Interno, SUCU_Interno)
   
   ALTER TABLE POSAD_DetallesPagosRegistros 
      ADD CONSTRAINT FK_DetallesPagosRegistros_Cierres FOREIGN KEY (EMPR_Interno, SUCU_Interno, CIER_Interno)
      REFERENCES POSAD_Cierres (EMPR_Interno, SUCU_Interno, CIER_Interno)

   ALTER TABLE POSAD_DetallesPagosRegistros 
      ADD CONSTRAINT FK_DetallesPagosRegistros_Retiros FOREIGN KEY (EMPR_Interno,SUCU_Interno, RETI_Interno)
      REFERENCES POSAD_Retiros (EMPR_Interno, SUCU_Interno, RETI_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Meses'))
BEGIN 
   CREATE TABLE POSAD_Meses (
      MES_Interno INT NOT NULL,
      MES_Orden INT NOT NULL,
      MES_Nombre1 VARCHAR(25) NOT NULL,
      MES_Nombre2 VARCHAR(25) NOT NULL,
      MES_Abreviatura1 VARCHAR(5) NOT NULL,
      MES_Abreviatura2 VARCHAR(5) NOT NULL,
      MES_DiasMinimo INT NOT NULL,
      MES_DiasMaximo INT NOT NULL,
      MES_CantidadFeriados INT NOT NULL
   )

   ALTER TABLE POSAD_Meses 
      ADD CONSTRAINT PK_Meses PRIMARY KEY CLUSTERED (MES_Interno)
END 
GO 

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type_desc = N'USER_TABLE' AND object_id = OBJECT_ID(N'POSAD_Dias'))
BEGIN 
   CREATE TABLE POSAD_Dias (
      DIA_Interno INT NOT NULL,
      DIA_Orden INT NOT NULL,
      DIA_Nombre1 VARCHAR(25) NOT NULL,
      DIA_Nombre2 VARCHAR(25) NOT NULL,
      DIA_Abreviatura1 VARCHAR(5) NOT NULL,
      DIA_Abreviatura2 VARCHAR(5) NOT NULL,
      DIA_HorasLaborablesMinimo INT NOT NULL,
      DIA_HorasLaborablesMaximo INT NOT NULL,
   )

   ALTER TABLE POSAD_Dias 
      ADD CONSTRAINT PK_Dias PRIMARY KEY CLUSTERED (DIA_Interno)
END 
GO 