-- Table to store FIFA 2026 World Cup third place team matchup assignments
-- Maps each of the 495 possible combinations to specific Round of 32 matches
CREATE TABLE [dbo].[wc_tournament_third_place_matchups]
(
    [tournament_code] INT NOT NULL,
    [combination_code] INT NOT NULL, -- 1-495
    [winner_match_code] VARCHAR(2) NOT NULL, -- '1A', '1B', '1D', '1E', '1G', '1I', '1K', '1L'
    [third_place_group] CHAR(1) NOT NULL, -- 'A' through 'L'
    PRIMARY KEY ([tournament_code], [combination_code], [winner_match_code]),
    CONSTRAINT FK_third_place_matchups_tournament 
        FOREIGN KEY ([tournament_code]) 
        REFERENCES [dbo].[wc_tournament]([tournament_code]),
    CONSTRAINT CK_third_place_group_matchups 
        CHECK ([third_place_group] IN ('A','B','C','D','E','F','G','H','I','J','K','L')),
    CONSTRAINT CK_winner_match_code
        CHECK ([winner_match_code] IN ('1A','1B','1D','1E','1G','1I','1K','1L'))
)
GO

-- Index for efficient lookup by combination
CREATE NONCLUSTERED INDEX IX_third_place_matchups_combination 
    ON [dbo].[wc_tournament_third_place_matchups]([tournament_code], [combination_code])
    INCLUDE ([winner_match_code], [third_place_group])
GO

-- Index for efficient lookup by match
CREATE NONCLUSTERED INDEX IX_third_place_matchups_match 
    ON [dbo].[wc_tournament_third_place_matchups]([tournament_code], [winner_match_code])
    INCLUDE ([combination_code], [third_place_group])
GO
