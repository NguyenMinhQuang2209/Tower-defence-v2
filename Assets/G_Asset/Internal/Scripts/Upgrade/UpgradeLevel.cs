using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLevel : MonoBehaviour
{
    private void OnMouseDown()
    {
        string currentCursor = UIManager.instance.GetCurrentUI();
        if (currentCursor != PayMessageExtensions.GetString(PayName.BuildingItem))
        {
            Debug.Log("Touch here");
        }
    }
}
