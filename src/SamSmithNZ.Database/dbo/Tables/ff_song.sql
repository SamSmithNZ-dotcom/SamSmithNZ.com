﻿CREATE TABLE [dbo].[ff_song] (
    [song_key]    INT            NOT NULL,
    [song_name]   VARCHAR (100)  NULL,
    [album_key]   INT            NULL,
    [song_order]  INT            NULL,
    [song_lyrics] VARCHAR (8000) NULL,
    [song_notes]  VARCHAR (2000) NULL,
    [song_image]  VARCHAR (200)  NULL,
    CONSTRAINT [PK_ff_track] PRIMARY KEY CLUSTERED ([song_key] ASC)
);




GO


