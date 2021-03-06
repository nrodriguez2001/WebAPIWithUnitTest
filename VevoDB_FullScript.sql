USE [Vevo]
GO
/****** Object:  Table [dbo].[Artists]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artists](
	[artist_id] [int] IDENTITY(1,1) NOT NULL,
	[urlSafeName] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Artists] PRIMARY KEY CLUSTERED 
(
	[artist_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Videos]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[video_id] [int] IDENTITY(1,1) NOT NULL,
	[artist_id] [int] NOT NULL,
	[isrc] [nvarchar](50) NOT NULL,
	[urlSafeTitle] [nvarchar](250) NOT NULL,
	[VideoTitle] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED 
(
	[video_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[VideosByArtist]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VideosByArtist]
AS
SELECT        dbo.Videos.isrc, dbo.Videos.VideoTitle, dbo.Videos.urlSafeTitle, dbo.Artists.name, dbo.Artists.urlSafeName
FROM            dbo.Artists INNER JOIN
                         dbo.Videos ON dbo.Artists.artist_id = dbo.Videos.artist_id


GO
SET IDENTITY_INSERT [dbo].[Artists] ON 

INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (1, N'big-time-rush
', N'Big Time Rush
')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (2, N'pitbull', N'Pitbull')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (3, N'katy-perry', N'Katy Perry')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (4, N'enrique-iglesias', N'Enrique Iglesias')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (5, N'katy-perry', N'Katy Perry')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (6, N'calvin-harris', N'Calvin Harris')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (7, N'one-republic', N'One Republic')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (8, N'john-legend', N'John Legend
')
INSERT [dbo].[Artists] ([artist_id], [urlSafeName], [name]) VALUES (17, N'shakira
', N'Shakira
')
SET IDENTITY_INSERT [dbo].[Artists] OFF
SET IDENTITY_INSERT [dbo].[Videos] ON 

INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (1, 1, N'USSM21200785', N'windows-down', N'Windows Down')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (2, 17, N'USRV81400102', N'la-la-la-(brazil-2014)', N'La La La (Brazil 2014)')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (3, 2, N'USRV81400057', N'we-are-one-(ole-ola)-[the-official-2014-fifa-world-cup-song]-(olodum-mix)', N'We Are One (Ole Ola) [The Official 2014 FIFA World Cup Song] (Olodum Mix)')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (4, 4, N'GBUV71400428', N'bailando-(español)', N'Bailando (Español)')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (5, 5, N'USUV71400083', N'dark-horse-(official)', N'Dark Horse (Official)')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (1014, 6, N'GB1101400141
', N'summer
', N'Summer
')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (1016, 7, N'USUV71301101
', N'counting-stars
', N'Counting Stars
')
INSERT [dbo].[Videos] ([video_id], [artist_id], [isrc], [urlSafeTitle], [VideoTitle]) VALUES (1017, 8, N'USSM21302088
', N'all-of-me
', N'All of Me
')
SET IDENTITY_INSERT [dbo].[Videos] OFF
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_Videos_Artists] FOREIGN KEY([artist_id])
REFERENCES [dbo].[Artists] ([artist_id])
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_Videos_Artists]
GO
/****** Object:  StoredProcedure [dbo].[udp_Artists_del]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Artists_del]
	@artist_id int,
	@result INT OUTPUT
AS
SET NOCOUNT ON

BEGIN
	IF(EXISTS(SELECT artist_id FROM dbo.Artists WHERE artist_id = @artist_id ))
	BEGIN
		DELETE FROM Videos
		WHERE [artist_id] = @artist_id

		DELETE FROM Artists
		WHERE [artist_id] = @artist_id

		SET @result = 0
	END
	ELSE
	BEGIN
		SET @result = 1
	END
END

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[udp_Artists_lst]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Artists_lst]ASSET NOCOUNT ONSELECT [artist_id], 	[urlSafeName], 	[name]FROM ArtistsSET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[udp_Artists_sel]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Artists_sel]	@artist_id intASSET NOCOUNT ONSELECT [artist_id], 	[urlSafeName], 	[name]FROM ArtistsWHERE [artist_id] = @artist_idSET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[udp_Artists_ups]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Artists_ups]

	@artist_id int,
	@urlSafeName nvarchar(50),
	@name nvarchar(50),
	@id INT OUTPUT
AS
SET NOCOUNT ON
IF @artist_id = 0 BEGIN
	INSERT INTO Artists (
		[urlSafeName],
		[name]
	)
	VALUES (
		@urlSafeName,
		@name
	)
    
	SET @id = SCOPE_IDENTITY()
	
END
ELSE BEGIN
	UPDATE Artists SET 
		[urlSafeName] = @urlSafeName,
		[name] = @name
	WHERE [artist_id] = @artist_id
	SET @id =@artist_id
END

SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[udp_Videos_del]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[udp_Videos_del]
	@video_id int,
	@result INT OUTPUT
AS
SET NOCOUNT ON
BEGIN
	IF(EXISTS(SELECT video_id FROM dbo.Videos WHERE video_id = @video_id ))
	BEGIN
		DELETE FROM Videos
		WHERE [video_id] = @video_id

		SET @result = 0
	END
	ELSE
	BEGIN
		SET @result = 1
	END
END

SET NOCOUNT OFF

GO
/****** Object:  StoredProcedure [dbo].[udp_Videos_lst]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Videos_lst]ASSET NOCOUNT ONSELECT [video_id], 	[artist_id], 	[isrc], 	[urlSafeTitle], 	[VideoTitle]FROM VideosSET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[udp_Videos_sel]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Videos_sel]	@video_id intASSET NOCOUNT ONSELECT [video_id], 	[artist_id], 	[isrc], 	[urlSafeTitle], 	[VideoTitle]FROM VideosWHERE [video_id] = @video_idSET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[udp_Videos_ups]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_Videos_ups]

	@video_id int,
	@artist_id int,
	@isrc nvarchar(50),
	@urlSafeTitle nvarchar(250),
	@VideoTitle nvarchar(max),
	@id INT OUTPUT
AS
SET NOCOUNT ON
IF @video_id = 0 BEGIN
	INSERT INTO Videos (
		[artist_id],
		[isrc],
		[urlSafeTitle],
		[VideoTitle]
	)
	VALUES (
		@artist_id,
		@isrc,
		@urlSafeTitle,
		@VideoTitle
	)

	SET @id = SCOPE_IDENTITY()
END
ELSE BEGIN
	UPDATE Videos SET 
		[artist_id] = @artist_id,
		[isrc] = @isrc,
		[urlSafeTitle] = @urlSafeTitle,
		[VideoTitle] = @VideoTitle
	WHERE [video_id] = @video_id
	SET @id = @video_id
END

GO
/****** Object:  StoredProcedure [dbo].[udp_VideosByArtist_sel]    Script Date: 6/21/2015 9:03:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[udp_VideosByArtist_sel]
	@artist_id int
AS
SET NOCOUNT ON

SELECT [video_id], 
	[artist_id], 
	[isrc], 
	[urlSafeTitle], 
	[VideoTitle]
FROM Videos
WHERE [artist_id] = @artist_id


SET NOCOUNT OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[22] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Artists"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 134
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Videos"
            Begin Extent = 
               Top = 9
               Left = 324
               Bottom = 208
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 6195
         Width = 6165
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VideosByArtist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VideosByArtist'
GO
