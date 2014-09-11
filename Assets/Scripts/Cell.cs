/// <summary>
/// An integer-type vector for holding the X and Y position of a 
/// <c>Cell</c> in a maze.
/// </summary>
public class IVector2 {
/// <summary>
/// Create an <c>IVector2</c> with pre-assigned values.
/// </summary>
/// 
/// <param name="x">The X position of the <c>Cell</c> in the maze</param>
/// <param name="y">The Y position of the <c>Cell</c> in the maze</param>
	public IVector2(int x, int y) {
		X = x;
		Y = y;
	}

/// <summary>
/// The X position of the <c>Cell</c> in the maze.
/// </summary>
	public int X { get; set; }

/// <summary>
/// The Y position of the <c>Cell</c> in the maze.
/// </summary>
	public int Y { get; set; }

	#region Overloaded Methods

/// <summary>
/// Overloaded equals (!=) operator.
/// </summary>
/// 
/// <param name="left"><c>IVector2</c> to the left of the != operator</param>
/// <param name="right"><c>IVector2</c> to the right of the != operator</param>
/// <returns>Whether or not the two <c>IVector2</c> objects are unequal</returns>
	public static bool operator !=(IVector2 left, IVector2 right) {
		return left.X != right.X || left.Y != right.Y;
	}

/// <summary>
/// Overloaded equals (==) operator.
/// </summary>
/// 
/// <param name="left"><c>IVector2</c> to the left of the == operator</param>
/// <param name="right"><c>IVector2</c> to the right of the == operator</param>
/// <returns>Whether or not the two <c>IVector2</c> objects are equal</returns>
	public static bool operator ==(IVector2 left, IVector2 right) {
		return left.X == right.X && left.Y == right.Y;
	}

/// <summary>
/// Overloaded Equals() function.
/// </summary>
/// 
/// <param name="left">The <c>IVector2</c> object to compare</param>
/// <returns>Whether or not the two <c>IVector2</c> objects are equal</returns>
	public override bool Equals(object compare) {
		IVector2 obj = compare as IVector2;

		return X == obj.X && Y == obj.Y;
	}

/// <summary>
/// Overloaded GetHashCode() function.
/// </summary>
/// 
/// <returns>The hash code of the class instance</returns>
	public override int GetHashCode() {
		return base.GetHashCode();
	}

	#endregion
}

/// <summary>
/// A <c>Cell</c> object functions more like a C-style struct,
/// and is merely used to hold information about a particular
/// <c>Cell</c>, such as which walls are up, its position in the
/// grid, and whether or not this <c>Cell</c> has been visited
/// by the generation algorithm.
/// </summary>

public class Cell {
/// <summary>
/// Create a new <c>Cell</c> object, with all of the walls intact
/// and set the cell as unvisited by the generation algorithm.
/// </summary>
    public Cell(IVector2 position) {
        North = South = East = West = true;
		Position = position;
        Visited = false;
    }

/// <summary>
/// Whether or not a particular wall exists (i.e., has not been knocked
/// down to create a larger space for the maze). True = wall exists, and
/// has not been knocked down.
/// </summary>
    public bool North { get; set; }
    public bool South { get; set; }
    public bool East  { get; set; }
    public bool West  { get; set; }

/// <summary>
/// The position of the <c>Cell</c> within the maze.
/// </summary>
	public IVector2 Position { get; set; }

/// <summary>
/// Whether or not this <c>Cell</c> has been visited by the generaion
/// algorithm.
/// </summary>
    public bool Visited { get; set; }
}