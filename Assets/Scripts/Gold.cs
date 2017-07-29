using UnityEngine;

public class Gold : MonoBehaviour, IDamagable {

    public GameObject Drop;

    Map map;

    private void Start()
    {
        map = Map.Instance;
    }

    public void Damage()
    {
        GameObject drop = Instantiate(Drop, transform.position, Quaternion.identity);
        map.AddObstacle(drop.transform);
        map.RemoveObstacle(transform);
        Destroy(gameObject);
    }
}
