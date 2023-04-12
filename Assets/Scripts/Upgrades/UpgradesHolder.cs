using Project.Assets.Scripts.Upgrades;
using Project.Upgrades;

[System.Serializable]
public class UpgradesHolder
{
    public float Money;
    public int Retries;
    public CastleUpgrades CastleUpgrades = new();

    public PikemanUpgrades PikemanUpgrades = new();
    public BuilderUpgrades BuilderUpgrades = new();
    public KnightUpgrades KnightUpgrades = new();
    public SwordsmanUpgrades SwordsmanUpgrades = new();

    public FireballAbilityUpgrades FireballAbilityUpgrades = new();
    public CashBonusAbilityUpgrades CashBonusAbilityUpgrades = new();
    public PoisonAbilityUpgrades PoisonAbilityUpgrades = new();
    public TimeWarpAbilityUpgrades TimeWarpAbilityUpgrades = new();

    public ArcherTurretUpgrades ArcherTurretUpgrades = new();
}
