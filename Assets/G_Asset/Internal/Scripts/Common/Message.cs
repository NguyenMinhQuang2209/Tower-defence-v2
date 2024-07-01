using System.Collections.Generic;

public enum PayMessage
{
    NotEnoughCoin,
    PleaseChooseReward
}
public enum PayName
{
    Reward,
    StoreCard
}
public static class PayMessageExtensions
{
    private static readonly Dictionary<PayMessage, string> payMessageStrings = new Dictionary<PayMessage, string>
    {
        { PayMessage.NotEnoughCoin, "Not enough coin!" },
        { PayMessage.PleaseChooseReward, "Please choose reward first!" },

    };
    private static readonly Dictionary<PayName, string> PayNameStrings = new Dictionary<PayName, string>
    {
        { PayName.Reward, "Reward" },
        { PayName.StoreCard, "StoreCard" },

    };

    public static string GetString(this PayMessage payMessage)
    {
        return payMessageStrings[payMessage];
    }
    public static string GetString(this PayName payMessage)
    {
        return PayNameStrings[payMessage];
    }
}