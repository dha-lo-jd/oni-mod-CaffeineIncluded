# CaffeineIncluded

An Oxygen Not Included mod (`netstandard2.1`) that changes the vanilla Espresso Machine.

## Effect

- Restores 35 stamina (35% of max) over 60 seconds (0.5833/s = 350/cycle).
- Athletics modifier of the "Espresso" effect: +1 -> -2.

Tuning values live in `CaffeineIncluded/EspressoTuning.cs`, the Harmony patches in `CaffeineIncluded/EspressoPatches.cs`.

## Compatibility

Verified against the U60 public test build (changelist 761211): the project compiles and the patch targets (`Db.Initialize`, `EspressoMachineWorkable.OnCompleteWork`) still exist.

## Build and install

`CaffeineIncluded/Local.Build.props` is machine-specific and ignored by Git. Copy `CaffeineIncluded/Local.Build.props.example` to `CaffeineIncluded/Local.Build.props` and set:

- `GameManagedPath`: the ONI `OxygenNotIncluded_Data\Managed` directory.
- `ModInstallPath`: the local mod folder (leave empty to skip deployment).

Then build `CaffeineIncluded.sln` in Rider or run `dotnet build CaffeineIncluded.sln`. A successful build copies the assembly and `mod.yaml` / `mod_info.yaml` into `ModInstallPath`.
