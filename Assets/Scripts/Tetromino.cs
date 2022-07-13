using UnityEngine;
using UnityEngine.Tilemaps;

//define custom enum - enumerated list of values
public enum Tetromino{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

[System.Serializable]   //custom attribute to display data in editor

//custom data structure to store data for each tetromino
public struct TetrominoData{
    public Tetromino tetromino;
    public Tile tile;

    //define coords that make up the cells of every piece
    //we don't need this to show up in the editor because we have the Data file 
    public Vector2Int[] cells{ get; private set; } //change from c# field to a c# property

    //assign array of coordinates to Vector2Int
    public void Initialize(){
        this.cells = Data.Cells[this.tetromino]; //key
    }
    
}