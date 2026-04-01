CREATE PROCEDURE [dbo].[FB_ResetTournamentToStart]
	@TournamentCode INT
AS
BEGIN
	--For group play, don't remove the team codes
	UPDATE g
	SET g.team_1_normal_time_score = null, g.team_2_normal_time_score = null,
	--g.team_1_code = null, g.team_2_code = null,
	g.team_1_elo_rating = null, g.team_2_elo_rating = null,
	g.team_1_pregame_elo_rating = null, g.team_2_pregame_elo_rating = null,
	g.team_1_postgame_elo_rating = null, g.team_2_postgame_elo_rating = null
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND (g.team_1_code >= 0 or g.team_2_code >= 0) 
	AND g.round_number = 1

	--For playoffs, remove the team codes
	UPDATE g
	SET g.team_1_normal_time_score = null, g.team_2_normal_time_score = null,
	g.team_1_code = 0, g.team_2_code = 0,
	g.team_1_elo_rating = null, g.team_2_elo_rating = null,
	g.team_1_pregame_elo_rating = null, g.team_2_pregame_elo_rating = null,
	g.team_1_postgame_elo_rating = null, g.team_2_postgame_elo_rating = null
	FROM wc_game g
	WHERE g.tournament_code = @TournamentCode
	AND (g.team_1_code > 0 or g.team_2_code > 0) 
	AND g.round_number > 1

	-- set all of the progress back to 0. 
	UPDATE g
	SET g.wins = 0, g.draws = 0, g.losses = 0,
	g.goals_for = 0, g.goals_against = 0, g.goal_difference = 0,
	g.played = 0, g.points = 0, 
	g.has_qualified_for_next_round = 0
	FROM wc_group_stage g
	WHERE g.tournament_code = @TournamentCode

	DELETE g FROM wc_goal g
	JOIN wc_game a ON g.game_code = a.game_code
	WHERE tournament_code = @TournamentCode

	DELETE FROM wc_group_stage_third_placed_teams
	WHERE tournament_code = @TournamentCode
END
GO
