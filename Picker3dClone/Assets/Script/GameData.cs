using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("Coin-Gem")]
    public int coinCount;
    public int gemCount;


    public int levelInd;
    public int lastPlayedLevelInd;
    public bool randomizeLevel;


    [Header("Level List")]
    public List<LevelData> levels;

    [Header("Picker Data")]
    public PickerData pickerData;


}
[System.Serializable]
public class PickerData
{
    public Vector3 pickerPos;

}
[System.Serializable]
public class Collectible
{
    public int collectibleType;
    public Vector3 pos;

}
[System.Serializable]
public class Platforms
{
    public Vector3 pos;
}

[System.Serializable]
public class LevelData
{
    //public string levelID;

    public List<Collectible> collectibles;
    public List<Platforms> platforms;
    public List<int> targetValue;
    //public  colletible;
    public int levelind;
    //public int price;
}
