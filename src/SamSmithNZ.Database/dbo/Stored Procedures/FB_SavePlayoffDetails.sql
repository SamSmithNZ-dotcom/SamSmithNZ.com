CREATE PROCEDURE [dbo].[FB_SavePlayoffDetails]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10),
	@GameNumber INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @number_of_teams_in_group INT

	SELECT @number_of_teams_in_group = tfr.number_of_teams_in_group 
	FROM wc_tournament t
	JOIN wc_tournament_format tf ON t.format_code = tf.format_code
	JOIN wc_tournament_format_round tfr ON tfr.format_round_code = tf.round_2_format_code
	WHERE tournament_code = @TournamentCode

	DECLARE @game_count INT
	SELECT @game_count = COUNT(*) 
	FROM wc_game
	WHERE tournament_code = @TournamentCode
	and round_number = @RoundNumber
	and round_code = @RoundCode

	----If the number of games doesn't audit correctly, add some new games
	--IF (@number_of_teams_in_group / 2 <> @game_count)
	--BEGIN
	--	SELECT g.tournament_code,
	--		g.game_code,
	--		g.game_number,
	--		g.game_time,
	--		g.[location],
	--		g.round_code,
	--		g.round_number,
	--		g.team_1_code,
	--		g.team_1_extra_time_score,
	--		g.team_1_normal_time_score,
	--		g.team_1_penalties_score,
	--		g.team_2_code,
	--		g.team_2_extra_time_score,
	--		g.team_2_normal_time_score,
	--		g.team_2_penalties_score
	--	FROM wc_game g
	--	WHERE tournament_code = @TournamentCode
	--	AND round_number = @RoundNumber
	--	AND round_code = @RoundCode
	--END

	DECLARE @Team1Score INT
	DECLARE @Team2Score INT
	SELECT @Team1Score = SUM(g.team_1_normal_time_score) + SUM(ISNULL(g.team_1_extra_time_score,0)) + SUM(ISNULL(g.team_1_penalties_score,0)),
		@Team2Score = SUM(g.team_2_normal_time_score) + SUM(ISNULL(g.team_2_extra_time_score,0)) + SUM(ISNULL(g.team_2_penalties_score,0))
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND g.round_number = @RoundNumber
	and g.game_number = @GameNumber
	GROUP BY g.round_code, g.game_number
	--ORDER BY g.game_number

	DECLARE @WinningTeamCode INT
	DECLARE @LosingTeamCode INT
		
	IF (@Team1Score > @Team2Score)
	BEGIN
		SELECT @WinningTeamCode = g.team_1_code, @LosingTeamCode = g.team_2_code
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.game_number = @GameNumber

	END
	ELSE IF (@Team1Score < @Team2Score)
	BEGIN
		SELECT @WinningTeamCode = g.team_2_code, @LosingTeamCode = g.team_1_code
		FROM wc_game g
		WHERE g.tournament_code = @TournamentCode
		AND g.game_number = @GameNumber
	END
	ELSE 
	BEGIN
		SELECT @WinningTeamCode = 0, @LosingTeamCode = 0
	END

	--Keep the winning team in the competition
	UPDATE te
	SET te.is_active = 1
	FROM wc_tournament_team_entry te
	WHERE te.tournament_code = @TournamentCode
	AND te.team_code = @WinningTeamCode

	--If the 3rd place and final games are done, mark them as done.
	IF ((@RoundCode = '3P' OR @RoundCode = 'FF') AND @WinningTeamCode > 0 AND @LosingTeamCode > 0)
	BEGIN
		UPDATE te
		SET te.is_active = 0
		FROM wc_tournament_team_entry te
		WHERE te.tournament_code = @TournamentCode
		AND te.team_code = @WinningTeamCode
	END
	--Move the team to the next round
	ELSE IF ((@RoundCode = '32' OR @RoundCode = '16' OR @RoundCode = 'QF' OR @RoundCode = 'SF') AND @WinningTeamCode > 0 AND @LosingTeamCode > 0)
	BEGIN
		DECLARE @NewGameNumber INT
		SELECT @NewGameNumber = game_number
		FROM wc_tournament_format_playoff_setup
		WHERE tournament_code = @TournamentCode
		AND team_1_prereq = 'Winner of game ' + CONVERT(VARCHAR(10),@GameNumber) 

		IF (NOT @NewGameNumber IS NULL)
		BEGIN
			UPDATE g
			SET team_1_code = @WinningTeamCode--,
				--team_1_pregame_elo_rating = e.current_elo_rating, 
				--team_1_elo_rating = e.current_elo_rating
			FROM wc_game g
			--JOIN wc_tournament_team_entry e ON g.tournament_code = e.tournament_code and e.team_code = g.team_1_code	
			WHERE g.tournament_code = @TournamentCode
			AND g.game_number = @NewGameNumber
		END
		ELSE
		BEGIN
			SELECT @NewGameNumber = game_number
			FROM wc_tournament_format_playoff_setup
			WHERE tournament_code = @TournamentCode
			AND team_2_prereq = 'Winner of game ' + CONVERT(VARCHAR(10),@GameNumber)

			IF (NOT @NewGameNumber IS NULL)
			BEGIN
				UPDATE g
				SET team_2_code = @WinningTeamCode--,
					--team_2_pregame_elo_rating = e.current_elo_rating, 
					--team_2_elo_rating = e.current_elo_rating
				FROM wc_game g
				--JOIN wc_tournament_team_entry e ON g.tournament_code = e.tournament_code and e.team_code = g.team_2_code				
				WHERE g.tournament_code = @TournamentCode
				AND g.game_number = @NewGameNumber
			END
		END
	END

	--Remove the losing team from active teams and odds
	UPDATE te
	SET te.is_active = 0
	FROM wc_tournament_team_entry te
	WHERE te.tournament_code = @TournamentCode
	AND te.team_code = @LosingTeamCode

	UPDATE c
	SET c.chance_to_win = CONVERT(DECIMAL(6,4), 0)
	FROM wc_tournament_team_chance_to_win c
	WHERE c.tournament_code = @TournamentCode
	AND c.team_code = @LosingTeamCode

	----Update ELO Ratings
	--UPDATE g
	--SET team_1_pregame_elo_rating = e.current_elo_rating, 
	--	team_1_elo_rating = e.current_elo_rating
	--FROM wc_game g
	--JOIN wc_tournament_team_entry e ON g.tournament_code = e.tournament_code and e.team_code = g.team_1_code
	--WHERE g.tournament_code = @TournamentCode
	--AND g.round_number = @RoundNumber
	--AND g.team_1_code > 0
	--AND g.team_2_code > 0
	--AND team_1_pregame_elo_rating IS NULL

	--UPDATE g
	--SET team_2_pregame_elo_rating = e.current_elo_rating, 
	--	team_2_elo_rating = e.current_elo_rating
	--FROM wc_game g
	--JOIN wc_tournament_team_entry e ON g.tournament_code = e.tournament_code and e.team_code = g.team_1_code
	--WHERE g.tournament_code = @TournamentCode
	--AND g.round_number = @RoundNumber
	--AND g.team_1_code > 0
	--AND g.team_2_code > 0
	--AND team_2_pregame_elo_rating IS NULL

END

GO