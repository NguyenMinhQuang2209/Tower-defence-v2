using UnityEngine;

public class WeaponBuildingItem : BuildingItem
{
    private Weapon weapon;
    private void Start()
    {
        weapon = GetComponent<Weapon>();
    }
    public override void BuildItemInit(bool isTrigger = true)
    {
        if (requires.Count > 0)
        {
            for (int i = 0; i < requires.Count; i++)
            {
                Collider2D parent = requires[i];
                if (parent.TryGetComponent<Sollider>(out var sollider))
                {
                    sollider.EquipmentWeapon(weapon);
                    base.BuildItemInit(isTrigger);
                    return;
                }
            }
        }
    }
}
