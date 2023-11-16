﻿--/*
--CREATE TABLE [dbo].[wc_tournament_team_final_placing]
--(
--	tournament_code INT, 
--	team_code INT, 
--	final_placing INT
--)
--*/

--CREATE PROCEDURE [dbo].[spWC_SetFinalTournamentPlacing]
--	@tournament_code INT
--AS
--	DELETE FROM wc_tournament_team_final_placing
--	WHERE tournament_code = @tournament_code

--	CREATE TABLE #tmp_final_placing (placing INT, team_code INT)

--	--1st Place
--	INSERT INTO #tmp_final_placing 
--	SELECT 1, CASE WHEN gss1.goals_with_penalties > gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
--	FROM wc_game g1
--	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
--	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
--	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
--	WHERE g1.tournament_code = @tournament_code
--	and g1.round_code = 'FF'
	 
--	--2nd Place
--	INSERT INTO #tmp_final_placing 
--	SELECT 2, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
--	FROM wc_game g1
--	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
--	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
--	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
--	WHERE g1.tournament_code = @tournament_code
--	and g1.round_code = 'FF'

--	--3rd Place
--	INSERT INTO #tmp_final_placing 
--	SELECT 3, CASE WHEN gss1.goals_with_penalties > gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
--	FROM wc_game g1
--	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
--	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
--	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
--	WHERE g1.tournament_code = @tournament_code
--	and g1.round_code = '3P'

--	--4th Place
--	INSERT INTO #tmp_final_placing 
--	SELECT 4, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
--	FROM wc_game g1
--	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
--	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
--	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
--	WHERE g1.tournament_code = @tournament_code
--	and g1.round_code = '3P'

--	--5th - 8th Place
--	INSERT INTO #tmp_final_placing 
--	SELECT 5, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
--	FROM wc_game g1
--	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
--	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
--	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
--	WHERE g1.tournament_code = @tournament_code
--	and g1.round_code = 'QF'

--	--9th - 16th Place
--	INSERT INTO #tmp_final_placing 
--	SELECT 9, CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
--	FROM wc_game g1
--	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
--	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
--	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
--	WHERE g1.tournament_code = @tournament_code
--	and g1.round_code = '16'

--	--17th - 32nd Place: Part 1 (Team 1)
--	INSERT INTO #tmp_final_placing 
--	SELECT DISTINCT 17, g1.team_1_code
--	FROM wc_game g1
--	WHERE g1.tournament_code = @tournament_code
--	and g1.team_1_code not in (SELECT team_code FROM #tmp_final_placing)

--	--17th - 32nd Place: Part 2 (Team 2)
--	INSERT INTO #tmp_final_placing 
--	SELECT DISTINCT 17, g2.team_2_code
--	FROM wc_game g2
--	WHERE g2.tournament_code = @tournament_code
--	and g2.team_2_code not in (SELECT team_code FROM #tmp_final_placing)

--	SELECT fp.placing, t.team_name
--	FROM #tmp_final_placing fp
--	JOIN wc_team t ON fp.team_code = t.team_code
--	ORDER BY fp.placing, t.team_name

--	INSERT INTO wc_tournament_team_final_placing
--	SELECT @tournament_code, team_code, placing
--	FROM #tmp_final_placing

--	DROP TABLE #tmp_final_placing