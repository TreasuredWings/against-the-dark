using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    void OnEnable()
    {
        // Tell the upgrade manager we gotta roll upgrades
        UpgradeManager.Instance.RollUpgrades();
    }
}
