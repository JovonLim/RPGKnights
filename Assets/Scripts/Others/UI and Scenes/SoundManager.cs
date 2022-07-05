using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] AudioClip townClip;
    [SerializeField] AudioClip battleClip;
    [SerializeField] AudioClip victoryClip;
    public static SoundManager instance;
    private new AudioSource audio;
    bool changedTrack;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("audio"))
        {
            PlayerPrefs.SetFloat("audio", 1);
            Load();
        } else
        {
            Load();
        }
    }

    private void Update()
    {
        if (slider == null)
        {
            Object sliderObj = FindObjectOfType<Slider>();
            if (sliderObj != null)
            {
                slider = (Slider) sliderObj;
                slider.onValueChanged.AddListener(delegate { ChangeVolume(); });
                Load();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex <= 2)
        {
            if (audio.clip != townClip)
            {
                audio.clip = townClip;
                changedTrack = true;
                audio.loop = true;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex <= 16)
        {
            if (audio.clip != battleClip)
            {
                audio.clip = battleClip;
                changedTrack = true;
                audio.loop = true;
            }
            
        } else if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            if (audio.clip != victoryClip)
            {
                audio.clip = victoryClip;
                changedTrack = true;
                audio.loop = false;
            }      
        } else
        {
            if (audio.clip != townClip)
            {
                audio.clip = townClip;
                changedTrack = true;
                audio.loop = true;
            }
        }
        if (changedTrack)
        {
            audio.Play();
            changedTrack = false;
        }
        
        
      
    }

    public void ChangeVolume()
    {
        if (slider != null)
        {
            GetComponent<AudioSource>().volume = slider.value;
            Save();
        }
        
    }

    void Load()
    {
        if (slider != null)
        slider.value = PlayerPrefs.GetFloat("audio");
    }

    private void Save()
    {
        if (slider != null)
        PlayerPrefs.SetFloat("audio", slider.value);
    
    }
}
