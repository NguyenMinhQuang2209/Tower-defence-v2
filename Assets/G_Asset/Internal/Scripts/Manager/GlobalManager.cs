using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;

    [field: SerializeField] public int defaultFreeReward { get; private set; } = 1;

    [field: SerializeField] public LayerMask enemiesMask { get; private set; } = 6;

    [field: SerializeField] public int defaultFirstRewardPrice { get; private set; } = 1;
    [field: SerializeField] public int rewardPricePlus { get; private set; } = 1;

    [Tooltip("At hour in minutes in day remember multiple with 60 to get hour")]
    [field: SerializeField] public float defaultGetFreeRewardAt { get; private set; } = 12f;
    [field: SerializeField] public int defaultStartDay { get; private set; } = 0;
    [field: SerializeField] public float defaultRateTimeSpeed { get; private set; } = 1f;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

}
