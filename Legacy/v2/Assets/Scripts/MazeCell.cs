using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {
    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    public MazeCellEdge GetEdge(MazeDirection direction) {
        return edges[(int)direction];
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
        edges[(int)direction] = edge;
    }

    public void OnPlayerEntered() {
        for (int i = 0; i < edges.Length; i++) {
            edges[i].OnPlayerEntered();
        }
    }

    public void OnPlayerExited() {
        for (int i = 0; i < edges.Length; i++) {
            edges[i].OnPlayerExited();
        }
    }
}
