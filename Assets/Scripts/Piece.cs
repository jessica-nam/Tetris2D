//In tetris you can only control one piece at a time so we only need one piece instance
//Once that piece gets locked into place, we just reinitialize the same piece but with different data
using UnityEngine;

public class Piece : MonoBehaviour
{
    //properties
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; } //copy of cell array to manipulate
    public Vector3Int position { get; private set; }

    //function to pass in data we want
    //pass in reference to game board to communicate piece changes to gameboard
    public void Initialize(Board board, Vector3Int position, TetrominoData data){
        this.board = board;
        this.position = position;
        this.data = data;

        //if array was never initialized...
        if(this.cells == null){
            this.cells = new Vector3Int[data.cells.Length];
        }
        //copy data from Vector2Int into our new array
        for(int i = 0; i < data.cells.Length; i++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    //player input
    private void Update(){

        this.board.Clear(this);

        if(Input.GetKeyDown(KeyCode.A)){
            Move(Vector2Int.left);
        }else if (Input.GetKeyDown(KeyCode.D)){
            Move(Vector2Int.right);
        }

        if(Input.GetKeyDown(KeyCode.S)){
            Move(Vector2Int.down);
        }

        //hard drop
        if(Input.GetKeyDown(KeyCode.Space)){
            HardDrop();
        }
        
        this.board.Set(this);
    }

    //hard drop
    private void HardDrop(){
        //continously move down until we cant
        while(Move(Vector2Int.down)){
            continue;
        }
    }

    //update position and check position validity
    private bool Move(Vector2Int translation){
        //what is new pos
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        //communicate to gameboard to check valid
        bool valid = this.board.IsValidPosition(this, newPosition);

        if(valid){
            this.position = newPosition;
        }
        return valid;
    }
}
