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
	EMAIL nvarchar(256) not null,

	USERNAME nvarchar(64) not null,
	PSWD_HASH nvarchar(2048) not null,
	PSWD_SALT nvarchar(2048) not null,

	REGISTERED_ON_UTC datetime not null,

	USER_STATUS char(1) not null
);
ALTER TABLE [es_tbUsers] ADD CONSTRAINT PK_es_tbUsers_ID PRIMARY KEY (ID);
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_BIRTHDAY CHECK (BIRTHDAY < DATEADD(yyyy, -13, GETDATE()));
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_GENDER CHECK (GENDER IN ('M', 'F'));
CREATE INDEX IX_es_tbUsers_USERNAME ON [es_tbUsers](USERNAME);
ALTER TABLE [es_tbUsers] ADD CONSTRAINT DF_es_tbUsers_REGISTERED_ON_UTC DEFAULT (GETUTCDATE()) FOR REGISTERED_ON_UTC;
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_REGISTERED_ON_UTC CHECK (REGISTERED_ON_UTC <= GETUTCDATE());
ALTER TABLE [es_tbUsers] ADD CONSTRAINT CK_es_tbUsers_USER_STATUS CHECK (USER_STATUS IN ('A', 'D', 'I', 'O'))

--CREATE TABLE [es_tbLogins](
--	ID uniqueidentifier not null,
--	IDes_tbUsers bigint not null,

--	LOGIN_TIME_UTC datetime not null,
--	LOGOUT_TIME_UTC datetime,

--	USER_AGENT nvarchar(256) not null,
--	USER_IP nvarchar(15) not null
--);
--ALTER TABLE [es_tbLogins] ADD CONSTRAINT PK_es_tbLogins_ID PRIMARY KEY NONCLUSTERED (ID);
--ALTER TABLE [es_tbLogins] ADD CONSTRAINT DF_es_tbLogins_ID DEFAULT (NEWID()) FOR ID;
--CREATE INDEX IX_es_tbLogins_IDes_tbUsers ON [es_tbLogins](ID);
--ALTER TABLE [es_tbLogins] ADD CONSTRAINT DF_es_tbLogins_LOGIN_TIME_UTC DEFAULT (GETUTCDATE()) FOR LOGIN_TIME_UTC;
--ALTER TABLE [es_tbLogins] ADD CONSTRAINT CK_es_tbLogins_LOGIN_TIME_UTC CHECK (LOGIN_TIME_UTC <= GETUTCDATE());
--ALTER TABLE [es_tbLogins] ADD CONSTRAINT CK_es_tbLogins_LOGOUT_TIME_UTC CHECK (LOGIN_TIME_UTC < LOGOUT_TIME_UTC);

CREATE TABLE [es_tbRooms](
	ID bigint identity(1,1) not null,
	IDes_tbUsers bigint not null,
	ROOM_NAME nvarchar(64) not null,
	ROOM_DESCRIPTION nvarchar(256),
	ROOM_CREATED_UTC datetime not null
);
ALTER TABLE [es_tbRooms] ADD CONSTRAINT PK_es_tbRooms_ID PRIMARY KEY (ID);
ALTER TABLE [es_tbRooms] ADD CONSTRAINT CK_es_tbRooms_ROOM_CREATED_UTC CHECK (ROOM_CREATED_UTC <= GETUTCDATE());

CREATE TABLE [es_tbRoomParticipants](
	ID uniqueidentifier not null,
	IDes_tbRooms bigint not null,
	IDes_tbUsers bigint not null,
);
ALTER TABLE [es_tbRoomParticipants] ADD CONSTRAINT PK_es_tbRoomParticipants_ID PRIMARY KEY NONCLUSTERED (ID);
ALTER TABLE [es_tbRoomParticipants] ADD CONSTRAINT DF_es_tbRoomParticipants_ID DEFAULT (NEWID()) FOR ID;

