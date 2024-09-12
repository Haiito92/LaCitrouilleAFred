using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelScores", menuName = "ScriptableObjects/LevelScores")]
public class LevelScoresSO : ScriptableObject
{
    [field:SerializeField] public SerializableDictionary<string, int> LevelScores { get; set; }
}
