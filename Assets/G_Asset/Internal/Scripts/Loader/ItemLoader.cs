using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ItemLoader
{
    [MenuItem("Load/Load and Create All")]
    public static void LoadAll()
    {
        SaveEnemyItems();
        SaveMapItems();
        LoadMapItems();
        LoadCardItems();
        LoadEnemyItems();
    }
    [MenuItem("Load/Load CardItems")]
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
    [MenuItem("Load/Load Enemy")]
    public static void LoadEnemyItems()
    {
        string folderPath = "Assets/G_Asset/Internal/Enemy/Default";
        string[] guids = AssetDatabase.FindAssets("t:Object", new[] { folderPath });
        List<Enemy> enemies = new();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Enemy asset = AssetDatabase.LoadAssetAtPath<Enemy>(assetPath);
            if (asset is Enemy enemy)
            {
                enemies.Add(enemy);
            }
        }
        EnemyPrefabManager enemyPrefab = GameObject.FindObjectOfType<EnemyPrefabManager>();
        if (enemyPrefab != null)
        {
            enemyPrefab.LoadListEnemy(enemies);
        }
    }
    [MenuItem("Load/Load Map")]
    public static void LoadMapItems()
    {
        string folderPath = "Assets/G_Asset/Internal/Map/MapObject";
        string[] guids = AssetDatabase.FindAssets("t:MapScriptableObject", new[] { folderPath });
        List<MapScriptableObject> maps = new();
        foreach (string guild in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guild);
            MapScriptableObject map = AssetDatabase.LoadAssetAtPath<MapScriptableObject>(assetPath);
            if (map != null)
            {
                maps.Add(map);
            }
        }
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.LoadMap(maps);
        }
    }
    [MenuItem("Map/Create map")]
    public static void SaveMapItems()
    {
        string folderPath = "Assets/G_Asset/Internal/Map/MapObject";
        string mapImageStorePath = "Assets/G_Asset/Local/Map";

        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { mapImageStorePath });
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            Texture2D mapSprite = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (mapSprite != null)
            {
                string mapAssetPath = $"{folderPath}/{mapSprite.name}.asset";
                MapScriptableObject map = AssetDatabase.LoadAssetAtPath<MapScriptableObject>(mapAssetPath);
                if (map == null)
                {
                    map = ScriptableObject.CreateInstance<MapScriptableObject>();
                    AssetDatabase.CreateAsset(map, mapAssetPath);
                }
                map.map = mapSprite;
                EditorUtility.SetDirty(map);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Enemy/Create enemy")]
    public static void SaveEnemyItems()
    {

        Dictionary<string, EnemyName> enemyNameDictionary = new();
        var enemyEnumList = Enum.GetValues(typeof(EnemyName));
        foreach (EnemyName name in enemyEnumList)
        {
            string enumName = Enum.GetName(typeof(EnemyName), name);
            enemyNameDictionary.Add(enumName, name);
        }

        string enemySavePath = "Assets/G_Asset/Internal/Enemy/Default";
        string enemySaveScriptableObject = "Assets/G_Asset/Internal/Enemy/ScriptableObject";

        string enemyRootImage = "Assets/G_Asset/Local/Enemy/Image";
        string enemyRootAnimationImage = "Assets/G_Asset/Local/Enemy/Animation";

        string[] enemyGuids = AssetDatabase.FindAssets("t:Sprite", new[] { enemyRootImage });
        foreach (string guid in enemyGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite enemySprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            string animatorPath = $"{enemyRootAnimationImage}/{enemySprite.name}.controller";

            RuntimeAnimatorController animator = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(animatorPath);
            if (animator == null)
            {
                string overrideControllerPath = $"{enemyRootAnimationImage}/{enemySprite.name}.overrideController";
                AnimatorOverrideController overrideController = AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(overrideControllerPath);
                animator = overrideController;
            }
            if (enemySprite != null)
            {
                string scriptObjectPath = $"{enemySaveScriptableObject}/{enemySprite.name}.asset";
                EnemyScriptableObject enemyScriptableObject = AssetDatabase.LoadAssetAtPath<EnemyScriptableObject>(scriptObjectPath);
                /*if (enemyScriptableObject != null)
                {
                    AssetDatabase.DeleteAsset(scriptObjectPath);
                    enemyScriptableObject = null;
                }*/
                if (enemyScriptableObject == null)
                {
                    enemyScriptableObject = ScriptableObject.CreateInstance<EnemyScriptableObject>();
                    EnemyName enemyName = enemyNameDictionary[enemySprite.name];
                    enemyScriptableObject.enemyName = enemyName;
                    AssetDatabase.CreateAsset(enemyScriptableObject, scriptObjectPath);
                    AssetDatabase.SaveAssets();
                }

                string enemyPath = $"{enemySavePath}/{enemySprite.name}.prefab";
                GameObject enemyObject = AssetDatabase.LoadAssetAtPath<GameObject>(enemyPath);
                if (enemyObject != null)
                {
                    AssetDatabase.DeleteAsset(enemyPath);
                    enemyObject = null;
                }
                if (enemyObject == null)
                {
                    enemyObject = new(enemySprite.name);

                    enemyObject.transform.localScale = new(0.16f, 0.16f, 1f);
                    SpriteRenderer spriteRender = enemyObject.AddComponent<SpriteRenderer>();
                    CapsuleCollider2D enemyCollider = enemyObject.AddComponent<CapsuleCollider2D>();

                    enemyCollider.offset = new(0.02123863f, 0f);
                    enemyCollider.size = new(0.8725682f, 1f);

                    spriteRender.sprite = enemySprite;
                    spriteRender.sortingOrder = 5;
                    EnemyChaseTarget enemyTarget = enemyObject.AddComponent<EnemyChaseTarget>();
                    Animator enemyAnimator = enemyObject.GetComponent<Animator>();
                    Rigidbody2D enemyRb = enemyObject.GetComponent<Rigidbody2D>();
                    enemyRb.gravityScale = 0;
                    enemyTarget.SetDefaultEnemy(enemyScriptableObject);
                    enemyAnimator.runtimeAnimatorController = animator;
                    PrefabUtility.SaveAsPrefabAsset(enemyObject, enemyPath);

                    GameObject.DestroyImmediate(enemyObject);
                }
            }
        }
        AssetDatabase.Refresh();
    }
}