CREATE TABLE [es_tbMessages](
	ID uniqueidentifier not null,
	IDes_tbRooms bigint not null,
	IDes_tbUsers bigint not null,
	SENT_UTC datetime not null,
	SERVER_RECEIVED_UTC datetime not null,
	CONTENT nvarchar(max) not null
);
ALTER TABLE [es_tbMessages] ADD CONSTRAINT PK_es_tbMessages_ID PRIMARY KEY (ID);
ALTER TABLE [es_tbMessages] ADD CONSTRAINT DF_es_tbMessages_ID DEFAULT (NEWID()) FOR ID;
ALTER TABLE [es_tbMessages] ADD CONSTRAINT CK_es_tbMessages_SENT_UTC CHECK (SENT_UTC <= GETUTCDATE());

CREATE TABLE [es_tbFriendships](
	ID uniqueidentifier not null,
	IDes_tbUsers_SENDER bigint not null,
	IDes_tbUsers_RECIPIENT bigint not null,

	REQUEST_SERVER_RECEIVED_UTC datetime not null,
	REQUEST_ACCEPTED_UTC datetime,
);
ALTER TABLE [es_tbFriendships] ADD CONSTRAINT PK_es_tbFriendships_ID PRIMARY KEY NONCLUSTERED (ID);
ALTER TABLE [es_tbFriendships] ADD CONSTRAINT DF_es_tbFriendships_ID DEFAULT (NEWID()) FOR ID;
ALTER TABLE [es_tbFriendships] ADD CONSTRAINT CK_es_tbFriendships_REQUEST_SERVER_RECEIVED_UTC CHECK (REQUEST_SERVER_RECEIVED_UTC <= GETUTCDATE());
ALTER TABLE [es_tbFriendships] ADD CONSTRAINT CK_es_tbFriendships_REQUEST_ACCEPTED_UTC CHECK (REQUEST_SERVER_RECEIVED_UTC < REQUEST_ACCEPTED_UTC AND REQUEST_ACCEPTED_UTC <= GETUTCDATE());

/* FOREIGN KEYS */
--ALTER TABLE [es_tbLogins] ADD CONSTRAINT FK_es_tbLogins_IDes_tbUsers FOREIGN KEY (IDes_tbUsers) REFERENCES es_tbUsers(ID);
ALTER TABLE [es_tbRooms] ADD CONSTRAINT FK_es_tbRooms_IDes_tbUsers FOREIGN KEY (IDes_tbUsers) REFERENCES es_tbUsers(ID);
ALTER TABLE [es_tbRoomParticipants] ADD CONSTRAINT FK_es_tbRoomParticipants_IDes_tbRooms FOREIGN KEY (IDes_tbRooms) REFERENCES es_tbRooms(ID);
ALTER TABLE [es_tbRoomParticipants] ADD CONSTRAINT FK_es_tbRoomParticipants_IDes_tbUsers FOREIGN KEY (IDes_tbUsers) REFERENCES es_tbUsers(ID);
ALTER TABLE [es_tbMessages] ADD CONSTRAINT FK_es_tbMessages_IDes_tbRooms FOREIGN KEY (IDes_tbRooms) REFERENCES es_tbRooms(ID);
ALTER TABLE [es_tbMessages] ADD CONSTRAINT FK_es_tbMessages_IDes_tbUsers FOREIGN KEY (IDes_tbUsers) REFERENCES es_tbUsers(ID);
ALTER TABLE [es_tbFriendships] ADD CONSTRAINT FK_es_tbFriendships_IDes_tbUsers_SENDER FOREIGN KEY (IDes_tbUsers_SENDER) REFERENCES es_tbUsers(ID);
ALTER TABLE [es_tbFriendships] ADD CONSTRAINT FK_es_tbFriendships_IDes_tbUsers_RECIPIENT FOREIGN KEY (IDes_tbUsers_RECIPIENT) REFERENCES es_tbUsers(ID);