using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public static Map Instance;

    public enum Direction { Up, Down, Left, Right};
    public Transform[] WalkableObjects;

    private List<Vector2> territory = new List<Vector2>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < WalkableObjects.Length; i++)
        {
            territory.Add(WalkableObjects[i].transform.position);
        }
    }

    public void Move(Transform target,Direction direction)
    {
        Vector2 wantedPosition = target.position;
        switch (direction)
        {
            case Direction.Up:
                wantedPosition += Vector2.up;
                break;
            case Direction.Down:
                wantedPosition += Vector2.down;
                break;
            case Direction.Left:
                wantedPosition += Vector2.left;
                break;
            case Direction.Right:
                wantedPosition += Vector2.right;
                break;
        }
        if (Walkable(wantedPosition))
        {
            target.position = wantedPosition;
        }
    }

    private bool Walkable(Vector2 position)
    {
        bool canWalk = false;
        for (int i = 0; i < territory.Count; i++)
        {
            if (territory[i] == position)
            {
                canWalk = true;
                break;
            }
        }
        return canWalk;
    }
}
