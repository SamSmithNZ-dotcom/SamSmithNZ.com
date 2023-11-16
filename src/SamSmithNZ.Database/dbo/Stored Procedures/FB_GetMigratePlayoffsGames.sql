﻿CREATE PROCEDURE [dbo].[FB_GetMigratePlayoffsGames]
	@TournamentCode INT,
	@RoundNumber INT
AS
BEGIN
	SELECT 1 AS RowType, --1 is a team
		g.round_number AS RoundNumber, 
		g.round_code AS RoundCode, 
		r.round_name AS RoundName, 
		g.game_code AS GameCode, 
		g.game_number AS GameNumber, 
		g.game_time AS GameTime, 
		t1.team_code AS Team1Code, 
		t1.team_name AS Team1Name, 
		g.team_1_normal_time_score AS Team1NormalTimeScore, 
		g.team_1_extra_time_score AS Team1ExtraTimeScore, 
		g.team_1_penalties_score AS Team1PenaltiesScore,
		g.team_1_pregame_elo_rating AS Team1PreGameEloRating,
		g.team_1_postgame_elo_rating AS Team1PostGameEloRating,
		t2.team_code AS Team2Code, 
		t2.team_name AS Team2Name, 
		g.team_2_normal_time_score AS Team2NormalTimeScore, 
		g.team_2_extra_time_score AS Team2ExtraTimeScore, 
		g.team_2_penalties_score AS Team2PenaltiesScore, 
		g.team_2_pregame_elo_rating AS Team2PreGameEloRating,
		g.team_2_postgame_elo_rating AS Team2PostGameEloRating,
		t1.flag_name AS Team1FlagName, 
		t2.flag_name AS Team2FlagName,
		CONVERT(BIT,CASE WHEN g.team_1_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team1Withdrew,
		CONVERT(BIT,CASE WHEN g.team_2_normal_time_score is NULL THEN 1 ELSE 0 END) AS Team2Withdrew,
		g.[location] AS [Location], 
		t.tournament_code AS TournamentCode, 
		t.[name] AS TournamentName, 
		ISNULL(te.coach_name,'') AS CoachName, 
		ISNULL(t3.flag_name,'') AS CoachFlag,
		NULL AS IsPenalty, 
		NULL AS IsOwnGoal, 
		1 AS SortOrder  
	FROM wc_game g
	JOIN wc_team t1 ON g.team_1_code = t1.team_code
	JOIN wc_team t2 ON g.team_2_code = t2.team_code
	JOIN wc_round r ON g.round_code = r.round_code
	JOIN wc_tournament t ON g.tournament_code = t.tournament_code	
	LEFT JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
	LEFT JOIN wc_team t3 ON te.coach_nationality = t3.team_name
	WHERE g.tournament_code = @TournamentCode
	AND g.round_number = @RoundNumber
	AND g.tournament_code NOT IN (SELECT DISTINCT p.tournament_code 
									FROM wc_tournament_format_playoff_setup p)
	ORDER BY g.game_time, g.game_number, SortOrder
END
GO