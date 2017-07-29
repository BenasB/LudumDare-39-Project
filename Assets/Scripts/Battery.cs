using UnityEngine;

public class Battery : MonoBehaviour, IPickupable {

    public int Batteries = 1;
    GameManager gm;
    Map map;

    private void Start()
    {
        gm = GameManager.Instance;
        map = Map.Instance;
    }

    public void Pickup()
    {
        gm.AddBatteries(Batteries);
        map.RemoveObstacle(transform);
        Destroy(gameObject);
    }
}
