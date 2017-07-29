using UnityEngine;

public class GoldNugget : MonoBehaviour, IPickupable
{
    Map map;

    private void Start()
    {
        map = Map.Instance;
    }

    public void Pickup()
    {
        map.RemoveObstacle(transform);
        Destroy(gameObject);
    }
}
