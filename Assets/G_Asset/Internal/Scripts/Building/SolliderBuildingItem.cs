using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolliderBuildingItem : BuildingItem
{
    private Sollider sollider;
    private void Start()
    {
        sollider = GetComponent<Sollider>();
    }
    public override void BuildItemInit()
    {
        if (requires.Count > 0)
        {
            Collider2D parent = requires[0];
            transform.SetParent(parent.gameObject.transform, true);
        }
        sollider.HideAttackSize();
        base.BuildItemInit();
    }
}
