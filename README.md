# CaffeineIncluded

An Oxygen Not Included mod project targeting `netstandard2.1`, based on the build and deployment setup in TrueSmartMopping.

## Effect

Changes the vanilla Espresso Machine:

- Restores 35 stamina (35% of max) over 60 seconds (0.5833/s = 350/cycle).
- Athletics modifier of the "Espresso" effect: +1 -> -2.

## Build and install

`CaffeineIncluded/Local.Build.props` is machine-specific and ignored by Git. It points to the ONI `OxygenNotIncluded_Data/Managed` directory and the local mod install directory. The current machine's paths are already configured. For another machine, copy `CaffeineIncluded/Local.Build.props.example` to `CaffeineIncluded/Local.Build.props` and update both paths.

Build `CaffeineIncluded.sln` in Rider or run `dotnet build CaffeineIncluded.sln`. If the per-user .NET SDK is installed but `dotnet` is not on `PATH`, run `& "$env:USERPROFILE\.dotnet\dotnet.exe" build CaffeineIncluded.sln` from PowerShell. A successful build copies the assembly and mod metadata into the configured local mod folder; PLib is merged into the mod assembly as in TrueSmartMopping. If automatic deployment is not needed, leave `ModInstallPath` empty in `Local.Build.props`.

The mod entry point is `CaffeineIncluded/CaffeineIncludedMod.cs`. Add Harmony patches and other mod code to the project, then update `mod.yaml` and `mod_info.yaml` as needed.
