﻿--CREATE PROCEDURE [dbo].[spWC_GetStats_TotalGoalsByTournament]
--	@tournament_code INT
--AS
--IF (@tournament_code > 0)
--BEGIN
--	SELECT t.team_code, t.team_name, sum(goals) AS sum_of_goals
--	FROM vWC_GameScoreSummary gs
--	INNER JOiN wc_team t ON gs.team_code = t.team_code
--	WHERE gs.tournament_code = @tournament_code
--	GROUP BY t.team_code, t.team_name
--	ORDER BY sum(goals) DESC
--END
--ELSE
--BEGIN
--	SELECT t.tournament_code, t.name AS tournament_name, 
--		COUNT(g.game_code) AS game_count,
--		ISNULL(sum(ISNULL(team_1_normal_time_score,0)) + sum(ISNULL(team_1_extra_time_score,0)) + 
--		sum(ISNULL(team_2_normal_time_score,0)) + sum(ISNULL(team_2_extra_time_score,0)),0) AS goals_scored,
--		ISNULL(CONVERT(decimal(8,2),(sum(ISNULL(team_1_normal_time_score,0)) + sum(ISNULL(team_1_extra_time_score,0)) + 
--		sum(ISNULL(team_2_normal_time_score,0)) + sum(ISNULL(team_2_extra_time_score,0)))) 
--			/ CONVERT(decimal(8,2),COUNT(game_code)),0) AS average_goals_per_game
--	FROM wc_tournament t
--	JOIN wc_game g ON t.tournament_code = g.tournament_code
--	GROUP BY t.tournament_code, t.name
--	ORDER BY t.tournament_code DESC

--/*
--	SELECT gs.tournament_code, CONVERT(decimal(8,2),sum(gs.goals)) ,
--	CONVERT(decimal(8,2),COUNT(g.game_code))
--	FROM vWC_GameScoreSummary gs
--	JOIN wc_game g ON gs.tournament_code = g.tournament_code
--	GROUP BY gs.tournament_code
--	ORDER BY gs.tournament_code DESC

--	select * from vWC_GameScoreSummary
--	where tournament_code = 19
--and game_code = 1*/

--END