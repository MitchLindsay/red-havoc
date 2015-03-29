using UnityEngine;

namespace Assets.Code.Entities.Abstract
{
    public abstract class GraphFactory<TGraph> : MonoBehaviour
    {
        public Transform GraphContainer = null;
        public Vector3 GraphPosition = Vector3.zero;
        public string NodeTag = "Node";
        public string GraphTag = "Graph";
        public string GraphName = "New Graph";
        public int GraphWidth = 50;
        public int GraphHeight = 50;

        public virtual void Destroy()
        {
            GameObject[] nodes = GameObject.FindGameObjectsWithTag(NodeTag);
            GameObject[] graphs = GameObject.FindGameObjectsWithTag(GraphTag);

            foreach (GameObject node in nodes)
                DestroyImmediate(node);

            foreach (GameObject graph in graphs)
                DestroyImmediate(graph);
        }

        public virtual void Generate() { }
    }
}