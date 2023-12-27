


# Harvest Data Sync

## Updating NuGet Packages
All NuGet packages in solution can be updated using the NuGet manager with exception to the following packages:
 - Microsoft.CodeAnalysis.NetAnalyzers
 - StyleCop.Analyzers
 
 These packages must be updated in the **Directory.Build.props** file found in the **src** directory.
 To update simply update the `Version` number attribute and Visual Studio will automatically update the NuGet packages.

### Other Restricted NuGet Packages

## Analyzers

