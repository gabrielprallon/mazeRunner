using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public float size = 2f;
    public float lateral = 1f;

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
                
				mazeCells [r, c] .floor = Instantiate (wall) as GameObject;
                mazeCells[r, c].floor.transform.parent = this.transform;
                mazeCells[r, c].floor.transform.localPosition = new Vector3(r * size, -((size + lateral) / 2f), c * size);
                mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

                mazeCells[r, c].ceiling = Instantiate(wall) as GameObject;
                mazeCells[r, c].ceiling.transform.parent = this.transform;
                mazeCells[r, c].ceiling.transform.localPosition = new Vector3(r * size, ((size + lateral) / 2f), c * size);
                mazeCells[r, c].ceiling.name = "Ceiling " + r + "," + c;
                mazeCells[r, c].ceiling.transform.Rotate(Vector3.right, 90f);

                if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall) as GameObject;
                    mazeCells[r, c].westWall.transform.parent = this.transform;
                    mazeCells[r, c].westWall.transform.localPosition = new Vector3(r * size, 0, (c * size) - (size / 2f));
                    mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells [r, c].eastWall = Instantiate (wall) as GameObject;
                mazeCells[r, c].eastWall.transform.parent = this.transform;
                mazeCells[r, c].eastWall.transform.localPosition = new Vector3(r * size, 0, (c * size) + (size / 2f));
                mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall) as GameObject;
                    mazeCells[r, c].northWall.transform.parent = this.transform;
                    mazeCells[r, c].northWall.transform.localPosition = new Vector3((r * size) - (size / 2f), 0, c * size);
                    mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall) as GameObject;
                mazeCells[r, c].southWall.transform.parent = this.transform;
                mazeCells[r, c].southWall.transform.localPosition = new Vector3((r * size) + (size / 2f), 0, c * size);
                mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
	}
}
