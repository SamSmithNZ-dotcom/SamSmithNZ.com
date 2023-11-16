﻿--CREATE PROCEDURE [dbo].[spWC_GetOddsSummary]	
--	@odds_limit DECIMAL(18, 4) = NULL,
--	@show_alive_teams bit = NULL,
--	@show_eliminated_teams bit = NULL,
--	@tournament_code INT = NULL,
--	@odds_date datetime = NULL
--AS
--	IF (@tournament_code is NULL)
--	BEGIN
--		SELECT @tournament_code = 20
--	END

--	CREATE TABLE #tmp_team (team_name VARCHAR(200), odds_limit DECIMAL(18, 4))

--	IF (@odds_limit is NULL)
--	BEGIN
--		INSERT INTO #tmp_team
--		SELECT distinct team_name, 0 
--		FROM wc_odds
--		WHERE tournament_code = @tournament_code
--	END
--	ELSE
--	BEGIN
--		INSERT INTO #tmp_team
--		SELECT distinct team_name, MAX(odds_probability)
--		FROM wc_odds 
--		WHERE tournament_code = @tournament_code
--		GROUP BY team_name
--		HAVING MAX(odds_probability) <= @odds_limit
--	END

--	--Show teams that are still in the world cup
--	IF (@show_alive_teams is NULL)
--	BEGIN
--		SELECT @show_alive_teams = 1
--	END
--	--Show eliminated teams
--	IF (@show_eliminated_teams is NULL)
--	BEGIN
--		SELECT @show_eliminated_teams = 1
--	END

--	IF (@show_alive_teams = 0)
--	BEGIN
--		DELETE t 
--		FROM #tmp_team t
--		WHERE t.team_name in (SELECT o.team_name 
--								FROM wc_odds o 		
--								WHERE tournament_code = @tournament_code						
--								GROUP BY o.team_name
--								HAVING MIN(o.odds_probability) > 0)		
--	END
	
--	IF (@show_eliminated_teams = 0)
--	BEGIN
--		DELETE t 
--		FROM #tmp_team t
--		WHERE t.team_name in (SELECT o.team_name 
--								FROM wc_odds o 	
--								WHERE tournament_code = @tournament_code							
--								GROUP BY o.team_name
--								HAVING MIN(o.odds_probability) = 0)		 
--	END


--	SELECT o.team_name, 
--    o.odds_probability,
--    o.odds_date,
--    o.odds_mean,
--    o.odds_max,
--    o.odds_min,
--    o.odds_stdDev,
--    o.odds_sample_size,
--	o.tournament_code
--	FROM wc_odds o
--	JOIN #tmp_team t ON o.team_name = t.team_name
--	WHERE tournament_code = @tournament_code
--	and (@odds_date is NULL or o.odds_date = @odds_date)
--	--and o.team_name = 'croatia' or o.team_name = 'usa'
--	ORDER BY o.odds_date, o.team_name