select * from [dbo].[wc_tournament_team_entry] where tournament_code = 22
and (team_code = 70 or team_code = 84)

update wc_tournament_team_entry
set is_active = 0
where tournament_code = 22
and team_code = 34

select * from wc_game 
where tournament_code = 22 
and game_number = 52

update [dbo].[wc_game]
set team_1_code = 3
where tournament_code = 22 
and game_number = 50

update [dbo].[wc_game]
set team_2_code = 43
where tournament_code = 22 
and game_number = 52

update g
set g.team_1_pregame_elo_rating = e.current_elo_rating
from wc_game g
join wc_tournament_team_entry e ON g.tournament_code = e.tournament_code and g.team_1_code = e.team_code
where g.team_1_pregame_elo_rating is null 
and g.tournament_code = 22

update g
set g.team_2_pregame_elo_rating = e.current_elo_rating
from wc_game g
join wc_tournament_team_entry e ON g.tournament_code = e.tournament_code and g.team_2_code = e.team_code
where g.team_2_pregame_elo_rating is null 
and g.tournament_code = 22

update wc_Game
set team_1_elo_rating = team_1_pregame_elo_rating
where tournament_code = 22 
and game_number >= 49
and team_1_elo_rating = 1000
and team_1_pregame_elo_rating > 1000

update wc_Game
set team_2_elo_rating = team_2_pregame_elo_rating
where tournament_code = 22 
and game_number >= 49
and team_2_elo_rating = 1000
and team_2_pregame_elo_rating > 1000