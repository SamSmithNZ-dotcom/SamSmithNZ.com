# Third Place Playoff Matchup Data - Implementation Guide

## Overview
This implementation stores the FIFA ANNEXE C official matchup assignments for the 2026 World Cup third-place playoff system. When 8 of the 12 third-place teams advance to the Round of 32, there are 495 possible combinations (C(12,8) = 495), and each combination has specific matchup assignments defined by FIFA.

## Database Schema

### Table: `wc_tournament_third_place_matchups`
Stores which third-place team plays in which Round of 32 match for each combination scenario.

**Columns:**
- `tournament_code` (INT) - Tournament identifier (23 = FIFA 2026 World Cup)
- `combination_code` (INT) - Combination number (1-495) from FIFA ANNEXE C
- `winner_match_code` (VARCHAR(2)) - Round of 32 match code ('1A', '1B', '1D', '1E', '1G', '1I', '1K', '1L')
- `third_place_group` (CHAR(1)) - Group letter (A-L) of the third-place team assigned to this match

**Primary Key:** `(tournament_code, combination_code, winner_match_code)`

**Total Rows:** 3,960 (495 combinations × 8 matches per combination)

## Files

### Database Scripts

1. **`dbo/Tables/wc_tournament_third_place_matchups.sql`**
   - Table definition with constraints and indexes
   - Foreign key to `wc_tournament` table
   - Check constraints for valid values

2. **`dbo/Scripts/Populate_ThirdPlaceMatchups.sql`**
   - Sample script with first 10 combinations
   - Useful for testing and development
   - Fast execution (80 rows)

3. **`dbo/Scripts/Populate_ThirdPlaceMatchups_Full.sql`**
   - **PRODUCTION SCRIPT** - Complete data for all 495 combinations
   - Auto-generated from CSV
   - File size: ~151 KB
   - Contains 3,960 INSERT statements
   - Includes verification queries

### Source Data

4. **`3rdPlacePlayoffs.csv`**
   - Official FIFA ANNEXE C data
   - 495 combinations × 8 match assignments
   - Format: `No., 1A, 1B, 1D, 1E, 1G, 1I, 1K, 1L`
   - Cell values: `3X` where X is group letter (e.g., "3E" = third place from Group E)

### Utility Scripts

5. **`GenerateFullInsertScript.ps1`**
   - PowerShell script to regenerate the full SQL script from CSV
   - Run if CSV data changes or needs regeneration
   - Usage: `.\GenerateFullInsertScript.ps1` (from SamSmithNZ.Database directory)

## Usage

### For Development/Testing
```sql
-- Run the sample script (10 combinations, 80 rows)
-- Execute: Populate_ThirdPlaceMatchups.sql
```

### For Production
```sql
-- Run the complete script (495 combinations, 3,960 rows)
-- Execute: Populate_ThirdPlaceMatchups_Full.sql
```

### Query Examples

**Find matchup for a specific combination:**
```sql
SELECT winner_match_code, third_place_group
FROM wc_tournament_third_place_matchups
WHERE tournament_code = 23 
  AND combination_code = 1
ORDER BY winner_match_code;
```

**Check which third-place team plays in match 1A for all combinations:**
```sql
SELECT combination_code, third_place_group
FROM wc_tournament_third_place_matchups
WHERE tournament_code = 23 
  AND winner_match_code = '1A'
ORDER BY combination_code;
```

**Count combinations where Group E's third place plays in match 1A:**
```sql
SELECT COUNT(*) as combination_count
FROM wc_tournament_third_place_matchups
WHERE tournament_code = 23 
  AND winner_match_code = '1A'
  AND third_place_group = 'E';
```

**Find which groups advance in a specific combination:**
```sql
-- Get all advancing third-place groups for combination 1
SELECT DISTINCT third_place_group
FROM wc_tournament_third_place_matchups
WHERE tournament_code = 23 
  AND combination_code = 1
ORDER BY third_place_group;
-- Returns the 8 groups (e.g., E, F, G, H, I, J, K, L)
```

**Find combination code for a specific set of advancing groups:**
```sql
-- Example: Groups A, B, D, E, G, I, K, L advance (sorted alphabetically)
-- Find which combination matches this exact set
SELECT combination_code
FROM wc_tournament_third_place_matchups
WHERE tournament_code = 23
GROUP BY combination_code
HAVING COUNT(DISTINCT third_place_group) = 8
  AND SUM(CASE WHEN third_place_group IN ('A','B','D','E','G','I','K','L') THEN 1 ELSE 0 END) = 8;
```

## How to Determine Active Combination

### Application Logic (To Be Implemented)
1. After group stage completes, determine which 8 groups have advancing third-place teams
2. Rank these 8 teams by:
   - Points
   - Goal difference
   - Goals scored
   - Fair play points
   - Drawing of lots
3. Assign sequence order (1-8) based on rankings
4. Find the matching combination_code by comparing the sorted list of advancing groups with each combination in `wc_tournament_third_place_matchups`
5. Use that combination_code to lookup matchup assignments (which third-place team plays in which Round of 32 match)

## Azure SQL Compatibility

All scripts are **Azure SQL Database compatible**:
- ? No subqueries in INSERT statements
- ? No BIT arithmetic without casting
- ? Variables used instead of subqueries in PRINT statements
- ? Simple SELECT queries for verification

## File Sizes
- CSV source data: ~38 KB
- Sample SQL script: ~3 KB
- Full SQL script: ~151 KB
- PowerShell generator: ~3 KB

## Next Steps

1. ? Create table structure
2. ? Generate population script from CSV
3. ? Validate Azure SQL compatibility
4. ? Run production script to populate data
5. ? Implement application logic to determine active combination
6. ? Create data access layer methods
7. ? Update UI to display correct matchups

## Related Tables

- `wc_tournament` - Tournament master data (foreign key reference)
- `wc_matches` - Match data including Round of 32 games
- `wc_group_stage_third_placed_teams` - Tracks which third-place teams advance

## Notes

- The combination_code values (1-495) match FIFA's official ANNEXE C numbering
- Match codes ('1A', '1B', etc.) represent the Round of 32 bracket positions
- The "winner" in winner_match_code refers to group winners who play in those matches
- Third-place teams fill the opposite positions in the bracket
