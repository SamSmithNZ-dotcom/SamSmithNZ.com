﻿CREATE TABLE [dbo].[wc_game] (
	[game_code] INT NOT NULL,
	[tournament_code] INT NOT NULL,
	[game_number] INT NOT NULL,
	[game_time] DATETIME NULL,
	[round_number] INT NULL,
	[round_code] VARCHAR (10) NULL,
	[location] VARCHAR (100) NOT NULL,
	[team_1_code] INT NOT NULL,
	[team_2_code] INT NOT NULL,
	[team_1_normal_time_score] INT NULL,
	[team_1_extra_time_score] INT NULL,
	[team_1_penalties_score] INT NULL,
	[team_2_normal_time_score] INT NULL,
	[team_2_extra_time_score] INT NULL,
	[team_2_penalties_score] INT NULL,
	[team_1_elo_rating] INT NULL, 
	[team_2_elo_rating] INT NULL, 
	[team_1_pregame_elo_rating] INT NULL, 
	[team_2_pregame_elo_rating] INT NULL,
	[team_1_postgame_elo_rating] INT NULL, 
	[team_2_postgame_elo_rating] INT NULL,
	CONSTRAINT [PK_wc_game] PRIMARY KEY CLUSTERED ([game_code] ASC)
);
GO
CREATE NONCLUSTERED INDEX IX_Game_TournamentCode
    ON dbo.wc_game (tournament_code);  
GO
