using UnityEngine;

[CreateAssetMenu(fileName ="DungeonGenerationData.asset", menuName ="DungenGenerationData/Dungeon Data")]

public class DungeonGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
