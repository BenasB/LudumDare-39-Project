using UnityEngine;
using System.Collections.Generic;

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

    public void Hit(Vector3 position)
    {
        if (!gm.Won)
        {
            List<Transform> obstaclesAtThisMoment = new List<Transform>();

            for (int i = 0; i < obstacles.Count; i++)
            {
                obstaclesAtThisMoment.Add(obstacles[i]);
            }

            for (int i = 0; i < obstaclesAtThisMoment.Count; i++)
            {
                if (obstaclesAtThisMoment[i].position == position + Vector3.up)
                {
                    IDamagable top = obstaclesAtThisMoment[i].GetComponent<IDamagable>();
                    if (top != null)
                    {
                        top.Damage();
                        continue;
                    }
                }
                if (obstaclesAtThisMoment[i].position == position + Vector3.down)
                {
                    IDamagable down = obstaclesAtThisMoment[i].GetComponent<IDamagable>();
                    if (down != null)
                    {
                        down.Damage();
                        continue;
                    }
                }
                if (obstaclesAtThisMoment[i].position == position + Vector3.left)
                {
                    IDamagable left = obstaclesAtThisMoment[i].GetComponent<IDamagable>();
                    if (left != null)
                    {
                        left.Damage();
                        continue;
                    }
                }
                if (obstaclesAtThisMoment[i].position == position + Vector3.right)
                {
                    IDamagable right = obstaclesAtThisMoment[i].GetComponent<IDamagable>();
                    if (right != null)
                    {
                        right.Damage();
                        continue;
                    }
                }
            }

            //Move all enemies towards the player
            if (!gm.Won)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<Enemy>().MoveTowardsTarget(playerPosition);
                }
            }
            if (IsPlayerOnEnemy())
                Debug.Log("You are dead.");

            gm.RemoveBattery();
        }
    }

    public void Move(Transform target,Direction direction, bool player)
    {
        if (!gm.Won)
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
                {
                    playerPosition = target.position;
                }
                if (IsPlayerOnEnemy())
                {
                    Debug.Log("You are dead.");
                    return;
                }
            }
            if (player)
            {
                gm.RemoveBattery();
                PickupItem(playerPosition);

                //Move all enemies towards the player
                if (!gm.Won)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].GetComponent<Enemy>().MoveTowardsTarget(playerPosition);
                    }
                    
                }
                if (IsPlayerOnEnemy())
                    Debug.Log("You are dead.");
            }
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
