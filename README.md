# Slicer & Sprinkles
<!-- TOC -->
* [Slicer & Sprinkles](#slicer--sprinkles)
  * [Engine](#engine)
  * [Standards](#standards)
    * [Code](#code)
    * [Files & Folders](#files--folders)
    * [Git](#git)
    * [License](#license)
    * [Authors](#authors)
<!-- TOC -->
A fruit-ninja-like in the theme of bakery. You cut ingredients to make a cake at the end of the round.

## Engine

The game is made with [Unity 3D](https://unity.com/).

## Standards

### Code

C# code is formatted with Rider settings.
Braces are on a new line.
140 characters per line.
Tab size is 4 and is used for indentation.

Use `var` when possible.
Do not use explicit types when it can be inferred.
Use short syntax for getters.
Use LINQ when possible.


**Naming conventions:**

- Automatic properties: `PascalCase`
- Classes: `PascalCase`
- Constants: `PascalCase`
- Delegates: `PascalCase`
- Enum values: `PascalCase`
- Enums: `PascalCase`
- Events: `PascalCase`
- Fields: `PascalCase`
- Interfaces: `PascalCase`
- Methods: `PascalCase`
- Namespaces: `PascalCase`
- Private fields: `_camelCase`
- Private static fields: `_camelCase`
- Properties: `PascalCase`
- Structs: `PascalCase`
- Type parameters: `PascalCase`
- Variables: `camelCase`

**Example:**

```csharp
public class MyClass : MonoBehaviour
{
    private int _myField;
    public int MyProperty { get; set; }
    public int MyMethod(int myParameter) => myParameter.ToString();
    
    private void Start()
    {
        _myField = 0;
        MyProperty = 0;
        MyMethod(0);
        
        var myVariable = 0;
        // LINQ
        var myLinqVariable = myVariable
            .Where(x => x > 0)
            .Select(x => x * 2)
            .ToList();
    }
}
```

### Files & Folders

- Folders are `PascalCase`
- Files are `PascalCase`
- Files are named after the class they contain

Assets are organized in folders by type.
- `Animations`
- `Audio`
- `Fonts`
- `Materials`
- `Models`
- `Prefabs`
- `Scenes`
- `Scripts`
- `Shaders`
- `Sprites`
- `Textures`
- `UI`
- `Video`

### Git

- Commit messages are in English
- Commit messages are in the present tense

Structure of a commit message:
```
type(scope): subject

Closes #issue-number[..., closes #other-issue-number]
```

Types :
- `chore` : updating build tasks, package manager configs, etc; no production code change
- `docs` : changes to documentation
- `feat` : new feature
- `fix` : bug fix
- `perf` : performance improvement*
- `refactor` : refactoring production code
- `style` : formatting, missing semi colons, etc; no code change
- `tweak` : small change to existing feature, like changing a color or a value

Scope is in one or two words, describing the part of the code that is affected by the commit, with a space between the two words and everything lowercase.

Subject is a short description of the change, with a capital letter and no dot at the end.

Closes is used to close issues automatically when the commit is merged, each commit should close at least one issue appart if it is tiny.

Commit regexp:
```regexp
(chore|docs|feat|fix|perf|refactor|style|tweak)(\(.*\)): [A-Z].*\.(\s+Closes #\d+(, closes #\d+)*)?
```

**Example:**

```
feat(ingredients): Add new ingredient.

Closes #42
```

```
tweak(bombs): Change time before explosion.
```

### License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details.

### Authors

- [@Bahsiik](https://github.com/Bahsiik)
- [@Ayfri](https://github.com/Ayfri)
- [@xhmyjae](https://github.com/xhmyjae)
