using UnityEngine;

public class Map : MonoBehaviour {

    public static Map Instance;

    public enum Direction { Up, Down, Left, Right};

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Move(Transform target,Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                target.position += Vector3.up;
                break;
            case Direction.Down:
                target.position += Vector3.down;
                break;
            case Direction.Left:
                target.position += Vector3.left;
                break;
            case Direction.Right:
                target.position += Vector3.right;
                break;
        }
    }
}
