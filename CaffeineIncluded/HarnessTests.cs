#if DEBUG
// In-game tests run by Gravitas.TestHarness (python Gravitas.TestProbe/probe.py run --project <this repo> --tests).
// The harness matches these types by simple name, so this mod does not reference the harness assembly.
using System;
using System.Linq;

namespace CaffeineIncluded;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class HarnessTestAttribute : Attribute
{
    public string Name { get; set; }
    public string Tags { get; set; }
}

internal sealed class HarnessAssertionException : Exception
{
    public HarnessAssertionException(string message) : base(message) { }
}

internal static class HarnessTests
{
    [HarnessTest(Name = "caffeine.espresso_effect_replaced", Tags = "caffeine")]
    public static void EspressoEffectReplaced()
    {
        var espresso = Db.Get().effects.Get(EspressoTuning.VanillaEffectId);
        var athleticsId = Db.Get().Attributes.Athletics.Id;
        var mods = espresso.SelfModifiers.Where(m => m.AttributeId == athleticsId).ToList();
        if (mods.Count != 1) throw new HarnessAssertionException("expected exactly one Athletics modifier, found " + mods.Count);
        if (mods[0].Value != EspressoTuning.AthleticsModifier)
            throw new HarnessAssertionException("Athletics modifier " + mods[0].Value + " != " + EspressoTuning.AthleticsModifier);
        if (espresso.duration != EspressoTuning.AthleticsDurationSeconds)
            throw new HarnessAssertionException("duration " + espresso.duration + " != " + EspressoTuning.AthleticsDurationSeconds);
    }

    [HarnessTest(Name = "caffeine.stamina_effect_registered", Tags = "caffeine")]
    public static void StaminaEffectRegistered()
    {
        var effect = Db.Get().effects.TryGet(EspressoTuning.StaminaEffectId);
        if (effect == null) throw new HarnessAssertionException("effect " + EspressoTuning.StaminaEffectId + " is not registered");
    }
}
#endif
