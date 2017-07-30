using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Material MineMaterial;
    public Color Dark;
    public int StartBatteries;
    public Text BatteryText;

    int batteryCount;
    int goldCount;
    int levelGoldCount;

    Coroutine lightCoroutine;
    Map map;

    public bool Won { get; set; }

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
        map = Map.Instance;
        levelGoldCount = map.GetLevelGold();
        batteryCount = StartBatteries;
        BatteryText.text = "x" + batteryCount.ToString();
    }

    public void AddGold()
    {
        goldCount++;
        if (goldCount == levelGoldCount) {
            Debug.Log("Completed level!");
            Won = true;
        }
    }

    public void AddBatteries(int amount)
    {
        batteryCount += amount;
        BatteryText.text = "x" + batteryCount.ToString();
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
}
