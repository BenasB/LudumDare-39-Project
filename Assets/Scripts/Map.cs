using UnityEngine;
using System.Collections.Generic;

public enum Action { Walk, Hit, None }

public class Map : MonoBehaviour {

    public static Map Instance;

    public enum Direction { Up, Down, Left, Right};

    List<Transform> walkableObjects = new List<Transform>();
    List<Transform> obstacles = new List<Transform>();
    List<Transform> enemies = new List<Transform>();
    Vector3 playerPosition;
    GameManager gm;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        //Find objects
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Walkable"))
            walkableObjects.Add(obj.GetComponent<Transform>());

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Obstacle"))
            obstacles.Add(obj.GetComponent<Transform>());

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            enemies.Add(obj.GetComponent<Transform>());
    }

    private void Start()
    {
        gm = GameManager.Instance;  
    }

    public Action Move(Transform target, Direction direction, bool player)
    {
        Action action = Action.None;

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
        if (CanAttack(wantedPosition) != null)
        {
            Debug.Log("Hit an object");
            CanAttack(wantedPosition).Damage();
            action = Action.Hit;

            gm.RemoveBattery();
            MoveEnemies();
        }
        else if (CanWalk(wantedPosition, player))
        {
            target.position = wantedPosition;
            playerPosition = target.position;
            action = Action.Walk;

            gm.RemoveBattery();
            MoveEnemies();
        }
        PickupItem(playerPosition);

        return action;
    }

    public void Move(Transform target, Vector3 position)
    {
        target.position = position;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public void RemoveObstacle(Transform transform)
    {
        obstacles.Remove(transform);
    }

    public void AddObstacle(Transform transform)
    {
        obstacles.Add(transform);
    }

    private void MoveEnemies()
    {
        if (!gm.Won && !gm.Dead)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<Enemy>().MoveTowardsTarget(playerPosition);
            }

        }
        if (IsPlayerOnEnemy())
            gm.Die();
    }

    private IDamagable CanAttack(Vector3 position)
    {
        IDamagable damagableObject = null;
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].position == position && obstacles[i].GetComponent<IDamagable>() != null)
            {
                damagableObject = obstacles[i].GetComponent<IDamagable>();
                break;
            }
        }
        return damagableObject;
    }

    private bool CanWalk(Vector3 position, bool player)
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
        if (player)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].position == position)
                {
                    canWalk = false;
                    break;
                }
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

    public int GetLevelGold()
    {
        int goldCount = 0;
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].GetComponent<Gold>() || obstacles[i].GetComponent<GoldNugget>())
                goldCount++;
        }
        return goldCount;
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
        if (CanWalk(currentNode + Vector3.up, false))
            listToReturn.Add(currentNode + Vector3.up);
        if (CanWalk(currentNode + Vector3.down, false))
            listToReturn.Add(currentNode + Vector3.down);
        if (CanWalk(currentNode + Vector3.right, false))
            listToReturn.Add(currentNode + Vector3.right);
        if (CanWalk(currentNode + Vector3.left, false))
            listToReturn.Add(currentNode + Vector3.left);

        return listToReturn;
    }
}
