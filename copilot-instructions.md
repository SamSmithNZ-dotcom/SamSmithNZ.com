# Coding Guidelines

## Type Declarations

- Always use explicit types instead of `var` when declaring variables
- This improves code readability and makes the intended type clear to other developers

### Examples:

**Do:**
```csharp
List<Artist> artists = new List<Artist>();
ArtistController controller = new ArtistController(mockRepo);
string name = "example";
```

**Don't:**
```csharp
var artists = new List<Artist>();
var controller = new ArtistController(mockRepo);
var name = "example";
```

## Exception:

The only acceptable use of `var` is when the type is obvious from the right-hand side and using the explicit type would be redundant, such as:

```csharp
var dictionary = new Dictionary<string, List<ComplexTypeName>>();
```

But even in these cases, prefer explicit types for consistency.