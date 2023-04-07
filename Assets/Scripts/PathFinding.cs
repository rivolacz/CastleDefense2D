using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Project
{
    public class PathFinding : MonoBehaviour
    {
        private static Tilemap tilemap;
        private static LayerMask obstacleMask;
        private static Dictionary<Vector2Int, Node> pathNodes = new();
        private static List<Node> openList = new List<Node>();
        private static HashSet<Node> closedList = new HashSet<Node>();
        [SerializeField]
        private Tilemap map;
        [SerializeField]
        private LayerMask layerMask;

        private void Awake()
        {
            tilemap = map;
            obstacleMask = layerMask;
            BuildNodes();
        }

        private static void BuildNodes()
        {
            InitNodes();
            FillNodesWithNeighbours();
            openList = new List<Node>(pathNodes.Count);
            closedList = new HashSet<Node>(pathNodes.Count);
        }

        private static void InitNodes()
        {
            var bounds = tilemap.cellBounds;
            pathNodes.Clear();
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    var collider = Physics2D.OverlapBox(new Vector2(tilePosition.x, tilePosition.y), tilemap.cellSize, 0, obstacleMask);
                    bool isObstacle = collider != null;
                    pathNodes.Add(new Vector2Int(x, y), new Node(tilePosition, isObstacle));
                }
            }
        }

        private static void FillNodesWithNeighbours()
        {
            var bounds = tilemap.cellBounds;
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    var position = new Vector2Int(x, y);
                    pathNodes[position].neighbors = GetNeighbors(x, y);
                }
            }
        }

        static List<Node> GetNeighbors(int x, int y)
        {
            List<Node> neighbors = new List<Node>();
            AddNeighbour(neighbors, x - 1, y);
            AddNeighbour(neighbors, x + 1, y);
            AddNeighbour(neighbors, x, y - 1);
            AddNeighbour(neighbors, x, y + 1);
            //Add diagonal 
            AddNeighbour(neighbors, x - 1, y - 1);
            AddNeighbour(neighbors, x - 1, y + 1);
            AddNeighbour(neighbors, x + 1, y - 1);
            AddNeighbour(neighbors, x + 1, y + 1);
            return neighbors;
        }

        private static void AddNeighbour(List<Node> neighbors, int x, int y)
        {
            var position = new Vector2Int(x, y);
            if (!pathNodes.ContainsKey(position)) return;
            var node = pathNodes[position];
            if (node != null)
            {
                neighbors.Add(node);
            }
        }
        public static List<Node> FindPath(Vector3 start, Vector3 end)
        {
            if(tilemap == null)
            {
                Debug.LogWarning("Tilemap is null");
                return new List<Node>();
            }
            var position = tilemap.WorldToCell(start);
            Vector2Int startInt = (Vector2Int)position;
            position = tilemap.WorldToCell(end);
            Vector2Int endInt = (Vector2Int)position;
            var path = FindPath(startInt, endInt);
            return path;
        }


        public static List<Node> FindPath(Vector2Int start, Vector2Int end)
        {
            if (!pathNodes.ContainsKey(start) || !pathNodes.ContainsKey(end))
            {
                Debug.LogError($"Tilemap doesnt have {start} or {end}");
                return null;
            }
            openList.Clear();
            closedList.Clear();

            Node startNode = pathNodes[start];
            Node endNode = pathNodes[end];
            if (endNode.isObstacle)
            {
                Debug.LogWarning("End node is obstacle");
                Node nearestNode = FindNearestUnobstructedNode(endNode);
                if (nearestNode == null)
                {
                    return new List<Node>();
                }       
            }
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    Node openListNode = openList[i];
                    int openListFCost = openListNode.FCost;
                    int currentNodeFCost = currentNode.FCost;
                    if (openListFCost < currentNodeFCost ||
                        openListFCost == currentNodeFCost &&
                        openListNode.HCost < currentNode.HCost)
                    {
                        currentNode = openListNode;
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNode)
                {
                    // Found the path
                    return RetracePath(startNode, endNode);
                }

                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (closedList.Contains(neighbor)) continue;

                    int newCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                    bool neighborInOpenList = openList.Contains(neighbor);
                    if (newCostToNeighbor < neighbor.GCost || !neighborInOpenList)
                    {
                        neighbor.GCost = newCostToNeighbor;
                        neighbor.HCost = GetDistanceChebyshev(neighbor, endNode);
                        neighbor.Parent = currentNode;

                        if (!neighborInOpenList)
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            Debug.LogWarning($"Couldnt find a path between {start} to {end}");
            // Couldn't find a path
            return null;
        }


        static int GetDistance(Node nodeA, Node nodeB)
        {
            float distance = Vector3.Distance(nodeA.position, nodeB.position);
            return Mathf.RoundToInt(distance * 10);         
        }


        //http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
        static int GetDistanceChebyshev(Node nodeA, Node nodeB)
        {
            var dx = Mathf.Abs(nodeA.position.x - nodeB.position.x);
            var dy = Mathf.Abs(nodeA.position.y - nodeB.position.y);
            const int horizontalMovementCost = 10;
            const int diagonalMovementCost = 14;
            return horizontalMovementCost * (dx + dy) + (diagonalMovementCost - 2 * horizontalMovementCost) * Mathf.Min(dx, dy);
        }


        private static List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }            
            path.Reverse();
            return path;
        }

        public static void AddObstacle(Vector3 obstaclePosition)
        {
            Vector2Int newPosition = (Vector2Int)tilemap.WorldToCell(obstaclePosition);
            if (!pathNodes.ContainsKey(newPosition)) return;
            pathNodes[newPosition].isObstacle = true;
        }

        public static void RemoveObstacle(Vector3 obstaclePosition)
        {
            Vector2Int newPosition = (Vector2Int)tilemap.WorldToCell(obstaclePosition);
            if (!pathNodes.ContainsKey(newPosition)) return;
            pathNodes[newPosition].isObstacle = false;
        }

        public static void RefreshTiles()
        {
            BuildNodes();
        }

        public static Node FindNearestUnobstructedNode(Node startNode)
        {
            Queue<Node> queue = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();
            queue.Enqueue(startNode);
            visited.Add(startNode);
            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();

                if (!currentNode.isObstacle)
                {
                    return currentNode;
                }
                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
            // No unobstructed nodes found
            return null;
        }
    }
}
