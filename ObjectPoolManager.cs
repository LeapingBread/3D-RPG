using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject[] objectPoolPerfebs;
    Queue<GameObject> soundQueue = new Queue<GameObject>();
    private void OnEnable()
    {
        EventHandler.InitSoundEffects += OnInitSoundsEffect;
    }
    private void OnDisable()
    {
        EventHandler.InitSoundEffects -= OnInitSoundsEffect;
    }
    void CreatSoundQueue()
    {
        var parent = new GameObject(objectPoolPerfebs[0].name).transform;
        parent.SetParent(transform);
        for(int i =0; i <20 ; i++)
        {
            GameObject newObj = Instantiate(objectPoolPerfebs[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }
    GameObject GetSoundObject()
    {
        if (soundQueue.Count < 2)
            CreatSoundQueue();
        return soundQueue.Dequeue();
    }
    void OnInitSoundsEffect(SoundDetial soundDetial)
    {
        var obj = GetSoundObject();
        obj.GetComponent<Sound>().SetSound(soundDetial);
        obj.SetActive(true);
        StartCoroutine(DisableSoundRoutin(obj,soundDetial.audioClip.length));
    }
    IEnumerator DisableSoundRoutin(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive (false);
        soundQueue.Enqueue(obj);
    }
}
