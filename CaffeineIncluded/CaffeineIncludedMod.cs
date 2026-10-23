using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;

namespace CaffeineIncluded;

public sealed class CaffeineIncludedMod : UserMod2
{
    public override void OnLoad(Harmony harmony)
    {
        base.OnLoad(harmony);
        PUtil.InitLibrary();
    }
}
