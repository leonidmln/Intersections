USE [master]
GO
/****** Object:  Database [Intersections]    Script Date: 5/8/2024 4:23:18 PM ******/
CREATE DATABASE [Intersections]
GO

USE [Intersections]
GO
/****** Object:  UserDefinedFunction [dbo].[CheckSegmentIntersection]    Script Date: 5/8/2024 4:23:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CheckSegmentIntersection]
(
    @startX1 FLOAT,
    @startY1 FLOAT,
    @endX1 FLOAT,
    @endY1 FLOAT,
    @startX2 FLOAT,
    @startY2 FLOAT,
    @endX2 FLOAT,
    @endY2 FLOAT
)
RETURNS BIT
AS
BEGIN
    DECLARE @denominator FLOAT;
    DECLARE @tSelf FLOAT;
    DECLARE @tOther FLOAT;

    SET @denominator = (@endY2 - @startY2) * (@endX1 - @startX1) - (@endX2 - @startX2) * (@endY1 - @startY1);

    -- the segments are parallel
    IF @denominator = 0
        RETURN 0;

    SET @tSelf = ((@endX2 - @startX2) * (@startY1 - @startY2) - (@endY2 - @startY2) * (@startX1 - @startX2)) / @denominator;
    SET @tOther = ((@endX1 - @startX1) * (@startY1 - @startY2) - (@endY1 - @startY1) * (@startX1 - @startX2)) / @denominator;

    -- check an intersection
    IF @tSelf >= 0 AND @tSelf <= 1 AND @tOther >= 0 AND @tOther <= 1
        RETURN 1;
    
    RETURN 0;
END;
GO
/****** Object:  Table [dbo].[Rectangle]    Script Date: 5/8/2024 4:23:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rectangle](
	[RectangleId] [int] NOT NULL,
	[RectangleName] [nvarchar](100) NOT NULL,
	[Geometry] [geometry] NULL,
 CONSTRAINT [PK_Rectangle] PRIMARY KEY CLUSTERED 
(
	[RectangleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RectangleSegment]    Script Date: 5/8/2024 4:23:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RectangleSegment](
	[RectangleSegmentId] [int] IDENTITY(1,1) NOT NULL,
	[RectangleId] [int] NOT NULL,
	[StartX] [float] NOT NULL,
	[StartY] [float] NOT NULL,
	[EndX] [float] NOT NULL,
	[EndY] [float] NOT NULL,
 CONSTRAINT [PK_RectanleSegment] PRIMARY KEY CLUSTERED 
(
	[RectangleSegmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Rectangle] ([RectangleId], [RectangleName], [Geometry]) VALUES (1, N'Rectangle 1', 0x00000000010405000000000000000000000000000000000000000000000000001440000000000000000000000000000014400000000000000840000000000000000000000000000008400000000000000000000000000000000001000000020000000001000000FFFFFFFF0000000003)
GO
INSERT [dbo].[Rectangle] ([RectangleId], [RectangleName], [Geometry]) VALUES (2, N'Rectangle 2', 0x00000000010405000000000000000000000000000000000000400000000000001040000000000000004000000000000010400000000000000000000000000000000000000000000000000000000000000000000000000000004001000000020000000001000000FFFFFFFF0000000003)
GO
INSERT [dbo].[Rectangle] ([RectangleId], [RectangleName], [Geometry]) VALUES (3, N'Rectangle 3', 0x00000000010405000000000000000000244000000000000024400000000000002E4000000000000024400000000000002E400000000000002E4000000000000024400000000000002E400000000000002440000000000000244001000000020000000001000000FFFFFFFF0000000003)
GO
INSERT [dbo].[Rectangle] ([RectangleId], [RectangleName], [Geometry]) VALUES (4, N'Rectangle 4', 0x00000000010405000000000000000000084000000000000008400000000000001840000000000000084000000000000018400000000000001840000000000000084000000000000018400000000000000840000000000000084001000000020000000001000000FFFFFFFF0000000003)
GO
SET IDENTITY_INSERT [dbo].[RectangleSegment] ON 
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (17, 1, 0, 0, 5, 0)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (18, 1, 5, 0, 5, 3)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (19, 1, 5, 3, 0, 3)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (20, 1, 0, 3, 0, 0)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (21, 2, 0, 2, 4, 2)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (22, 2, 4, 2, 4, 0)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (23, 2, 4, 0, 0, 0)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (24, 2, 0, 0, 0, 2)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (25, 3, 10, 10, 15, 10)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (26, 3, 15, 10, 15, 15)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (27, 3, 15, 15, 10, 15)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (28, 3, 10, 15, 10, 10)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (29, 4, 3, 3, 6, 3)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (30, 4, 6, 3, 6, 6)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (31, 4, 6, 6, 3, 6)
GO
INSERT [dbo].[RectangleSegment] ([RectangleSegmentId], [RectangleId], [StartX], [StartY], [EndX], [EndY]) VALUES (32, 4, 3, 6, 3, 3)
GO
SET IDENTITY_INSERT [dbo].[RectangleSegment] OFF
GO
ALTER TABLE [dbo].[RectangleSegment]  WITH CHECK ADD  CONSTRAINT [FK_RectangleSegment_Rectangle] FOREIGN KEY([RectangleId])
REFERENCES [dbo].[Rectangle] ([RectangleId])
GO
ALTER TABLE [dbo].[RectangleSegment] CHECK CONSTRAINT [FK_RectangleSegment_Rectangle]
GO
USE [master]
GO
ALTER DATABASE [Intersections] SET  READ_WRITE 
GO
