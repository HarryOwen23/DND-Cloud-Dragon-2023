<h1 align="center">DND-Cloud-Dragon (Name is a work in progress)</h1>

A DND related project for Elastacloud + beyond. 

<h2 align="left">About</h2>

This is an open source project created to provide support with building for both DND and other ideas.
Things it can be used for: 
- Character creation
- Campaign Assistance
- Document different ideas user's would have in mind
- World building help
- Standard array and point-buy ability score tools
- File-based combat session persistence

<h2 align="left">Getting Started </h2>

Use the Azure Functions API to generate ability scores and manage combat.
`/roll-stats` rolls 4d6 drop lowest, while `/standard-array` returns the standard array.

 <h2 align="left">Hopes for the project</h2>

<p>With DND-Cloud-Dragon-2023, I hope to make an accessible tool to help dnd players make character building a nice and spicy experience!</p>
<p>The tool will also be used for DM's to help with world builing for their campaigns.</p>
<p>Massive thanks to all who want to contribute to the repo!</p>

<h2 align="left">Future Updates</h2>

<h2 align="left">Disclaimer</h2>


<h2 align="left">Credits</h2>


<h3 align="left">Special Thanks</h3>

### Running Tests

Ensure the [.NET SDK](https://dotnet.microsoft.com/download) is installed.
From the repository root execute:

```bash
dotnet test CloudDragonLib/CloudDragonLib.sln
```

This command builds the solution and runs all unit tests including `CloudDragon.Tests`.
Additional tests verify the new standard array method and character generation features.

