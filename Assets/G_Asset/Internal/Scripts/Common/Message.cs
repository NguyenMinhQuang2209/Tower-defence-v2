using System.Collections.Generic;

public enum PayErrorMessage
{
    NotEnoughCoin,
    PleaseChooseReward
}
public enum PayMessage
{
    FreeRewardRemain
}
public enum PayName
{
    Reward,
    StoreCard,
    BuildingItem,
    Upgrade
}
public static class PayMessageExtensions
{
    public static string MAX_LEVEL = "(Cấp tối đa)";
    private static readonly Dictionary<PayErrorMessage, string> payErrorMessageStrings = new Dictionary<PayErrorMessage, string>
    {
        { PayErrorMessage.NotEnoughCoin, "Not enough coin!" },
        { PayErrorMessage.PleaseChooseReward, "Please choose reward first!" },

    };
    private static readonly Dictionary<PayMessage, string> payMessageStrings = new Dictionary<PayMessage, string>
    {
        { PayMessage.FreeRewardRemain, "The free times remain: " },

    };
    private static readonly Dictionary<PayName, string> payNameStrings = new Dictionary<PayName, string>
    {
        { PayName.Reward, "Reward" },
        { PayName.StoreCard, "StoreCard" },
        { PayName.BuildingItem, "BuildingItem" },
        { PayName.Upgrade, "Upgrade" },

    };
    public static readonly Dictionary<LevelPlusName, string> payLevelPlusNameStrings = new()
    {
        {LevelPlusName.Health,"Máu" },
        {LevelPlusName.Damage,"Sức mạnh" },
        {LevelPlusName.Speed,"Tốc độ" },
        {LevelPlusName.Range,"Khoảng cách" },
        {LevelPlusName.NumberOfBullet,"Số lượng đạn" },
    };

    public static string GetString(this PayErrorMessage payMessage)
    {
        return payErrorMessageStrings[payMessage];
    }
    public static string GetString(this PayName payMessage)
    {
        return payNameStrings[payMessage];
    }
    public static string GetString(this PayMessage payMessage)
    {
        return payMessageStrings[payMessage];
    }
    public static string GetString(this LevelPlusName payMessage)
    {
        return payLevelPlusNameStrings[payMessage];
    }
}