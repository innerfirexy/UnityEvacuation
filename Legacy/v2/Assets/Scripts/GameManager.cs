using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;
    private Maze mazeInstance;

    public Player playerPrefab;
    private Player playerInstance;

	// Use this for initialization
	private void Start () {
        BeginGame();
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
        }
	}

    //private IEnumerator BeginGame()
    //{
    //    //Camera.main.clearFlags = CameraClearFlags.Skybox;
    //    //Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
    //    mazeInstance = Instantiate(mazePrefab) as Maze;
    //    yield return StartCoroutine(mazeInstance.Generate());
    //    playerInstance = Instantiate(playerPrefab) as Player;
    //    //playerInstance.TeleportToCell(
    //    //  mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    //    playerInstance.TeleportToCell(
    //        mazeInstance.GetCell(mazeInstance.CenterCoordinates));
    //    //Camera.main.clearFlags = CameraClearFlags.Depth;
    //    //Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    //    //Color ambientColor = new Color();
    //    //ambientColor.r = 0.5f;
    //    //ambientColor.g = 0.5f;
    //    //ambientColor.b = 0.5f;
    //    //RenderSettings.ambientLight = ambientColor;
    //}

    private void BeginGame() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
        //playerInstance = Instantiate(playerPrefab) as Player;
        //playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }

    private void RestartGame() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null) {
            Destroy(playerInstance.gameObject);
        }
        BeginGame();
    }
}
