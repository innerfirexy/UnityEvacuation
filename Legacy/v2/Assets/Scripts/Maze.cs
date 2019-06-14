using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maze : MonoBehaviour {
    public IntVector2 size;
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public float generationStepDelay;
    private MazeCell[,] cells;

    public MazeCell GetCell(IntVector2 coordinates) {
        return cells[coordinates.x, coordinates.z];
    }

    public IntVector2 RandomCoordinates {
        get {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public IntVector2 CenterCoordinates {
        get {
            return new IntVector2(size.x / 2, size.z / 2);
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinates) {
        return coordinates.x >= 0 && coordinates.x < size.x && coordinates.z >= 0 && coordinates.z < size.z;
    }

    public IEnumerator Generate() {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        for (int x = 0; x < size.x; x++) {
            for (int z = 0; z < size.z; z++) {
                yield return delay;
                IntVector2 currentPos = new IntVector2(x, z);
                MazeCell currentCell = GetCell(currentPos);
                if (currentCell == null) {
                    currentCell = CreateCell(currentPos);
                }
                // Create passages and walls
                foreach (MazeDirection direction in System.Enum.GetValues(typeof(MazeDirection))) {
                    IntVector2 coordinates = currentPos + direction.ToIntVector2();
                    if (ContainsCoordinates(coordinates)) {
                        MazeCell neighbor = GetCell(coordinates);
                        if (neighbor == null) {
                            neighbor = CreateCell(coordinates);
                            CreatePassage(currentCell, neighbor, direction);
                        } 
                        else {
                            CreatePassage(currentCell, neighbor, direction);
                        }
                    }
                    else {
                        CreateWall(currentCell, null, direction);
                    }
                }
            }
        }
    }

    // This way of random generation is not good
    public IEnumerator GenerateRandom() {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        IntVector2 coordinates = RandomCoordinates;
        while (ContainsCoordinates(coordinates) && GetCell(coordinates) == null) {
            yield return delay;
            CreateCell(coordinates);
            coordinates += MazeDirections.RandomValue.ToIntVector2();
        }
    }


    private MazeCell CreateCell(IntVector2 coordinates) {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction);
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null) {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }
}
