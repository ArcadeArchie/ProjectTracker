ProjectTracker
=============

Welcome to the ProjectTracker source code!

From this repository you can build the ProjectTracker versions and modify them in any way you can imagine, and share your changes with others!

Getting up and running
----------------------
This Project is build using AvaloniaUI, .NET 5 and Xamarin.Forms, so for building and developing the Project you'll need:
* VS 2019 16.9 or higher
  * With Mobile, Cross Platfrom and Desktop Workloads installed
* .NET SDK 5.0 or higher
* Add the Gitlab NuGet feed (as its required for some packages of mine)
```
nuget source Add -Name "Arcade GitLab" -Source "https://git.nstrassburger.de/api/v4/projects/21/packages/nuget/index.json"
```

## Guides
- [Getting Started with .NET][dotnet-quickstart]
- [Getting Started with Xamarin][xamarin-quickstart]
- [Getting Started with AvaloniaUI][avaloniaui-quickstart]
- [Install NuGet][install-nuget]

[dotnet-quickstart]: https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro
[xamarin-quickstart]: https://dotnet.microsoft.com/learn/xamarin/hello-world-tutorial/intro
[avaloniaui-quickstart]: https://docs.avaloniaui.net/docs/getting-started
[install-nuget]: https://www.nuget.org/downloads

## Folder structure
### src/Desktop
Contains all Folders and Files for building and debugging the Desktop version of the Tracker

-----
### src/Mobile
Contains all Folders and Files for building and debugging the Mobile version of the Tracker

-----
### Legacy

Contains all Folders and Files for building and debugging the Older Desktop versions of the Tracker 
##### !May not work and will not be developed anymore!

-----

## Resources
Feature/Progress Tracking: [Trello](https://trello.com/b/DEfts4IE/project-tracker-mobile)
### Docs
* [.NET](https://docs.microsoft.com/de-de/dotnet/)
* [Xamarin.Forms](https://docs.microsoft.com/de-de/xamarin/xamarin-forms/)
* [AvaloniaUI](https://docs.avaloniaui.net)
