# Heating Optimizer

## Overview

The Heating Optimizer is a C# application designed to optimize heating production costs based on real-time data. 
It processes heating demand and electricity prices from CSV files and calculates cost-effective production strategies using different heating units.

### Prerequisites

- .NET SDK 6.0 or later
- Visual Studio Code or Visual Studio
- Avalonia UI framework (for UI components)

### Instructions on how to use the app

1. Run the project:

   use 'dotnet run' in the therminal

2. Load the machines:

   press 'Load Machines'
   select your machines file (.csv or .json)
   press 'confirm'

3. Open Timeframe Data:

   in the topbar select 'Open Timeframe Data' 
   select your Timeframe
   on the right side bar, selcet the graph type
   select what machines you want to use (GB1, GB2, OB1, GM1, HP1)

4. If you need to edit a machine:

   in the topbar select 'edit machine info'
   press on any data you want to edit
   press enter on keyboard
   you will see how it updates live

### How to Build the project?

1. Clone the repository:

   git clone https://github.com/your-repo/heating-optimizer.git
   cd heating-optimizer

2. Build the project:

   dotnet build

3. Run the application:

   dotnet run

## How to pull?

1. Fork the repository.
2. Create a new branch: "git checkout -b feature-name".
3. Commit changes: "git commit -m "Add new feature" ".
4. Push to the branch: "git push origin feature-name".
5. Open a pull request.
