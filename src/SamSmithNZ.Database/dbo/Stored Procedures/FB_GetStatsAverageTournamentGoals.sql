CREATE PROCEDURE [dbo].[FB_GetStatsAverageTournamentGoals]
	@CompetitionCode INT = NULL,
	@TournamentCode INT = NULL
AS
BEGIN
	DECLARE @CurrentTime DATETIME 
	SELECT @CurrentTime = GETDATE()

	SELECT t.tournament_code AS TournamentCode,
		t.[name] as TournamentName,
		(SELECT COUNT(g4.game_code) FROM wc_game g4 WHERE g4.tournament_code = t.tournament_code AND g4.game_time <= @CurrentTime) AS TotalGamesCompleted,
		SUM(g.team_1_normal_time_score) + SUM(ISNULL(g.team_1_extra_time_score,0)) + SUM(g.team_2_normal_time_score) + SUM(ISNULL(g.team_2_extra_time_score,0)) AS TotalGoals,
		CASE 
			WHEN (SELECT COUNT(g4.game_code) FROM wc_game g4 WHERE g4.tournament_code = t.tournament_code AND g4.game_time <= @CurrentTime) = 0 THEN 0
			ELSE ROUND(CONVERT(DECIMAL(10,2),SUM(g.team_1_normal_time_score) + SUM(ISNULL(g.team_1_extra_time_score,0)) + SUM(g.team_2_normal_time_score) + SUM(ISNULL(g.team_2_extra_time_score,0))) / 
				CONVERT(DECIMAL(10,2),(SELECT COUNT(g4.game_code) FROM wc_game g4 WHERE g4.tournament_code = t.tournament_code AND g4.game_time <= @CurrentTime)),2)
		END AS AverageGoalsPerGame
	FROM wc_tournament t
	LEFT JOIN wc_game g ON t.tournament_code = g.tournament_code
	WHERE (t.tournament_code = @TournamentCode OR @TournamentCode IS NULL)
	AND (t.competition_code = @CompetitionCode OR @CompetitionCode IS NULL)
	AND t.competition_code != 4	AND t.competition_code != 2
	GROUP BY t.tournament_code, t.[name]
	HAVING SUM(g.team_1_normal_time_score) + SUM(ISNULL(g.team_1_extra_time_score,0)) + SUM(g.team_2_normal_time_score) + SUM(ISNULL(g.team_2_extra_time_score,0)) > 0	
	ORDER BY AverageGoalsPerGame DESC
END
GO