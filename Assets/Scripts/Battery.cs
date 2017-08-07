using UnityEngine;

public class Battery : MonoBehaviour, IInteractable {

    public int Batteries = 1;
    GameManager gm;
    Map map;

    private void Start()
    {
        gm = GameManager.Instance;
        map = Map.Instance;
    }

    public void Interact()
    {
        gm.AddBatteries(Batteries);
        map.RemoveObstacle(transform);
        Destroy(gameObject);
    }
}
