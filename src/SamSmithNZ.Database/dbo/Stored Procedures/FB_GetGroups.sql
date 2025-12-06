CREATE PROCEDURE [dbo].[FB_GetGroups]
	@TournamentCode INT,
	@RoundNumber INT,
	@RoundCode VARCHAR(10) = NULL
AS
BEGIN

	SELECT t.team_name AS TeamName, 
		CONVERT(VARCHAR(100),t.flag_name) AS TeamFlagName, 
		gs.draws AS Draws, 
		gs.goal_difference AS GoalDifference, 
		gs.goals_against AS GoalsAgainst, 
		gs.goals_for AS GoalsFor, 
		gs.group_ranking AS GroupRanking, 
		gs.has_qualified_for_next_round AS HasQualifiedForNextRound, 
		gs.losses AS Losses,
		CASE WHEN gs.played < 0 THEN 0 ELSE gs.played END AS Played, 
		gs.points AS Points, 
		gs.round_code AS BaseRoundCode,
		gs.round_code AS RoundCode, 
		gs.round_number AS RoundNumber, 
		gs.team_code AS TeamCode, 
		gs.tournament_code AS TournamentCode, 
		gs.wins AS Wins,
		e.current_elo_rating AS ELORating,
		CASE WHEN gs.played < 0 THEN 1 ELSE 0 END AS TeamWithdrew
	FROM wc_group_stage gs
	JOIN wc_team t ON gs.team_code = t.team_code
	LEFT JOIN wc_tournament_team_entry e ON gs.tournament_code = e.tournament_code AND gs.team_code = e.team_code
	WHERE gs.tournament_code = @TournamentCode
	AND gs.round_number = @RoundNumber
	AND (gs.round_code = @RoundCode OR @RoundCode IS NULL)

	UNION
	SELECT t.team_name AS TeamName, 
		CONVERT(VARCHAR(100),t.flag_name) AS TeamFlagName, 
		gs.draws AS Draws, 
		gs.goal_difference AS GoalDifference, 
		gs.goals_against AS GoalsAgainst, 
		gs.goals_for AS GoalsFor, 
		gs.group_ranking AS GroupRanking, 
		gs.has_qualified_for_next_round AS HasQualifiedForNextRound, 
		gs.losses AS Losses,
		CASE WHEN gs.played < 0 THEN 0 ELSE gs.played END AS Played, 
		gs.points AS Points, 
		'3rd' AS BaseRoundCode, 
		gs.round_code AS RoundCode, 
		gs.round_number AS RoundNumber, 
		gs.team_code AS TeamCode, 
		gs.tournament_code AS TournamentCode, 
		gs.wins AS Wins,
		e.current_elo_rating AS ELORating,
		CASE WHEN gs.played < 0 THEN 1 ELSE 0 END AS TeamWithdrew
	FROM wc_group_stage_third_placed_teams gs
	JOIN wc_team t ON gs.team_code = t.team_code
	JOIN wc_tournament_team_entry e ON gs.tournament_code = e.tournament_code AND gs.team_code = e.team_code
	WHERE gs.tournament_code = @TournamentCode
	AND gs.round_number = @RoundNumber
	AND (gs.round_code = @RoundCode OR @RoundCode IS NULL)

	ORDER BY group_ranking
END