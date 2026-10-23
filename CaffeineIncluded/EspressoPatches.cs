using HarmonyLib;
using Klei.AI;

namespace CaffeineIncluded;

public static class EspressoTuning
{
    public const string StaminaEffectId = "CaffeineIncludedEspressoStamina";
    public const float AthleticsModifier = -2f;
    public const float AthleticsDurationSeconds = 0.75f * 600f;
    public const float StaminaRestoreTotal = 35f;
    public const float StaminaRestoreSeconds = 60f;

    // 35 / 60 = 0.5833 per second (= 350 per 600 s cycle)
    public const float StaminaPerSecond = StaminaRestoreTotal / StaminaRestoreSeconds;
}

[HarmonyPatch(typeof(Db), nameof(Db.Initialize))]
public static class DbInitializePatch
{
    public static void Postfix(Db __instance)
    {
        var effects = __instance.effects;

        var espresso = effects.Get("Espresso");
        var athletics = Db.Get().Attributes.Athletics.Id;
        espresso.SelfModifiers.RemoveAll(m => m.AttributeId == athletics);
        espresso.SelfModifiers.Add(new AttributeModifier(
            athletics,
            EspressoTuning.AthleticsModifier,
            espresso.Name));
        espresso.duration = EspressoTuning.AthleticsDurationSeconds;

        var stamina = new Effect(
            EspressoTuning.StaminaEffectId,
            "Espresso Energy",
            "Gradually restores stamina.",
            EspressoTuning.StaminaRestoreSeconds,
            show_in_ui: true,
            trigger_floating_text: false,
            is_bad: false);
        stamina.Add(new AttributeModifier(
            Db.Get().Amounts.Stamina.deltaAttribute.Id,
            EspressoTuning.StaminaPerSecond,
            "Espresso Energy"));
        effects.Add(stamina);
    }
}

[HarmonyPatch(typeof(EspressoMachineWorkable), "OnCompleteWork")]
public static class EspressoMachineWorkableOnCompleteWorkPatch
{
    public static void Postfix(WorkerBase worker)
    {
        if (Db.Get().Amounts.Stamina.Lookup(worker.gameObject) == null)
        {
            return;
        }

        worker.GetComponent<Effects>().Add(EspressoTuning.StaminaEffectId, should_save: true);
    }
}
