namespace CaffeineIncluded;

public static class EspressoTuning
{
    public const string VanillaEffectId = "Espresso";
    public const string StaminaEffectId = "CaffeineIncludedEspressoStamina";

    public const float AthleticsModifier = -2f;
    public const float AthleticsDurationSeconds = 0.75f * 600f;

    public const float StaminaRestoreTotal = 35f;
    public const float StaminaRestoreSeconds = 60f;

    // 35 / 60 = 0.5833 per second (= 350 per 600 s cycle)
    public const float StaminaPerSecond = StaminaRestoreTotal / StaminaRestoreSeconds;
}
