using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Card Game/Level Data")]
public class LevelData : ScriptableObject
{
    public LevelInfo[] levelInfo;
}

[Serializable]
public class LevelInfo
{
    public int levelNumber; //level number
    public int rows = 3;    // num rows in grid
    public int columns = 3; //num columsn in grid
}
