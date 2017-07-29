using UnityEngine;

public class Player : MonoBehaviour {

    Map map;
    Vector2 input;

    private void Start()
    {
        map = Map.Instance;
        if (!map)
            Debug.LogError("Can't find the instance of the map script");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Up"))
            map.Move(transform, Map.Direction.Up);
        if (Input.GetButtonDown("Down"))
            map.Move(transform, Map.Direction.Down);
        if (Input.GetButtonDown("Left"))
            map.Move(transform, Map.Direction.Left);
        if (Input.GetButtonDown("Right"))
            map.Move(transform, Map.Direction.Right);
    }
}
