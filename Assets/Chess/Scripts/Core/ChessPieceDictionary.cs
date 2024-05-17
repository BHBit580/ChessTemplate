using System.Collections.Generic;
using UnityEngine;
namespace Chess.Scripts.Core
{
    /* This class will keep a track of the chess pieces and their positions on the board.
   All the enemy as well as player chess pieces and their positions will be stored in a dictionary of this class
  Till now only "initial" positions of the chess pieces are stored in this dictionary */
    public class ChessPieceDictionary : MonoBehaviour
    {
        public Dictionary<Vector2Int, string> ChessPiecePositionMap = new Dictionary<Vector2Int, string>(); //Storing coordinate and name of the chess piece

        private void Start()
        {
            foreach (Transform child in transform)
            {
                Vector2Int currentPieceCoordinate =
                    new Vector2Int(child.GetComponent<ChessPlayerPlacementHandler>().row,
                        child.GetComponent<ChessPlayerPlacementHandler>().column);
                ChessPiecePositionMap.Add(currentPieceCoordinate, child.name);
            }
        }
    }
}
