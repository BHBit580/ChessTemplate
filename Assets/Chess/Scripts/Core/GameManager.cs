using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class GameManager : MonoBehaviour
    {
        private Dictionary<Vector2Int, Color> _defaultTileColorOfEnemy = new Dictionary<Vector2Int, Color>();   // This is the default tile color dictionary
                                                                                                              // where enemies are present
                                                                                                              // Suppose 1 enemy present in white block and 2 present in blue block
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    GameObject selectedGameObject = hit.collider.gameObject;

                    Vector2Int currentPieceCoordinate =
                        new Vector2Int(selectedGameObject.GetComponent<ChessPlayerPlacementHandler>().row,
                            selectedGameObject.GetComponent<ChessPlayerPlacementHandler>().column);

                    
                    SelectedChessPiece(hit.collider.gameObject.name, currentPieceCoordinate);        //Giving the gameobject name and
                                                                                                     //current coordinate of the selected chess piece function
                                                                                                     //by which our ray got hit
                }
            }
        }

        private void SelectedChessPiece(string chessPieceName, Vector2Int selectedPieceCoordinate)
        {
            List<Vector2Int> possibleMoves = new List<Vector2Int>();
            switch (chessPieceName)
            {
                case "Rook":
                    possibleMoves = ChessPiece.Instance.CalculateRookMoves(selectedPieceCoordinate);
                    break;
                case "Bishop":
                    possibleMoves = ChessPiece.Instance.CalculateBishopMoves(selectedPieceCoordinate);
                    break;
                case "Queen":
                    possibleMoves = ChessPiece.Instance.CalculateQueenMoves(selectedPieceCoordinate);
                    break;
                case "King":
                    possibleMoves = ChessPiece.Instance.CalculateKingMoves(selectedPieceCoordinate);
                    break;
                case "Pawn":
                    possibleMoves = ChessPiece.Instance.CalculatePawnMoves(selectedPieceCoordinate);
                    break;
                case "Knight":
                    possibleMoves = ChessPiece.Instance.CalculateKnightMoves(selectedPieceCoordinate);
                    break;
            }
            
            HighlightTheTiles(possibleMoves);
        }


        private void HighlightTheTiles(List<Vector2Int> moves)
        {
            ChessBoardPlacementHandler.Instance.ClearHighlights();                  //Clear previous highlights
            EnemyTileColorClearHighLights();
            
            foreach (var move in moves)
            {
                if (ChessPiece.Instance.EnemyPresent(move))
                {
                    SpriteRenderer tileSpriteRenderer = ChessBoardPlacementHandler.Instance.GetTile(move.x, move.y)
                        .GetComponent<SpriteRenderer>();
                    _defaultTileColorOfEnemy.Add(move, tileSpriteRenderer.color);                       
                    
                    tileSpriteRenderer.color = Color.red;                            //Highlighting the enemy tile
                }
                else ChessBoardPlacementHandler.Instance.Highlight(move.x, move.y);
            }
        }

        private void EnemyTileColorClearHighLights()
        {
            foreach (var coordinates in _defaultTileColorOfEnemy.Keys.ToList())
            {
                GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(coordinates.x, coordinates.y);
                if (tile != null)
                {
                    tile.GetComponent<SpriteRenderer>().color = _defaultTileColorOfEnemy[coordinates];
                }

                _defaultTileColorOfEnemy.Remove(coordinates);
            }
        }
    }

}
