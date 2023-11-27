<!-- Begin README -->

<div align="center">
    <a href="https://github.com/scottgriv/uwp-audio_recorder" target="_blank">
        <picture>
            <source media="(prefers-color-scheme: dark)" srcset="./docs/images/icon-dark.png">
            <source media="(prefers-color-scheme: light)" srcset="./docs/images/icon-light.png">
            <img alt="Icon" src="./docs/images/icon-dark.png" width="207" height="207">
        </picture>
    </a>
</div>
<p align="center">
    <a href="https://docs.microsoft.com/en-us/dotnet/csharp/"><img src="https://img.shields.io/badge/C Sharp-7.3.0-6C287D?style=for-the-badge&logo=csharp" alt="C# Badge" /></a>
    <a href="https://docs.microsoft.com/en-us/windows/uwp/"><img src="https://img.shields.io/badge/UWP-10.0.x-0178D6?style=for-the-badge&logo=dotnet" alt="UWP Badge" /></a>
    <a href="https://docs.microsoft.com/en-us/visualstudio/"><img src="https://img.shields.io/badge/Visual Studio-17.3.x-5C2D91?style=for-the-badge&logo=visualstudio" alt="VS Badge" /></a>
    <br>
    <a href="https://github.com/scottgriv"><img src="https://img.shields.io/badge/github-follow_me-9031AC?style=for-the-badge&logo=github&color=9031AC" alt="GitHub Badge" /></a>
    <a href="mailto:scott.grivner@gmail.com"><img src="https://img.shields.io/badge/gmail-contact_me-DC4233?style=for-the-badge&logo=gmail" alt="Email Badge" /></a>
    <a href="https://www.buymeacoffee.com/scottgriv"><img src="https://img.shields.io/badge/buy_me_a_coffee-support_me-FEDE1F?style=for-the-badge&logo=buymeacoffee&color=FEDE1F" alt="BuyMeACoffee Badge" /></a>
    <br>
    <a href="https://prgoptimized.com" target="_blank"><img src="https://img.shields.io/badge/PRG-Silver Project-C0C0C0?style=for-the-badge&logo=data:image/svg%2bxml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBzdGFuZGFsb25lPSJubyI/Pgo8IURPQ1RZUEUgc3ZnIFBVQkxJQyAiLS8vVzNDLy9EVEQgU1ZHIDIwMDEwOTA0Ly9FTiIKICJodHRwOi8vd3d3LnczLm9yZy9UUi8yMDAxL1JFQy1TVkctMjAwMTA5MDQvRFREL3N2ZzEwLmR0ZCI+CjxzdmcgdmVyc2lvbj0iMS4wIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciCiB3aWR0aD0iMjYuMDAwMDAwcHQiIGhlaWdodD0iMzQuMDAwMDAwcHQiIHZpZXdCb3g9IjAgMCAyNi4wMDAwMDAgMzQuMDAwMDAwIgogcHJlc2VydmVBc3BlY3RSYXRpbz0ieE1pZFlNaWQgbWVldCI+Cgo8ZyB0cmFuc2Zvcm09InRyYW5zbGF0ZSgwLjAwMDAwMCwzNC4wMDAwMDApIHNjYWxlKDAuMTAwMDAwLC0wLjEwMDAwMCkiCmZpbGw9IiNDMEMwQzAiIHN0cm9rZT0ibm9uZSI+CjxwYXRoIGQ9Ik0xMiAzMjggYy04IC04IC0xMiAtNTEgLTEyIC0xMzUgMCAtMTA5IDIgLTEyNSAxOSAtMTQwIDQyIC0zOCA0OAotNDIgNTkgLTMxIDcgNyAxNyA2IDMxIC0xIDEzIC03IDIxIC04IDIxIC0yIDAgNiAyOCAxMSA2MyAxMyBsNjIgMyAwIDE1MCAwCjE1MCAtMTE1IDMgYy04MSAyIC0xMTkgLTEgLTEyOCAtMTB6IG0xMDIgLTc0IGMtNiAtMzMgLTUgLTM2IDE3IC0zMiAxOCAyIDIzCjggMjEgMjUgLTMgMjQgMTUgNDAgMzAgMjUgMTQgLTE0IC0xNyAtNTkgLTQ4IC02NiAtMjAgLTUgLTIzIC0xMSAtMTggLTMyIDYKLTIxIDMgLTI1IC0xMSAtMjIgLTE2IDIgLTE4IDEzIC0xOCA2NiAxIDc3IDAgNzIgMTggNzIgMTMgMCAxNSAtNyA5IC0zNnoKbTExNiAtMTY5IGMwIC0yMyAtMyAtMjUgLTQ5IC0yNSAtNDAgMCAtNTAgMyAtNTQgMjAgLTMgMTQgLTE0IDIwIC0zMiAyMCAtMTgKMCAtMjkgLTYgLTMyIC0yMCAtNyAtMjUgLTIzIC0yNiAtMjMgLTIgMCAyOSA4IDMyIDEwMiAzMiA4NyAwIDg4IDAgODggLTI1eiIvPgo8L2c+Cjwvc3ZnPgo=" alt="Silver" /></a>
