using UnityEngine;
using UnityEngine.UI;

public class BackGroundMusicAndVibration : MonoBehaviour
{
    [SerializeField]
    private Sprite ActiveVolume;
    [SerializeField]
    private Sprite MuteVolume;
    [SerializeField]
    private GameObject VolumeButton;
    [SerializeField]
    private Slider SoundSlider;
    [SerializeField]
    private Sprite VibrationOn;
    [SerializeField]
    private Sprite VibrationOff;
    [SerializeField]
    private GameObject VibrationButton;
    public void Start()
    {
        if (GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter)
        {
            Image Volumesprite = VolumeButton.GetComponent<Image>();
            Volumesprite.sprite = ActiveVolume;
            SoundSlider.interactable = true;
            SoundSlider.value = GameContentReaderAndSetter.Instance.GameSoundVolumeGetterAndSetter;
        }
        else
        {
            Image Volumesprite = VolumeButton.GetComponent<Image>();
            Volumesprite.sprite = MuteVolume;
            SoundSlider.interactable = false;
        }
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Image VibrationSprite= VibrationButton.GetComponent<Image>();
            VibrationSprite.sprite = VibrationOn;
        }
        else
        {
            Image VibrationSprite = VibrationButton.GetComponent<Image>();
            VibrationSprite.sprite = VibrationOff;
        }
    }
    public void VolumeButtonFunction()
    {
        GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter = !GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter;
        if (GameContentReaderAndSetter.Instance.GameSoundGetterAndSetter)
        {
            Image Volumesprite=VolumeButton.GetComponent<Image>();
            Volumesprite.sprite = ActiveVolume;
            SoundSlider.interactable=true;
            SoundSlider.value = GameContentReaderAndSetter.Instance.GameSoundVolumeGetterAndSetter;
        }
        else
        {
            Image Volumesprite = VolumeButton.GetComponent<Image>();
            Volumesprite.sprite = MuteVolume;
            SoundSlider.interactable=false;
        }
    } 
    public void VolumeSliderValueChange()
    {
        GameContentReaderAndSetter.Instance.GameSoundVolumeGetterAndSetter= SoundSlider.value;
    }
    public void SetVibrationOnOrOff()
    {
        GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter = !GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter;
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Image VibrationSprite = VibrationButton.GetComponent<Image>();
            VibrationSprite.sprite = VibrationOn;
        }
        else
        {
            Image VibrationSprite = VibrationButton.GetComponent<Image>();
            VibrationSprite.sprite = VibrationOff;
        }
    }
}//class
