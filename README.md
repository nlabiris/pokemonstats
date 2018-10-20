# Pokémon Stats

[![Software License](https://img.shields.io/github/license/nlabiris/pokemonstats.svg)](LICENSE.md)
[![GitHub (pre-)release](https://img.shields.io/badge/beta-v0.1.1-red.svg)](https://github.com/nlabiris/pokemonstats/releases)

A simple Pokédex written in C# using [WPF](https://docs.microsoft.com/en-us/dotnet/framework/wpf/getting-started/). All the data are comes from [veekun pokedex](https://github.com/veekun/pokedex) and are hosted in a MySQL server. The solution is created using Visual Studio 2017.

![](https://i.imgur.com/Wuz2mpg.png)

![](https://i.imgur.com/BZKieyT.png)

## Prerequisites

* [Visual Studio 2017 version 15.7.3 or later](https://www.microsoft.com/net/download/windows) with the following workloads:
   *  WPF (Windows Presentation Foundation)
* MySQL or MariaDB server
* [.NET Framework 4.7.2 or later](https://www.microsoft.com/net/download/windows)

## Usage

First of all, get the [veekun pokedex](https://github.com/veekun/pokedex) and follow the instructions in order to setup the database.

Then, `clone` this repository or download as `zip` file.
* Using git: `git clone https://github.com/nlabiris/pokemonstats.git`
* [Download zip](https://github.com/nlabiris/pokemonstats/archive/master.zip) and extract to your desired location

However, a simple text editor, eg. VS Code, will do the job. (if you know what files need to change).

### Setup database connection

#### 1. Using Visual Studio

From the menu, click on **Project > PokemonStats_WPF Properties...**, click at the Settings tab and fill the `DatabaseConnectionString` and `DatabaseUsed` fields. For the connection string check [MySQL connection strings](https://www.connectionstrings.com/mysql/) for the appropriate format.
Enter `MySQL` at the `DatabaseUsed` field.

#### 2. Using a simple text editor

Open the following files:
* `App-template.config`
* `Properties/Settings.settings`
* `Properties/Settings.Designer.cs`

> You need to rename `App-template.config` to `App.config` in order to build the project.

Then make the necessary changes at the connection string and database options (like the Visual Studio step).

## Change log

Please see [CHANGELOG](CHANGELOG.md) for more information on what has changed recently.

## Credits

* **Nikos Labiris** - *Initial work* - [nlabiris](https://github.com/nlabiris)

See also the list of [contributors](https://github.com/nlabiris/pokemonstats/graphs/contributors) who participated in this project.

### Contributing

Please see [CONTRIBUTING](CONTRIBUTING.md) and [CODE_OF_CONDUCT](CODE_OF_CONDUCT.md) for details.

### Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/nlabiris/pokemonstats/releases). 

## License

This project is licensed under the *GNU Affero General Public License v3.0* (AGPLv3). Please see [License File](LICENSE.md) for more information.