using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelScores", menuName = "ScriptableObjects/LevelScores")]
public class LevelScoresSO : ScriptableObject
{
    [field:SerializeField] public List<LevelScoreWrapper> LevelScores { get; set; }

    public LevelScoresSO(LevelScoresSO origin)
    {
        LevelScores = origin.LevelScores;
    }
}

[Serializable]
public struct LevelScoreWrapper
{
    public string LevelName;
    public int Score;

    public static LevelScoreWrapper DivideScore(LevelScoreWrapper lsw, int q)
    {
        lsw.Score /= q;
        return lsw;
    }
}

public static class LevelScoresSOExt
{
    public static LevelScoreWrapper GetLSW(this List<LevelScoreWrapper> lsw, string name) => lsw.Find((x => x.LevelName == name));
    
    public static int GetIndex(this List<LevelScoreWrapper> lsws, LevelScoreWrapper lsw)
    {
        return lsws.FindIndex((x => x.LevelName == lsw.LevelName));;
    }
    
    public static int GetIndex(this List<LevelScoreWrapper> lsws, string name, out LevelScoreWrapper lsw)
    {
        lsw = lsws.Find(x => x.LevelName == name);
        return lsws.FindIndex((x => x.LevelName == name));;
    }
    public static int GetScore(this List<LevelScoreWrapper> lsw, string name)
    {
        return lsw.Find((x => x.LevelName == name)).Score;
    }
    //
    // public static LevelScoreWrapper DivideScore(this LevelScoreWrapper lsw, int q)
    // {
    //     lsw.Score /= q;
    //     return lsw;
    // }
}
    