</p>

---------------

<h1 align="center">🎙️🔊 UWP C# Audio Recorder Application 🔊🎙️</h1>

A simple **Audio Recorder** and Playback application written in `C#` using **Visual Studio** and the **Universal Windows Platform (UWP)** framework. 
- Under the **Record** tab:
    - Record audio
- Under the **Archive** tab:
    - Playback audio
    - Delete audio
    - Export audio to a `mp3` file

<div align="center">
    <img src="./docs/images/demo.gif" style="width: 75%;"/>
    <br>
    <i>Application Preview</i>
</div>

---------------

## Table of Contents

- [What is the Universal Windows Platform (UWP)?](#what-is-the-universal-windows-platform-uwp)
- [Getting Started](#getting-started)
    - [Requirements](#requirements)
    - [Installation](#installation)
    - [Usage](#usage)
- [References](#references)
- [License](#license)
- [Credits](#credits)

## What is the Universal Windows Platform (UWP)?

The **Universal Windows Platform (UWP)** is a platform-homogeneous application architecture created by Microsoft and first introduced in Windows 10. The purpose of this software platform is to help develop Metro-style apps that run on both Windows 10 and Windows 10 Mobile without the need to be re-written for each. It supports Windows app development using C++, C#, VB.NET, and XAML. Developers can build UWP apps that exclusively use UWP controls and APIs, or they can use UWP controls and APIs in desktop apps that are built using one of the supported Windows app frameworks.

<div align="center">
    <a href="" target="_blank">
        <img src="./docs/images/UWP.png" style="width: 50%;"/>
    </a>
    <br>
    <i>What is the Universal Windows Platform (UWP)?</i>
</div>

## Getting Started

### Requirements

- Visual Studio 2022
- Windows 10 SDK (10.0.19041.0)

### Installation

1. Clone the repository
2. Open the project in Visual Studio
3. Build the project
4. Run the project

### Usage

- Click the `Record` button to start recording audio
- Click the `Stop` button to stop recording audio
- Click the `Play` button to play the recorded audio
- Click the `Delete` button to delete the recorded audio
- Click the `Export` button to export the recorded audio to a `mp3` file

## References

- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/) - `C#` is a simple, modern, object-oriented, and type-safe programming language (used to develop the application).)
- [Universal Windows Platform (UWP) documentation](https://docs.microsoft.com/en-us/windows/uwp/) - The Universal Windows Platform (UWP) is a platform-homogeneous application architecture created by Microsoft and first introduced in Windows 10.
- [Visual Studio Documentation](https://docs.microsoft.com/en-us/visualstudio/) - Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs, as well as websites, web apps, web services and mobile apps.

## License

This project is released under the terms of the **MIT License**, which permits use, modification, and distribution of the code, subject to the conditions outlined in the license.
- The [MIT License](https://choosealicense.com/licenses/mit/) provides certain freedoms while preserving rights of attribution to the original creators.
- For more details, see the [LICENSE](LICENSE) file in this repository. in this repository.

## Credits

**Author:** [Scott Grivner](https://github.com/scottgriv) <br>
**Email:** [scott.grivner@gmail.com](mailto:scott.grivner@gmail.com) <br>
**Website:** [scottgrivner.dev](https://www.scottgrivner.dev) <br>
**Reference:** [Main Branch](https://github.com/scottgriv/uwp-audio_recorder) <br>

---------------

<div align="center">
    <a href="https://github.com/scottgriv" target="_blank">
        <img src="./docs/images/footer.png" width="100" height="100"/>
    </a>
</div>

<!-- End README -->