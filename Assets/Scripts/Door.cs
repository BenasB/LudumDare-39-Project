using UnityEngine;

public class Door : MonoBehaviour {

    Map mp;

    private void Start()
    {
        mp = Map.Instance;
    }

    public void Open()
    {
        mp.RemoveObstacle(transform);
        GetComponent<Animator>().SetBool("open", true);
    }
}
