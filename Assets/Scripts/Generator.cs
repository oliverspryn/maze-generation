using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
	#region Fields

	private Cell[,] Cells;
	private Cell Current;
	private Stack<Cell> Stack;
	private int Visited;
	private int Total;

	public float Height {
		get {
			//Create a wall to get its parameters
			GameObject basis = Instantiate(Wall) as GameObject;
			Vector3 size = basis.transform.localScale;
			Destroy(basis);

			return size.y * Y + size.z * (Y - 1);
		}
	}

	public float Width {
		get {
		//Create a wall to get its parameters
			GameObject basis = Instantiate(Wall) as GameObject;
			Vector3 size = basis.transform.localScale;
			Destroy(basis);

			return size.x * X + size.z * (X - 1);
		}
	}

/// <summary>
/// A reference to a wall prefab.
/// </summary>
	public GameObject Wall;

/// <summary>
/// The number of cells in the maze, in the X-direction.
/// </summary>
	public int X = 10;

/// <summary>
/// The number of cells in the maze, in the Y-direction.
/// </summary>
	public int Y = 10;

	#endregion

	public void Start() {
	//Create the new cells
		Cells = new Cell[X, Y];

		for(int i = 0; i < X; ++i) {
			for(int j = 0; j < Y; ++j) {
				Cells[i, j] = new Cell(new IVector2(i, j));
			}
		}

	/**
	 * Build the maze here... 
	 * 
	 * For pseudocode lines 6 and 9, use the helper functions GetUnvisitedNeighbor() and
	 * DestroyWalls(ref WallA, ref WallB), respeectively.
	*/
		
	//Construct the walls in the game
		ConstructWalls();
	}

	#region Helper Methods
	
	private void ConstructWalls() {
	//Create a wall to get its parameters
		GameObject basis = Instantiate(Wall) as GameObject;
		Vector3 position = new Vector3(0.0f, basis.transform.localScale.y / 2.0f, 0.0f);
		Vector3 size = basis.transform.localScale;
		Destroy(basis);

	//Precalculate the locations of each row and column of walls
		int halfX = (int)Math.Floor((X - 1) / 2.0f);
		int halfY = (int)Math.Floor((Y - 1) / 2.0f);

		List<float> XPos = new List<float>();
		List<float> YPos = new List<float>();

		if(X % 2 == 0) { //Even
			for(int i = -1 * halfX; i <= halfX; ++i) {
				XPos.Add(i * size.x);
			}
		} else {
			for(int i = -1 * (halfX - 1); i <= halfX; ++i) {
				XPos.Add(i * size.x - (size.x / 2.0f));
			}
		}

		if(Y % 2 == 0) { //Even
			for(int i = -1 * halfY; i <= halfY; ++i) {
				YPos.Add(i * size.y);
			}
		} else {
			for(int i = -1 * (halfY - 1); i <= halfY; ++i) {
				YPos.Add(i * size.y - (size.y / 2.0f));
			}
		}

	//Create the walls
		for(int i = 0; i < X - 1; ++i) {
			for(int j = 0; j < Y - 1; ++j) {
				if(Cells[i, j].North && j != Y - 2) { //NORTH
					GameObject wall = Instantiate(Wall) as GameObject;
					position.x = XPos[i];
					position.z = YPos[j] + (size.x / 2.0f);

					wall.transform.position = position;
				}

				if(Cells[i, j].East && i != X - 2) { //EAST
					GameObject wall = Instantiate(Wall) as GameObject;
					position.x = XPos[i] + (size.y / 2.0f);
					position.z = YPos[j];

					wall.transform.position = position;
					wall.transform.Rotate(wall.transform.rotation.x, 90.0f, wall.transform.rotation.z);
				}
			}
		}
	}

	private void DestroyWalls(ref Cell cellOne, ref Cell cellTwo) {
		IVector2 N = new IVector2(cellOne.Position.X, cellOne.Position.Y); N.Y++;
		IVector2 S = new IVector2(cellOne.Position.X, cellOne.Position.Y); S.Y--;
		IVector2 E = new IVector2(cellOne.Position.X, cellOne.Position.Y); E.X++;
		IVector2 W = new IVector2(cellOne.Position.X, cellOne.Position.Y); W.X--;

	//CellTwo is North of CellOne
		if(cellTwo.Position == N)
			cellOne.North = cellTwo.South = false;

	//CellTwo is South of CellOne
		if(cellTwo.Position == S)
			cellOne.South = cellTwo.North = false;

	//CellTwo is East of CellOne
		if(cellTwo.Position == E)
			cellOne.East = cellTwo.West = false;

	//CellTwo is West of CellOne
		if(cellTwo.Position == W)
			cellOne.West = cellTwo.East = false;

		cellOne.Visited = cellTwo.Visited = true;
	}

	private List<Cell> GetUnvisitedNeighbor() {
		List<Cell> ret = new List<Cell>();

	//Has North been visited?
		if(Current.North && Current.Position.Y + 1 < Y && !Cells[Current.Position.X, Current.Position.Y + 1].Visited) // North == +Y
			ret.Add(Cells[Current.Position.X, Current.Position.Y + 1]);

	//Has South been visited?
		if(Current.South && Current.Position.Y - 1 >= 0 && !Cells[Current.Position.X, Current.Position.Y - 1].Visited) // South == -Y
			ret.Add(Cells[Current.Position.X, Current.Position.Y - 1]);

	//Has East been visited?
		if(Current.East && Current.Position.X + 1 < X && !Cells[Current.Position.X + 1, Current.Position.Y].Visited) // East == +X
			ret.Add(Cells[Current.Position.X + 1, Current.Position.Y]);

	//Has West been visited?
		if(Current.West && Current.Position.X - 1 >= 0 && !Cells[Current.Position.X - 1, Current.Position.Y].Visited) // West == -X
			ret.Add(Cells[Current.Position.X - 1, Current.Position.Y]);

		return ret;
	}

	#endregion
}