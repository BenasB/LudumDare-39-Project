using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Material MineMaterial;
    public Color Dark;
    public int StartBatteries;
    public Text BatteryText;

    public GameObject CompletionPanel;
    public GameObject RetryPanel;

    public AudioClip BatteryPickupClip;
    public AudioClip GoldPickupClip;
    public AudioClip DeathClip;

    int batteryCount;
    int goldCount;
    int levelGoldCount;

    float sfxVolume;
    AudioSource source;
    Coroutine lightCoroutine;
    Map map;

    public bool Won { get; set; }
    public bool Dead { get; set; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        MineMaterial.color = Color.white;
    }

    private void Start()
    {
        sfxVolume = PlayerPrefs.GetFloat("SFX");
        map = Map.Instance;
        source = GetComponent<AudioSource>();
        levelGoldCount = map.GetLevelGold();
        batteryCount = StartBatteries;
        BatteryText.text = "x" + batteryCount.ToString();
        CompletionPanel.SetActive(false);
        RetryPanel.SetActive(false);
    }

    public void AddGold()
    {
        goldCount++;
        PlayClip(GoldPickupClip);
        if (goldCount == levelGoldCount) {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            CompletionPanel.SetActive(true);
            Debug.Log("Completed level!");
            Won = true;
        }
    }

    public void AddBatteries(int amount)
    {
        batteryCount += amount;
        BatteryText.text = "x" + batteryCount.ToString();
        PlayClip(BatteryPickupClip);
        if (batteryCount > 0)
        {
            if (lightCoroutine != null)
                StopCoroutine(lightCoroutine);
            lightCoroutine = StartCoroutine(Lighten());
        }
    }

    public void RemoveBattery()
    {
        if (batteryCount > 0)
        {
            batteryCount--;
            BatteryText.text = "x" + batteryCount.ToString();
            if (batteryCount == 0)
            {
                if (lightCoroutine != null)
                    StopCoroutine(lightCoroutine);
                lightCoroutine = StartCoroutine(Darken());
            }
        }
    }

    public void Die()
    {
        PlayClip(DeathClip);
        RetryPanel.SetActive(true);
        Debug.Log("You are dead");
        Dead = true;
    }

    public float SFXVolume()
    {
        return sfxVolume;
    }

    private void OnApplicationQuit()
    {
        MineMaterial.color = Color.white;
    }

    IEnumerator Darken()
    {
        float speed = 7f;
        do
        {
            Vector3 newColor = Vector3.MoveTowards(new Vector3(MineMaterial.color.r, MineMaterial.color.g, MineMaterial.color.b), new Vector3(Dark.r,Dark.g, Dark.b), speed*Time.deltaTime);
            MineMaterial.color = new Vector4(newColor.x, newColor.y, newColor.z, 1.0f);
            yield return new WaitForEndOfFrame();
        } while (MineMaterial.color != Dark);
    }

    IEnumerator Lighten()
    {
        float speed = 7f;
        do
        {
            Vector3 newColor = Vector3.MoveTowards(new Vector3(MineMaterial.color.r, MineMaterial.color.g, MineMaterial.color.b), new Vector3(Color.white.r, Color.white.g, Color.white.b), speed * Time.deltaTime);
            MineMaterial.color = new Vector4(newColor.x, newColor.y, newColor.z, 1.0f);
            yield return new WaitForEndOfFrame();
        } while (MineMaterial.color != Dark);
    }

    public void ResetMaterialColor()
    {
        MineMaterial.color = Color.white;
    }

    private void PlayClip(AudioClip clip)
    {
        source.clip = clip;
        source.volume = SFXVolume();
        source.Play();
    }
}
