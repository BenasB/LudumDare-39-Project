using UnityEngine;

public class Player : MonoBehaviour {

    Map map;
    Vector2 input;

    private void Start()
    {
        map = Map.Instance;
        if (!map)
            Debug.LogError("Can't find the instance of the map script");

        map.SetPlayerPosition(transform.position);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Up"))
            map.Move(transform, Map.Direction.Up, true);
        if (Input.GetButtonDown("Down"))
            map.Move(transform, Map.Direction.Down, true);
        if (Input.GetButtonDown("Left"))
            map.Move(transform, Map.Direction.Left, true);
        if (Input.GetButtonDown("Right"))
            map.Move(transform, Map.Direction.Right, true);

        if (Input.GetButtonDown("Action"))
            map.Hit(transform.position);
    }
}
