USE [airline_planner]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 6/13/2017 9:15:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cities_flights]    Script Date: 6/13/2017 9:15:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities_flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city_departure_id] [int] NULL,
	[city_arrival_id] [int] NULL,
	[flight_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 6/13/2017 9:15:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[departure_time] [datetime] NULL,
	[status] [varchar](255) NULL
) ON [PRIMARY]
GO
