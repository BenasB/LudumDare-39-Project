using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public static Map Instance;

    public enum Direction { Up, Down, Left, Right};

    private List<Transform> walkableObjects = new List<Transform>();
    private List<Transform> obstacles = new List<Transform>();
    private List<Transform> enemies = new List<Transform>();
    private Vector3 playerPosition;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //Find objects
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Walkable"))
            walkableObjects.Add(obj.GetComponent<Transform>());

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Obstacle"))
            obstacles.Add(obj.GetComponent<Transform>());

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            enemies.Add(obj.GetComponent<Transform>());
    }

    public void Hit(Vector3 position)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].position == position + Vector3.up)
            {
                IDamagable top = obstacles[i].GetComponent<IDamagable>();
                if (top != null)
                    top.Damage();
            }
            if (obstacles[i].position == position + Vector3.down)
            {
                IDamagable down = obstacles[i].GetComponent<IDamagable>();
                if (down != null)
                    down.Damage();
            }
            if (obstacles[i].position == position + Vector3.left)
            {
                IDamagable left = obstacles[i].GetComponent<IDamagable>();
                if (left != null)
                    left.Damage();
            }
            if (obstacles[i].position == position + Vector3.right)
            {
                IDamagable right = obstacles[i].GetComponent<IDamagable>();
                if (right != null)
                    right.Damage();
            }
        }

        //Move all enemies towards the player
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Enemy>().MoveTowardsTarget(playerPosition);
        }
        if (IsPlayerOnEnemy())
            Debug.Log("You are dead.");
    }

    public void Move(Transform target,Direction direction, bool player)
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
            if (player)
                playerPosition = target.position;
            if (IsPlayerOnEnemy())
            {
                Debug.Log("You are dead.");
                return;
            }
        }
        if (player)
        {
            PickupItem(playerPosition);

            //Move all enemies towards the player
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<Enemy>().MoveTowardsTarget(playerPosition);
            }
            if (IsPlayerOnEnemy())
                Debug.Log("You are dead.");
        }
    }

    public void Move(Transform target, Vector3 position)
    {
        target.position = position;
    }

    public void RemoveObstacle(Transform transform)
    {
        obstacles.Remove(transform);
    }

    public void AddObstacle(Transform transform)
    {
        obstacles.Add(transform);
    }

    private bool Walkable(Vector3 position)
    {
        bool canWalk = false;
        for (int i = 0; i < walkableObjects.Count; i++)
        {
            if (walkableObjects[i].position == position)
            {
                canWalk = true;
                break;
            }
        }
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].position == position && obstacles[i].GetComponent<IPickupable>() == null)
            {
                canWalk = false;
                break;
            }
        }
        return canWalk;
    }

    private void PickupItem(Vector3 position)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].position == position)
            {
                IPickupable item = obstacles[i].GetComponent<IPickupable>();
                if (item != null)
                    item.Pickup();
            }
        }
    }

    private bool IsPlayerOnEnemy()
    {
        bool isPlayerOnEnemy = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (playerPosition == enemies[i].position)
            {
                isPlayerOnEnemy = true;
                break;
            }
        }
        return isPlayerOnEnemy;
    }

    public IList<Vector3> GetWalkableNodes(Vector3 currentNode)
    {
        IList<Vector3> listToReturn = new List<Vector3>();
        if (Walkable(currentNode + Vector3.up))
            listToReturn.Add(currentNode + Vector3.up);
        if (Walkable(currentNode + Vector3.down))
            listToReturn.Add(currentNode + Vector3.down);
        if (Walkable(currentNode + Vector3.right))
            listToReturn.Add(currentNode + Vector3.right);
        if (Walkable(currentNode + Vector3.left))
            listToReturn.Add(currentNode + Vector3.left);

        return listToReturn;
    }
}
