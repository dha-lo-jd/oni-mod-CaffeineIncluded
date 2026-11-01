using HarmonyLib;
using Klei.AI;

namespace CaffeineIncluded;

[HarmonyPatch(typeof(Db), nameof(Db.Initialize))]
public static class DbInitializePatch
{
    public static void Postfix(Db __instance)
    {
        ReplaceAthleticsModifier(__instance);
        AddStaminaEffect(__instance);
    }

    private static void ReplaceAthleticsModifier(Db db)
    {
        var espresso = db.effects.Get(EspressoTuning.VanillaEffectId);
        var athleticsId = db.Attributes.Athletics.Id;

        espresso.SelfModifiers.RemoveAll(m => m.AttributeId == athleticsId);
        espresso.SelfModifiers.Add(new AttributeModifier(athleticsId, EspressoTuning.AthleticsModifier, espresso.Name));
        espresso.duration = EspressoTuning.AthleticsDurationSeconds;
    }

    private static void AddStaminaEffect(Db db)
    {
        const string name = "Espresso Energy";

        var stamina = new Effect(
            EspressoTuning.StaminaEffectId,
            name,
            "Gradually restores stamina.",
            EspressoTuning.StaminaRestoreSeconds,
            show_in_ui: true,
            trigger_floating_text: false,
            is_bad: false);
        stamina.Add(new AttributeModifier(db.Amounts.Stamina.deltaAttribute.Id, EspressoTuning.StaminaPerSecond, name));
        db.effects.Add(stamina);
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
