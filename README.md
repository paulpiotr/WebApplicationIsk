# WebApplicationIsk
WebApplicationIsk
- kongiguracja połączenia z bazą banych w pliku WebApplicationIsk\appsettings.json
- blok ConnectionStrings\WebApplicationIsk

```json
"ConnectionStrings": {
     "WebApplicationIsk": "Server=.\\SQLExpress;Database=isk;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
- po kompilacji jeśli w bazie jest pusto wymusi rejestrację.
- używałem bazy danych SQLExpress
- poniżej schemat user do wgrania

```tsql
USE [isk]
GO
/****** Object:  Table [dbo].[user] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](32) NULL,
	[surname] [varchar](32) NULL,
	[email] [varchar](32) NOT NULL,
	[role] [varchar](32) NULL,
	[login] [varchar](32) NOT NULL,
	[password] [text] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_user_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_user_login] UNIQUE NONCLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[user] ADD  DEFAULT (getdate()) FOR [created]
GO
```

- W razie pytań jestem do dyspozycji.