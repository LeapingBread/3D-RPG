using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SoundData_SO",menuName = "SoundData_SO")]
public class SoundData_SO : ScriptableObject
{
    public List<SoundDetial> soundDataList;
    public SoundDetial GetSoundDetial(SoundName soundName)
    {
        return soundDataList.Find(n => n.soundName == soundName);
    }
}
[System.Serializable]
public class SoundDetial
{
    public SoundName soundName;
    public AudioClip audioClip;
    public float volume;
    public float pitch;
}
