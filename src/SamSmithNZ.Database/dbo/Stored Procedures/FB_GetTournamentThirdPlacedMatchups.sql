CREATE PROCEDURE [dbo].[FB_GetTournamentThirdPlacedMatchups]
	@TournamentCode INT,
	@GroupsInTop8 VARCHAR(50)
AS
BEGIN
	-- Parse comma-delimited group list into a table variable
	DECLARE @GroupTable TABLE (GroupLetter CHAR(1));
	DECLARE @Pos INT;
	DECLARE @NextPos INT;
	DECLARE @Value VARCHAR(10);

	-- Add comma at end to make parsing easier
	SET @GroupsInTop8 = @GroupsInTop8 + ',';
	SET @Pos = 1;
	SET @NextPos = CHARINDEX(',', @GroupsInTop8, @Pos);

	-- Manual string split loop
	WHILE @NextPos > 0
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@GroupsInTop8, @Pos, @NextPos - @Pos)));

		IF LEN(@Value) > 0
		BEGIN
			INSERT INTO @GroupTable (GroupLetter) VALUES (@Value);
		END

		SET @Pos = @NextPos + 1;
		SET @NextPos = CHARINDEX(',', @GroupsInTop8, @Pos);
	END

	-- Find the combination_code where all 8 third-place groups match the provided list
	DECLARE @CombinationCode INT
	SELECT @CombinationCode = m.combination_code
	FROM wc_tournament_third_place_matchups m
	INNER JOIN @GroupTable g ON m.third_place_group = g.GroupLetter
	WHERE m.tournament_code = @TournamentCode
	GROUP BY m.combination_code
	HAVING COUNT(DISTINCT m.third_place_group) = 8;

	SELECT tournament_code AS TournamentCode, combination_code AS CombinationCode, 
		winner_match_code AS WinnerMatchCode, third_place_group AS ThirdPlaceGroup
	FROM wc_tournament_third_place_matchups m
	WHERE m.tournament_code = @TournamentCode
	AND m.combination_code = @CombinationCode 
END
