USE [master];

IF EXISTS (SELECT * FROM sys.databases WHERE name='evo_dbChatServer')
BEGIN
	ALTER DATABASE evo_dbChatServer SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE evo_dbChatServer;
END

CREATE DATABASE evo_dbChatServer;
GO

USE [evo_dbChatServer];

CREATE TABLE [es_tbUsers](
	ID bigint identity(1,1) not null,

	FIRST_NAME nvarchar(64) not null,
	MIDDLE_NAME nvarchar(64),
	LAST_NAME nvarchar(64) not null,
	BIRTHDAY date not null,
	GENDER char(1) not null,

	USERNAME nvarchar(64) not null,
	PSWD_HASH nvarchar(2048) not null,
	PSWD_SALT nvarchar(2048) not null,

	REGISTERED_ON_UTC datetime not null
);
ALTER TABLE [es_tbUsers] ADD CONSTRAINT PK_es_tbUsers_ID PRIMARY KEY (ID);
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_BIRTHDAY CHECK (BIRTHDAY < DATEADD(yyyy, -13, GETDATE()));
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_GENDER CHECK (GENDER IN ('M', 'F'));
CREATE INDEX IX_es_tbUsers_USERNAME ON [es_tbUsers](USERNAME);
ALTER TABLE [es_tbUsers] ADD CONSTRAINT DF_es_tbUsers_REGISTERED_ON_UTC DEFAULT (GETUTCDATE()) FOR REGISTERED_ON_UTC;
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_REGISTERED_ON_UTC CHECK (REGISTERED_ON_UTC <= GETUTCDATE());

CREATE TABLE [es_tbLogins](
	ID uniqueidentifier not null,
	IDes_tbUsers bigint not null,

	LOGIN_TIME_UTC datetime not null,
	LOGOUT_TIME_UTC datetime,

	USER_AGENT nvarchar(256) not null,
	USER_IP nvarchar(15) not null
);
ALTER TABLE [es_tbLogins] ADD CONSTRAINT PK_es_tbLogins_ID PRIMARY KEY NONCLUSTERED (ID);
CREATE INDEX IX_es_tbLogins_IDes_tbUsers ON [es_tbLogins](ID);
ALTER TABLE [es_tbLogins] ADD CONSTRAINT DF_es_tbLogins_LOGIN_TIME_UTC DEFAULT (GETUTCDATE()) FOR LOGIN_TIME_UTC;
ALTER TABLE [es_tbLogins] ADD CONSTRAINT CK_es_tbLogins_LOGIN_TIME_UTC CHECK (LOGIN_TIME_UTC <= GETUTCDATE());
ALTER TABLE [es_tbLogins] ADD CONSTRAINT CK_es_tbLogins_LOGOUT_TIME_UTC CHECK (LOGIN_TIME_UTC < LOGOUT_TIME_UTC);

/* FOREIGN KEYS */
ALTER TABLE [es_tbLogins] ADD CONSTRAINT FK_es_tbLogins_IDes_tbUsers FOREIGN KEY (IDes_tbUsers) REFERENCES es_tbUsers(ID);