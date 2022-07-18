using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] Slider slider;
    [SerializeField] AudioClip townClip;
    [SerializeField] AudioClip battleClip;
    [SerializeField] AudioClip victoryClip;
    public static SoundManager instance;
    private new AudioSource audio;
    public float volume = 1;
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
        audio.volume = volume;
    }

    private void Update()
    {
        if (slider == null)
        {
            Object sliderObj = FindObjectOfType<Slider>();
            if (sliderObj != null)
            {
                slider = (Slider) sliderObj;
                Load();
                slider.onValueChanged.AddListener(delegate { ChangeVolume(); });               
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
            audio.volume = slider.value;
            volume = slider.value;
            if (DataPersistenceManager.instance.HasGameData())
            {
                SaveData(DataPersistenceManager.instance.gameData);
            }          
        }
        
    }

    void Load()
    {  
        if (slider != null)
        {
            if (DataPersistenceManager.instance.HasGameData())
            {
                LoadData(DataPersistenceManager.instance.gameData);
            }   
            slider.value = volume;
        }          
    }

    public void LoadData(GameData data)
    {
        volume = data.volume;
    }

    public void SaveData(GameData data)
    {
       data.volume = volume;
       DataPersistenceManager.instance.SaveVolume();
    }
}
