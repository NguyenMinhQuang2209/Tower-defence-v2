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
    BuildingItem
}
public static class PayMessageExtensions
{
    private static readonly Dictionary<PayErrorMessage, string> payErrorMessageStrings = new Dictionary<PayErrorMessage, string>
    {
        { PayErrorMessage.NotEnoughCoin, "Not enough coin!" },
        { PayErrorMessage.PleaseChooseReward, "Please choose reward first!" },

    };
    private static readonly Dictionary<PayMessage, string> payMessageStrings = new Dictionary<PayMessage, string>
    {
        { PayMessage.FreeRewardRemain, "The free times remain: " },

    };
    private static readonly Dictionary<PayName, string> PayNameStrings = new Dictionary<PayName, string>
    {
        { PayName.Reward, "Reward" },
        { PayName.StoreCard, "StoreCard" },
        { PayName.BuildingItem, "BuildingItem" },

    };

    public static string GetString(this PayErrorMessage payMessage)
    {
        return payErrorMessageStrings[payMessage];
    }
    public static string GetString(this PayName payMessage)
    {
        return PayNameStrings[payMessage];
    }
    public static string GetString(this PayMessage payMessage)
    {
        return payMessageStrings[payMessage];
    }
}