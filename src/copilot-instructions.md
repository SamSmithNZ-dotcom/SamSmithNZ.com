# Copilot Instructions for SamSmithNZ.com

Project type
- ASP.NET Core Razor Pages (prioritize Razor Pages patterns over Blazor or MVC)
- .NET target: .NET 10
- C# language version: 14.0

General coding rules
- Do not use `var`. Always use explicit types for local variables (e.g., `int count = 0;`, `string name = "";`).
- Follow existing file and project organization. Prefer minimal diffs and small, focused changes.
- Keep answers and code changes short and impersonal.
- Avoid adding comments unless the file already uses them for context or it’s necessary for complex changes.
- Do not introduce new libraries or update package versions unless strictly required.
- Preserve existing coding style (naming, spacing, brace style, and region usage) within each file.
- Avoid heavy formatting in responses. Use backticks around `file`, `directory`, `function`, and `class` names when needed.

Razor Pages guidelines
- Favor page handlers and `PageModel` patterns (`OnGet`, `OnPost`, etc.) and dependency injection.
- Use tag helpers and partials for UI reuse. Keep views strongly typed.
- Keep view logic simple. Move complex logic into the `PageModel` or service layer.
- When adding new views/partials, place them under `SamSmithNZ.Web/Views` following existing conventions.

Database and SQL
- For scripts targeting `dbo.wc_game` and related tables:
  - Use `(SELECT MAX(game_code)+1 FROM dbo.wc_game)` when inserting new rows.
  - Use correct `round_code` values: `32`, `16`, `QF`, `SF`, `3P`, `FF`.
  - Keep `team_1_code` and `team_2_code` as `0` when teams are unknown.
  - Align `game_time` and `location` to the authoritative schedule provided.
- Avoid transactions unless explicitly requested. Insert rows individually when asked.

World Cup UI specifics
- `Playoffs.cshtml` must remain backward compatible with older tournaments.
- Render Round of 32 via a dedicated partial when `Model.Games` contains `round_code == "32"`.
- Use `PlayoffSectionViewModel` for partials and filter by `round_code`.

Testing and validation
- Build locally after changes and fix compile errors before committing.
- Respect existing unit and functional test patterns where present.

Security and compliance
- Follow Microsoft content policies.
- Avoid content that violates copyrights.
- Provide reminders that you are an AI programming assistant for non-software questions.

Assistant identity
- When asked for your name, respond with "GitHub Copilot".
