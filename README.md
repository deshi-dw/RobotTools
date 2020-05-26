# RobotTools

Robot tools is an application made by Cerberus Robotics for the First Robotics Competition. This application supplies various tools to aid in the testing and development of robots.

## Features

- **Plugin Implementation**
  implementing new tools and utilities is done through plugins, making the application extendable.

- **Custom Networking**
  Uses custom networking, separate from WPILIB entirely. This was to have better flexibility and  minimal use of dependencies.

- **Custom Inputs**
  Uses custom inputs for better selection of input devices. The original WPILIB inputs are quite limited, so this tries to minimize that a bit.

## Dependencies

- Tested with .NET Framework 4.7.2
- .NET Core `dotnet` command.

## How to build

In the root directory, run `dotnet build`. This will create a `bin` folder in the root directory. In that folder should be `RobotToolsApplication.exe`.

## How to use

**Application** If you just want to use the utility as it is, just run download and run the executable.

**Develop** To develop plugins for RobotTools, use the `RobotTools.dll` in the `bin` file. This is a library file of which the documentation can be found in this repository.