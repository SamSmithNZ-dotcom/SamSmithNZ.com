-- Populate wc_tournament_third_place_matchups from FIFA ANNEXE C data
-- Source: 3rdPlacePlayoffs.csv containing 495 combinations
-- Format: Each row has combination number + 8 match assignments (1A, 1B, 1D, 1E, 1G, 1I, 1K, 1L)
-- Cell format: "3X" where X is group letter (A-L)

-- Tournament code for FIFA 2026 World Cup
DECLARE @TournamentCode INT = 23;

-- Clear existing data for this tournament
DELETE FROM [dbo].[wc_tournament_third_place_matchups] WHERE tournament_code = @TournamentCode;

-- Insert all 495 combinations from FIFA ANNEXE C
-- Each combination generates 8 rows (one per Round of 32 match)
INSERT INTO [dbo].[wc_tournament_third_place_matchups] 
    (tournament_code, combination_code, winner_match_code, third_place_group)
VALUES
    -- Combination 1: 3E, 3J, 3I, 3F, 3H, 3G, 3L, 3K
    (@TournamentCode, 1, '1A', 'E'), (@TournamentCode, 1, '1B', 'J'), (@TournamentCode, 1, '1D', 'I'), (@TournamentCode, 1, '1E', 'F'),
    (@TournamentCode, 1, '1G', 'H'), (@TournamentCode, 1, '1I', 'G'), (@TournamentCode, 1, '1K', 'L'), (@TournamentCode, 1, '1L', 'K'),
    
    -- Combination 2: 3H, 3G, 3I, 3D, 3J, 3F, 3L, 3K
    (@TournamentCode, 2, '1A', 'H'), (@TournamentCode, 2, '1B', 'G'), (@TournamentCode, 2, '1D', 'I'), (@TournamentCode, 2, '1E', 'D'),
    (@TournamentCode, 2, '1G', 'J'), (@TournamentCode, 2, '1I', 'F'), (@TournamentCode, 2, '1K', 'L'), (@TournamentCode, 2, '1L', 'K'),
    
    -- Combination 3: 3E, 3J, 3I, 3D, 3H, 3G, 3L, 3K
    (@TournamentCode, 3, '1A', 'E'), (@TournamentCode, 3, '1B', 'J'), (@TournamentCode, 3, '1D', 'I'), (@TournamentCode, 3, '1E', 'D'),
    (@TournamentCode, 3, '1G', 'H'), (@TournamentCode, 3, '1I', 'G'), (@TournamentCode, 3, '1K', 'L'), (@TournamentCode, 3, '1L', 'K'),
    
    -- Combination 4: 3E, 3J, 3I, 3D, 3H, 3F, 3L, 3K
    (@TournamentCode, 4, '1A', 'E'), (@TournamentCode, 4, '1B', 'J'), (@TournamentCode, 4, '1D', 'I'), (@TournamentCode, 4, '1E', 'D'),
    (@TournamentCode, 4, '1G', 'H'), (@TournamentCode, 4, '1I', 'F'), (@TournamentCode, 4, '1K', 'L'), (@TournamentCode, 4, '1L', 'K'),
    
    -- Combination 5: 3E, 3G, 3I, 3D, 3J, 3F, 3L, 3K
    (@TournamentCode, 5, '1A', 'E'), (@TournamentCode, 5, '1B', 'G'), (@TournamentCode, 5, '1D', 'I'), (@TournamentCode, 5, '1E', 'D'),
    (@TournamentCode, 5, '1G', 'J'), (@TournamentCode, 5, '1I', 'F'), (@TournamentCode, 5, '1K', 'L'), (@TournamentCode, 5, '1L', 'K'),
    
    -- Combination 6: 3E, 3G, 3J, 3D, 3H, 3F, 3L, 3K
    (@TournamentCode, 6, '1A', 'E'), (@TournamentCode, 6, '1B', 'G'), (@TournamentCode, 6, '1D', 'J'), (@TournamentCode, 6, '1E', 'D'),
    (@TournamentCode, 6, '1G', 'H'), (@TournamentCode, 6, '1I', 'F'), (@TournamentCode, 6, '1K', 'L'), (@TournamentCode, 6, '1L', 'K'),
    
    -- Combination 7: 3E, 3G, 3I, 3D, 3H, 3F, 3L, 3K
    (@TournamentCode, 7, '1A', 'E'), (@TournamentCode, 7, '1B', 'G'), (@TournamentCode, 7, '1D', 'I'), (@TournamentCode, 7, '1E', 'D'),
    (@TournamentCode, 7, '1G', 'H'), (@TournamentCode, 7, '1I', 'F'), (@TournamentCode, 7, '1K', 'L'), (@TournamentCode, 7, '1L', 'K'),
    
    -- Combination 8: 3E, 3G, 3J, 3D, 3H, 3F, 3I, 3K
    (@TournamentCode, 8, '1A', 'E'), (@TournamentCode, 8, '1B', 'G'), (@TournamentCode, 8, '1D', 'J'), (@TournamentCode, 8, '1E', 'D'),
    (@TournamentCode, 8, '1G', 'H'), (@TournamentCode, 8, '1I', 'F'), (@TournamentCode, 8, '1K', 'I'), (@TournamentCode, 8, '1L', 'K'),
    
    -- Combination 9: 3E, 3G, 3J, 3D, 3H, 3F, 3I, 3K
    (@TournamentCode, 9, '1A', 'E'), (@TournamentCode, 9, '1B', 'G'), (@TournamentCode, 9, '1D', 'J'), (@TournamentCode, 9, '1E', 'D'),
    (@TournamentCode, 9, '1G', 'H'), (@TournamentCode, 9, '1I', 'F'), (@TournamentCode, 9, '1K', 'I'), (@TournamentCode, 9, '1L', 'K'),
    
    -- Combination 10: 3H, 3G, 3I, 3C, 3J, 3F, 3L, 3K
    (@TournamentCode, 10, '1A', 'H'), (@TournamentCode, 10, '1B', 'G'), (@TournamentCode, 10, '1D', 'I'), (@TournamentCode, 10, '1E', 'C'),
    (@TournamentCode, 10, '1G', 'J'), (@TournamentCode, 10, '1I', 'F'), (@TournamentCode, 10, '1K', 'L'), (@TournamentCode, 10, '1L', 'K');

-- NOTE: This script contains only first 10 combinations as a sample for testing.
-- For PRODUCTION use, run: Populate_ThirdPlaceMatchups_Full.sql
-- The full script contains all 495 combinations (3,960 INSERT statements).
-- To regenerate the full script, run: GenerateFullInsertScript.ps1

-- Verification query - show sample data
SELECT TOP 20
    combination_code,
    winner_match_code,
    third_place_group
FROM [dbo].[wc_tournament_third_place_matchups]
WHERE tournament_code = @TournamentCode
ORDER BY combination_code, winner_match_code;

-- Count total rows
DECLARE @RowCount INT;
SELECT @RowCount = COUNT(*) 
FROM [dbo].[wc_tournament_third_place_matchups] 
WHERE tournament_code = @TournamentCode;

PRINT 'Sample data inserted for first 10 combinations.';
PRINT 'Total rows in table: ' + CAST(@RowCount AS VARCHAR);
PRINT 'NOTE: Complete CSV import requires all 495 combinations (3,960 rows total)';