using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour{
    public Tilemap tilemap { get; private set; }     //reference to tile map to draw something onto tilemap
    public Piece activePiece {get; private set;}    //reference to game piece
    public TetrominoData[] tetrominoes;     //define the array of tetromino data so we can customize in the editor
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt Bounds{
        get{ //get position (corner of our board)
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, - this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }


    //spawn pieces on to gameboard
    private void Awake(){
        this.tilemap = GetComponentInChildren<Tilemap>(); //tilemap is a child of board
        this.activePiece = GetComponentInChildren<Piece>();
        //loop through list of data and initialize
        for(int i = 0; i < this.tetrominoes.Length; i++){
            this.tetrominoes[i].Initialize();
        }
    }

    //when our games starts, spawn a piece
    private void Start(){
        SpawnPiece();
    }

    public void SpawnPiece(){ //spawn random pieces
        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];

        //initializing piece then passing in a reference to gameboard, giving it spawn pos, and then the random data
        this.activePiece.Initialize(this, this.spawnPosition, data);
        //set piece on tilemap
        Set(this.activePiece);
    }

    //set the tiles on tilemap
    public void Set(Piece piece){
        for(int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece){
        for(int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position){

        RectInt bounds = this.Bounds;

        for(int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece.cells[i] + position;            
            //is there already a tile occupying the space?
            if(this.tilemap.HasTile(tilePosition)){
                return false;
            }
            //is it out of bounds?
            if(!bounds.Contains((Vector2Int)tilePosition)){
                return false;
            }
        }

        return true;
    }

}
