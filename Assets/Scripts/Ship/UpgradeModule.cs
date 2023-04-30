using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModuleUI : MonoBehaviour
{
    // function for buttons to upgrade modules

    public void UpgradeModule(ref ShipStats ship, string moduleName)
    {
        ship.UpgradeModuleLevel(moduleName);
    }
}
