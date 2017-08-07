using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Map map;
    IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();
    IList<Vector3> path;

    private void Start()
    {
        map = Map.Instance;
    }

    public void MoveTowardsTarget(Vector3 target)
    {
        IList<Vector3> path = new List<Vector3>();
        Vector3 goal = FindShortestPath(target);
        Vector3 curr = goal;
        while (curr != transform.position)
        {
            path.Add(curr);
            curr = nodeParents[curr];
        }
        if (path.Count > 0)
            map.Move(transform, path[path.Count - 1]);
    }

    Vector3 FindShortestPath(Vector3 target)
    {
        nodeParents = new Dictionary<Vector3, Vector3>();
        Vector3 startPosition = transform.position;
        Queue<Vector3> queue = new Queue<Vector3>();
        HashSet<Vector3> exploredNodes = new HashSet<Vector3>();
        queue.Enqueue(startPosition);

        while (queue.Count != 0)
        {
            Vector3 currentNode = queue.Dequeue();
            if (currentNode == target)
            {
                return currentNode;
            }

            IList<Vector3> nodes = map.GetWalkableNodes(currentNode);

            foreach (Vector3 node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {
                    //Mark the node as explored
                    exploredNodes.Add(node);

                    //Store a reference to the previous node
                    nodeParents.Add(node, currentNode);

                    //Add this to the queue of nodes to examine
                    queue.Enqueue(node);
                }
            }
        }

        return startPosition;
    }
}
