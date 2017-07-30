using UnityEngine;

public class Rock : MonoBehaviour, IDamagable {

    Map map;

    private void Start()
    {
        map = Map.Instance;
    }

    public void Damage()
    {
        map.RemoveObstacle(transform);
        Destroy(gameObject);
    }
}
