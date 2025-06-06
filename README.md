# DND-Cloud-Dragon

This repository hosts utilities and tools for Dungeons & Dragons character creation and world building.

## About

DND-Cloud-Dragon began as an internal Elastacloud project and is now open source. It contains helpers for:

- Character creation
- Campaign assistance
- Documenting ideas
- World building support
- Standard array and point-buy calculators
- File-based combat session persistence

## Getting Started

Use the Azure Functions API to generate ability scores and manage combat. For example:

- `/roll-stats` rolls 4d6 dropping the lowest die
- `/standard-array` returns the standard ability score array

## Hopes for the Project

Our goal is to make character building fast and enjoyable for players and Dungeon Masters alike. Contributions are welcome!

## Future Updates

Stay tuned for more features and improvements.

## Disclaimer

This project is provided as-is and is not affiliated with Wizards of the Coast.

## Credits

Special thanks to everyone contributing to the project.

## Running Tests

Ensure the [.NET SDK](https://dotnet.microsoft.com/download) is installed. From the repository root run:

```bash
dotnet test CloudDragonLib/CloudDragonLib.sln
```

This command builds the solution and executes all unit tests, including those in `CloudDragon.Tests`. Continuous integration runs these tests automatically via GitHub Actions. See `.github/workflows/dotnet.yml` for details.
