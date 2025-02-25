using UnityEngine;
public class PlayerSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource BackGroundVolume;
    public void Awake()
    {
        BackGroundVolume.ignoreListenerPause = true;
    }
    public void Start()
    {
        if (GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter)
        {
            BackGroundVolume.volume = GameContentReaderAndSetter.Instance.GameSoundVolumeGetterAndSetter;
            BackGroundVolume.Play();
        }
    }
    public void Update()
    {
        //if(GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter)
        //{
        //    BackGroundVolume.volume=GameContentReaderAndSetter.Instance.GameSoundVolumeGetterAndSetter;
        //    BackGroundVolume.Play();
        //}
        //else
        //{
        //    BackGroundVolume.Stop();
        //}
        if (GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter)
        {
            float newVolume = GameContentReaderAndSetter.Instance.GameSoundVolumeGetterAndSetter;
            BackGroundVolume.volume = newVolume;
            if (!BackGroundVolume.isPlaying)
            {
                BackGroundVolume.Play();
            }
        }
        else
        { 
            if (BackGroundVolume.isPlaying)
            {
                BackGroundVolume.Stop();
            }
        }
    }
}
