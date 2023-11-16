﻿CREATE PROCEDURE [dbo].[spWC_GetPlayerListForGame]
	@game_code INT
AS
--SELECT 0 AS player_code, ' (select player)' AS player_name, 1 AS number, '' AS position, '' AS team_name
--UNION
SELECT p.player_code, 
	--p.player_name,
					SUBSTRING(p.player_name, CHARINDEX(' ', p.player_name) + 1, LEN(p.player_name)) + ', ' + SUBSTRING(p.player_name, 1, CASE WHEN (CHARINDEX(' ', p.player_name) - 1) < 0 THEN 0 ELSE CHARINDEX(' ', p.player_name) -1 END) + ' (' + t.team_name + ')' AS player_name,       
	--p.player_name + ' (' + t.team_name + ')', 
	p.number, p.position, t.team_name
FROM wc_player p 
JOIN wc_team t ON p.team_code = t.team_code
JOIN wc_game g ON p.tournament_code = g.tournament_code and (p.team_code = g.team_1_code or p.team_code = g.team_2_code)
WHERE g.game_code = @game_code
ORDER BY team_name, SUBSTRING(p.player_name, CHARINDEX(' ', p.player_name) + 1, LEN(p.player_name)) + ', ' + SUBSTRING(p.player_name, 1, CASE WHEN (CHARINDEX(' ', p.player_name) - 1) < 0 THEN 0 ELSE CHARINDEX(' ', p.player_name) -1 END) + ' (' + t.team_name + ')'