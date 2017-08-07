using UnityEngine;

public class GoldNugget : MonoBehaviour, IInteractable
{
    Map map;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        map = Map.Instance;
    }

    public void Interact()
    {
        map.RemoveObstacle(transform);
        gm.AddGold();
        Destroy(gameObject);
    }
}
