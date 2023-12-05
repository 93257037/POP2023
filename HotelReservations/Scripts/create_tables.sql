

CREATE TABLE room_type (
	room_type_id INT IDENTITY(1,1) PRIMARY KEY,
	room_type_name VARCHAR(255) NOT NULL UNIQUE,
	room_type_is_active BIT NOT NULL
);

CREATE TABLE room (
	room_id INT IDENTITY(1,1) PRIMARY KEY,
	room_number VARCHAR(25) NOT NULL UNIQUE,
	has_TV BIT NOT NULL,
	has_mini_bar BIT NOT NULL,
	room_is_active BIT NOT NULL,
	room_type_id INT NOT NULL,
	CONSTRAINT FK_ROOM_ROOM_TYPE
	FOREIGN KEY (room_type_id) REFERENCES dbo.room_type (room_type_id)
);


CREATE TABLE "user" (
	"user_id" INT IDENTITY(1,1) PRIMARY KEY,
	"first_name" VARCHAR(40) NOT NULL,
	"last_name" VARCHAR(50) NOT NULL,
	"JMBG" VARCHAR(13) NOT NULL,
	"username" VARCHAR(20) NOT NULL UNIQUE,
	"password" VARCHAR(50) NOT NULL,
	"user_type" VARCHAR(15) NOT NULL
);

SELECT * FROM dbo."user" u
WHERE u."user_type" = 'administrator'

--CREATE TABLE "administrator" (
--	"user_id" INT PRIMARY KEY,
--	CONSTRAINT FK_ADMIN_USER
--	FOREIGN KEY ("user_id") REFERENCES dbo."user" ("user_id")
--);

--SELECT * FROM dbo.administrator a
--LEFT JOIN dbo."user" u ON u."user_id" = a."user_id" 
