using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SceneSoundData_SO", menuName = "SceneSoundData_SO")]

public class SceneSoundData_SO : ScriptableObject
{
   public List<SceneSoundDetial> sceneSoundsList;
    public SceneSoundDetial GetSceneSoundDetial(string sceneName)
    {
        return sceneSoundsList.Find(n => n.sceneName == sceneName);
    }
}
[System.Serializable]
public class SceneSoundDetial
{
    public string sceneName;
    public SoundName musicName;
    public SoundName ambientName;
}
