﻿CREATE PROCEDURE [dbo].[FB_GetTournamentTeamsPlacing]
	@TournamentCode INT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tmp_final_placing (SortOrder int, FinalPlacing VARCHAR(50), TeamCode INT)

	--1st Place
	INSERT INTO #tmp_final_placing 
	SELECT 1, '1st', CASE WHEN gss1.goals_with_penalties > gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = 'FF'
 
	--2nd Place
	INSERT INTO #tmp_final_placing 
	SELECT 2, '2nd', CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = 'FF'

	--3rd Place
	INSERT INTO #tmp_final_placing 
	SELECT 3, '3rd', CASE WHEN gss1.goals_with_penalties > gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = '3P'

	--4th Place
	INSERT INTO #tmp_final_placing 
	SELECT 4, '4th', CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = '3P'

	--5th - 8th Place
	INSERT INTO #tmp_final_placing 
	SELECT 5, '5th - 8th', CASE WHEN gss1.goals_with_penalties IS NULL THEN g1.team_1_code ELSE CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = 'QF'
	INSERT INTO #tmp_final_placing 
	SELECT 5, '5th - 8th', CASE WHEN gss2.goals_with_penalties IS NULL THEN g1.team_2_code ELSE CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = 'QF'

	--9th - 16th Place
	INSERT INTO #tmp_final_placing 
	SELECT 9, '9th - 16th', CASE WHEN gss1.goals_with_penalties IS NULL THEN g1.team_1_code ELSE CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = '16'
	INSERT INTO #tmp_final_placing 
	SELECT 9, '9th - 16th', CASE WHEN gss2.goals_with_penalties IS NULL THEN g1.team_2_code ELSE CASE WHEN gss1.goals_with_penalties < gss2.goals_with_penalties THEN g1.team_1_code ELSE g2.team_2_code END END
	FROM wc_game g1
	JOIN vWC_GameScoreSummary gss1 ON g1.tournament_code = gss1.tournament_code and g1.game_code = gss1.game_code and gss1.is_team_1 = 1
	JOIN wc_game g2 ON g1.tournament_code = g2.tournament_code and g1.game_code = g2.game_code
	JOIN vWC_GameScoreSummary gss2 ON g2.tournament_code = gss2.tournament_code and g2.game_code = gss2.game_code and gss2.is_team_1 = 0
	WHERE g1.tournament_code = @TournamentCode
	and g1.round_code = '16'

	--17th - 32nd Place: Part 1 (Team 1)
	INSERT INTO #tmp_final_placing 
	SELECT DISTINCT 17, '17th - 32nd', g1.team_1_code
	FROM wc_game g1
	WHERE g1.tournament_code = @TournamentCode
	and g1.team_1_code not in (SELECT TeamCode FROM #tmp_final_placing)

	--17th - 32nd Place: Part 2 (Team 2)
	INSERT INTO #tmp_final_placing 
	SELECT DISTINCT 17, '17th - 32nd', g2.team_2_code
	FROM wc_game g2
	WHERE g2.tournament_code = @TournamentCode
	and g2.team_2_code not in (SELECT TeamCode FROM #tmp_final_placing)

	SELECT DISTINCT fp.FinalPlacing AS Placing, 
		t.team_code AS TeamCode, 
		t.team_name AS TeamName, 
		t.flag_name AS FlagName, 
		r.region_code AS RegionCode, 
		r.region_abbrev AS RegionName, 
		ISNULL(te.fifa_ranking,0) AS FifaRanking, 
		te.coach_name AS CoachName, 
		ISNULL(ct.flag_name,'') AS CoachNationalityFlagName,
		e.elo_rating AS ELORating,
		fp.SortOrder
	FROM #tmp_final_placing fp
	JOIN wc_team t ON fp.TeamCode = t.team_code
	JOIN wc_tournament_team_entry te ON te.team_code = t.team_code
	JOIN wc_region r ON t.region_code = r.region_code
	LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
	JOIN wc_tournament_team_elo_rating e ON te.tournament_code = e.tournament_code AND te.team_code = e.team_code
	WHERE te.tournament_code = @TournamentCode
	ORDER BY fp.SortOrder, 
		e.elo_rating DESC, t.team_name

	DROP TABLE #tmp_final_placing
END
GO