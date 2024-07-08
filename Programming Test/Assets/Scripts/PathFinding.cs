using System.Collections.Generic;
using UnityEngine;

//Pathfinding script using Dijkstras's Algorithm
public class PathFinding
{
    private GameObject[] nodes;

    public List<Transform> FindShortestPath(Transform start, Transform end)
    {

        nodes = GameObject.FindGameObjectsWithTag("node");

        List<Transform> result = new List<Transform>();
        Transform node = DijkstrasAlgorithm(start, end);

        // While there's still previous node, we will continue.
        while (node != null)
        {
            result.Add(node);
            TileInfo currentNode = node.GetComponent<TileInfo>();
            node = currentNode.GetParentNode();
        }

        // Reverse the list so that it will be from start to end.
        result.Reverse();
        if (result[0] == end)
        {
            return null;
        }
        return result;
    }

    private Transform DijkstrasAlgorithm(Transform start, Transform end)
    {
        // Nodes that are unexplored
        List<Transform> unexplored = new List<Transform>();

        // We add all the nodes we found into unexplored.
        foreach (GameObject obj in nodes)
        {
            TileInfo n = obj.GetComponent<TileInfo>();
            if (n.GetisWalkable())
            {
                n.ResetNode();
                unexplored.Add(obj.transform);
            }
        }

        // Set the starting node weight to 0;
        TileInfo startNode = start.GetComponent<TileInfo>();
        startNode.SetWeight(0);

        while (unexplored.Count > 0)
        {
            // Sort the explored by their weight in ascending order.
            unexplored.Sort((x, y) => x.GetComponent<TileInfo>().GetWeight().CompareTo(y.GetComponent<TileInfo>().GetWeight()));

            // Get the lowest weight in unexplored.
            Transform current = unexplored[0];

            //Remove the node, since we are exploring it now.
            unexplored.Remove(current);

            TileInfo currentNode = current.GetComponent<TileInfo>();
            List<Transform> neighbours = currentNode.GetNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                TileInfo node = neighNode.GetComponent<TileInfo>();

                // We want to avoid those that had been explored and is not walkable.
                if (unexplored.Contains(neighNode) && node.GetisWalkable())
                {
                    // Get the distance of the object.
                    float distance = Vector3.Distance(neighNode.position, current.position);
                    distance = currentNode.GetWeight() + distance;

                    // If the added distance is less than the current weight.
                    if (distance < node.GetWeight())
                    {
                        // We update the new distance as weight and update the new path now.
                        node.SetWeight(distance);
                        node.SetParentNode(current);
                    }
                }
            }

        }

        return end;
    }
}
