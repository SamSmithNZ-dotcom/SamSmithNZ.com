CREATE PROCEDURE [dbo].[FB_GetTournamentTeamsPlacing]
	@TournamentCode INT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tmp_final_placing (SortOrder int, FinalPlacing VARCHAR(50), TeamCode INT)
	DECLARE @ActiveTeams INT
	SELECT @ActiveTeams = COUNT(*)
	FROM wc_tournament_team_entry
	WHERE tournament_code = @TournamentCode
	AND is_active = 1

	IF (@TournamentCode <= 19)
	BEGIN
		INSERT INTO #tmp_final_placing
		SELECT final_placing, CONVERT(VARCHAR(50), final_placing), team_code 
		FROM wc_tournament_team_final_placing
		WHERE tournament_code = @TournamentCode
	END
	ELSE
	BEGIN
		--1st Place
		INSERT INTO #tmp_final_placing 
		SELECT TOP 1 1, '1st', --We need top as sometimes the final is a replay
			CASE WHEN g.team_1_normal_time_score IS NULL THEN NULL ELSE CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'FF'
		ORDER BY g.game_time DESC
 
		--2nd Place
		INSERT INTO #tmp_final_placing 
		SELECT TOP 1 2, '2nd', --We need top as sometimes the final is a replay
			CASE WHEN g.team_1_normal_time_score IS NULL THEN NULL ELSE CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'FF'
		ORDER BY g.game_time DESC

		--Final
		INSERT INTO #tmp_final_placing 
		SELECT 4, 'Final pending', 
			g.team_1_code
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'FF'
		AND g.team_1_normal_time_score IS NULL
		UNION
		SELECT 4, 'Final pending', 
			g.team_2_code
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'FF'
		AND g.team_2_normal_time_score IS NULL

		--3rd Place
		INSERT INTO #tmp_final_placing 
		SELECT 3, '3rd', 
			CASE WHEN g.team_1_normal_time_score IS NULL THEN NULL ELSE CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '3P'

		--4th Place
		INSERT INTO #tmp_final_placing 
		SELECT 4, '4th', 
			CASE WHEN g.team_1_normal_time_score IS NULL THEN NULL ELSE CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE g.team_2_code END END
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '3P'

		--3rd-4th Place
		INSERT INTO #tmp_final_placing 
		SELECT 4, '3rd place game pending', 
			g.team_1_code
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '3P'
		AND g.team_1_normal_time_score IS NULL
		UNION
		SELECT 4, '3rd place game pending', 
			g.team_2_code
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '3P'
		AND g.team_2_normal_time_score IS NULL
		UNION
		SELECT 4, 'Knocked out in semi finals', 
			g.team_1_code
		FROM wc_game g
		JOIN vWC_GameScoreSummary ss ON g.tournament_code = ss.tournament_code AND g.game_code = ss.game_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'SF'
		AND ss.team_1_goals < ss.team_2_goals 
		AND NOT EXISTS (SELECT 1 FROM wc_game g2 WHERE g2.tournament_code = g.tournament_code AND g2.round_code = '3P')
		UNION
		SELECT 4, 'Knocked out in semi finals', 
			g.team_2_code
		FROM wc_game g
		JOIN vWC_GameScoreSummary ss ON g.tournament_code = ss.tournament_code AND g.game_code = ss.game_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'SF'
		AND ss.team_1_goals > ss.team_2_goals 
		AND NOT EXISTS (SELECT 1 FROM wc_game g2 WHERE g2.tournament_code = g.tournament_code AND g2.round_code = '3P')

		--SELECT NOT EXISTS (select * from wc_game g2 WHERE g2.round_code = '3P' and tournament_code = 19)

		--Top 4
		INSERT INTO #tmp_final_placing 
		SELECT 4, 
			CASE WHEN te.is_active = 1 THEN 'Active in SF' END,
			g.team_1_code
			--CASE WHEN g.team_1_normal_time_score IS NULL THEN g.team_1_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE 0 END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'SF'
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		INSERT INTO #tmp_final_placing 
		SELECT 4, 
			CASE WHEN te.is_active = 1 THEN 'Active in SF' END,
			g.team_2_code
			--CASE WHEN g.team_2_normal_time_score IS NULL THEN g.team_2_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN 0 ELSE g.team_2_code END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'SF'
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		
		--5th - 8th Place
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 5 ELSE 6 END, 
			CASE WHEN te.is_active = 1 THEN 'Active in QF' ELSE 'Knocked out in quarter finals' END,
			g.team_1_code
			--CASE WHEN g.team_1_normal_time_score IS NULL THEN g.team_1_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE 0 END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'QF'
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE not TeamCode IS NULL)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 5 ELSE 6 END, 
			CASE WHEN te.is_active = 1 THEN 'Active in QF' ELSE 'Knocked out in quarter finals' END,
			g.team_2_code
			--CASE WHEN g.team_2_normal_time_score IS NULL THEN g.team_2_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN 0 ELSE g.team_2_code END
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = 'QF'
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)

		--9th - 16th Place
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 9 ELSE 10 END AS SortOrder,  
			CASE WHEN te.is_active = 1 THEN 'Active in top 16' ELSE 'Knocked out in top 16' END AS info,
			g.team_1_code
			--CASE WHEN g.team_1_normal_time_score IS NULL THEN g.team_1_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) > g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN g.team_1_code ELSE 0 END 
			--END AS TeamCode
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '16'
		AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		INSERT INTO #tmp_final_placing 
		SELECT CASE WHEN te.is_active = 1 THEN 9 ELSE 10 END, 
			CASE WHEN te.is_active = 1 THEN 'Active in top 16' ELSE 'Knocked out in top 16' END,
			g.team_2_code
			--CASE WHEN g.team_2_normal_time_score IS NULL THEN g.team_2_code ELSE 
			--	CASE WHEN g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0) + ISNULL(g.team_1_penalties_score,0) < g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0) + ISNULL(g.team_2_penalties_score,0) THEN 0 ELSE g.team_2_code END 
			--END
		FROM wc_game g
		JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
		WHERE g.tournament_code = @TournamentCode
		AND g.round_code = '16'
		--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)

		IF EXISTS (SELECT 1 FROM wc_game g WHERE g.tournament_code = @TournamentCode AND g.round_code = '32')
		BEGIN
			--17th - 32nd Place
			INSERT INTO #tmp_final_placing 
			SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 17 ELSE 18 END,  
				CASE WHEN te.is_active = 1 THEN 'Active in top 32' ELSE 'Knocked out in top 32' END, 
				g.team_1_code
			FROM wc_game g
			JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
			WHERE g.tournament_code = @TournamentCode
			AND g.round_code = '32'
			AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			INSERT INTO #tmp_final_placing 
			SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 17 ELSE 18 END, 
				CASE WHEN te.is_active = 1 THEN 'Active in top 32' ELSE 'Knocked out in top 32' END, 
				g.team_2_code
			FROM wc_game g
			JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
			WHERE g.tournament_code = @TournamentCode
			AND g.round_code = '32'
			--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)

			--33rd - 48th Place
			INSERT INTO #tmp_final_placing 
			SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 33 ELSE 34 END,  
				CASE WHEN te.is_active = 1 THEN 'Active in group' ELSE 'Knocked out in group stage' END, 
				g.team_1_code
			FROM wc_game g
			JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
			WHERE g.tournament_code = @TournamentCode
			AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			INSERT INTO #tmp_final_placing 
			SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 33 ELSE 34 END, 
				CASE WHEN te.is_active = 1 THEN 'Active in group' ELSE 'Knocked out in group stage' END, 
				g.team_2_code
			FROM wc_game g
			JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
			WHERE g.tournament_code = @TournamentCode
			--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		END
		ELSE
		BEGIN
			--17th - 32nd Place
			INSERT INTO #tmp_final_placing 
			SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 33 ELSE 34 END,  
				CASE WHEN te.is_active = 1 THEN 'Active in group' ELSE 'Knocked out in group stage' END, 
				g.team_1_code
			FROM wc_game g
			JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_1_code = te.team_code
			WHERE g.tournament_code = @TournamentCode
			AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			--AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			INSERT INTO #tmp_final_placing 
			SELECT DISTINCT CASE WHEN te.is_active = 1 THEN 33 ELSE 34 END, 
				CASE WHEN te.is_active = 1 THEN 'Active in group' ELSE 'Knocked out in group stage' END, 
				g.team_2_code
			FROM wc_game g
			JOIN wc_tournament_team_entry te ON g.tournament_code = te.tournament_code AND g.team_2_code = te.team_code
			WHERE g.tournament_code = @TournamentCode
			--AND g.team_1_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
			AND g.team_2_code NOT IN (SELECT TeamCode FROM #tmp_final_placing WHERE NOT TeamCode IS NULL)
		END
	END

	CREATE TABLE #TeamRecord(TeamCode INT, GF INT, GA INT, GD INT)--, PKs INT, PKsMissed INT)
	INSERT INTO #TeamRecord
	SELECT g.team_1_code, 
		SUM(g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0)),
		SUM(g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0)), 
		NULL  
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	GROUP BY g.team_1_code
	UNION
	SELECT g.team_2_code, 
		SUM(g.team_2_normal_time_score + ISNULL(g.team_2_extra_time_score,0)), 
		SUM(g.team_1_normal_time_score + ISNULL(g.team_1_extra_time_score,0)),
		NULL  
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	GROUP BY g.team_2_code

	SELECT DISTINCT fp.FinalPlacing AS Placing, 
		t.team_code AS TeamCode, 
		t.team_name AS TeamName, 
		t.flag_name AS FlagName, 
		r.region_code AS RegionCode, 
		r.region_abbrev AS RegionName, 
		ISNULL(te.fifa_ranking,0) AS FifaRanking, 
		ISNULL(te.starting_elo_rating,0) AS StartingEloRating,
		te.coach_name AS CoachName, 
		ISNULL(ct.flag_name,'') AS CoachNationalityFlagName,
		te.current_elo_rating AS CurrentEloRating,
		CASE WHEN @ActiveTeams > 0 THEN te.is_active ELSE 0 END AS IsActive,
		fp.SortOrder AS SortOrder,
		ISNULL(cw.chance_to_win,0) * CONVERT(DECIMAL(8,4), 100) AS ChanceToWin,
		SUM(tr.GF) AS GF,
		SUM(tr.GA) AS GA,		
		SUM(tr.GF) - SUM(tr.GA) AS GD
	FROM #tmp_final_placing fp
	JOIN wc_team t ON fp.TeamCode = t.team_code
	JOIN wc_tournament_team_entry te ON te.team_code = t.team_code
	JOIN wc_region r ON t.region_code = r.region_code
	LEFT JOIN wc_team ct ON ct.team_name = te.coach_nationality
	LEFT JOIN wc_tournament_team_chance_to_win cw ON te.tournament_code = cw.tournament_code AND te.team_code = cw.team_code
	JOIN #TeamRecord tr ON tr.TeamCode = fp.TeamCode
	WHERE te.tournament_code = @TournamentCode
	--AND fp.TeamCode = 29
	GROUP BY fp.FinalPlacing,
		t.team_code, 
		t.team_name, 
		t.flag_name, 
		r.region_code, 
		r.region_abbrev, 
		ISNULL(te.fifa_ranking,0), 
		ISNULL(te.starting_elo_rating,0),
		te.coach_name, 
		ISNULL(ct.flag_name,''),
		te.current_elo_rating,
		CASE WHEN @ActiveTeams > 0 THEN te.is_active ELSE 0 END,
		fp.SortOrder,
		ISNULL(cw.chance_to_win,0) * CONVERT(DECIMAL(8,4), 100)
	ORDER BY ISNULL(cw.chance_to_win,0) * CONVERT(DECIMAL(8,4), 100) DESC,
		CASE WHEN @ActiveTeams > 0 THEN te.is_active ELSE 0 END DESC, 
		fp.SortOrder,
		te.current_elo_rating DESC, 
		t.team_name

	DROP TABLE #tmp_final_placing
	DROP TABLE #TeamRecord
END
GO