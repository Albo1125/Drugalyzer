# Drugalyzer
Drugalyzer is a UK-based resource for FiveM by Albo1125 that provides saliva drug testing functionality for drivers. It is available at [https://github.com/Albo1125/Drugalyzer](https://github.com/Albo1125/Drugalyzer)

## Installation & Usage
1. Download the latest release.
2. Unzip the Drugalyzer folder into your resources folder on your FiveM server.
3. Add the following to your server.cfg file:
```text
ensure Drugalyzer
```
4. Optionally, customise the commands in `sv_Drugalyzer.lua`.
5. Optionally, enable and customise the whitelist in `vars.lua`.

## Commands & Controls
* /druga ID - Drugalyzes the player with ID. Aliases /drugalyze /drugalyse
* /saliva CANNABIS COCAINE - Returns a saliva sample. Replace CANNABIS and COCAINE with either true or false depending on if they are present in your saliva.
* /failprovide - Fail to provide a proper sample if currently being drugalyzed.

## Improvements & Licencing
Please view the license. Improvements and new feature additions are very welcome, please feel free to create a pull request. As a guideline, please do not release separate versions with minor modifications, but contribute to this repository directly. However, if you really do wish to release modified versions of my work, proper credit is always required and you should always link back to this original source and respect the licence.

## Libraries used (many thanks to their authors)
* [CitizenFX.Core.Client](https://www.nuget.org/packages/CitizenFX.Core.Client)
