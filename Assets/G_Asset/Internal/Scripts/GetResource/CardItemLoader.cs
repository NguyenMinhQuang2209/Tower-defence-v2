using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CardItemLoader
{
    [MenuItem("Tools/Load CardItems")]
    public static void LoadCardItems()
    {
        string folderPath = "Assets/G_Asset/Internal/Card";
        string[] guids = AssetDatabase.FindAssets("t:CardItem", new[] { folderPath });
        List<CardItem> cardItems = new List<CardItem>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            CardItem cardItem = AssetDatabase.LoadAssetAtPath<CardItem>(assetPath);
            if (cardItem != null)
            {
                cardItems.Add(cardItem);
            }
        }
        PrefabPreferenceManager gameManager = GameObject.FindObjectOfType<PrefabPreferenceManager>();
        if (gameManager != null)
        {
            gameManager.SetListCardItem(cardItems);
        }
    }
}
