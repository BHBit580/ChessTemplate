using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



namespace Chess.Scripts.Core
{
    public class ChessPiece : GenericSingleton<ChessPiece>
    {
        [SerializeField] private ChessPieceDictionary enemyChessPieceDictionary;
        [SerializeField] private ChessPieceDictionary playerChessPieceDictionary;

        
        

        public bool EnemyPresent(Vector2Int coordinates)
        {
            if (enemyChessPieceDictionary.ChessPiecePositionMap
                .ContainsKey(coordinates)) // Getting the info of enemyPositions from other class
            {
                return true;
            }

            return false;
        }

        private bool IsSquareOccupied(int row, int column) //Know if the square/tile is occupied or not. It does not tell who is occupying it
        {
            GameObject tileChild =
                ChessBoardPlacementHandler.Instance.GetTile(row, column); //Gives us the tile gameobject 

            if (tileChild != null)
            {
                Transform parentTransform = tileChild.transform.parent;
                int rowNumber =
                    Int32.Parse(Regex.Match(parentTransform.gameObject.name, @"\d+").Value) - 1; //Converting the string of the parent gameobject to int
                int colNumber = Int32.Parse(tileChild.name);
                

                if (playerChessPieceDictionary.ChessPiecePositionMap.ContainsKey(new Vector2Int(rowNumber, colNumber))) return true;
                
                if(enemyChessPieceDictionary.ChessPiecePositionMap.ContainsKey(new Vector2Int(rowNumber, colNumber))) return true;
                
                return false;
            }

            return false;
        }

        public List<Vector2Int> CalculateRookMoves(Vector2Int currentRookCoordinate)
        {
            List<Vector2Int> possibleRookMoves = new List<Vector2Int>();

            // RIGHT MOVING
            for (int i = currentRookCoordinate.y + 1; i < 8; i++)
            {
                if (IsSquareOccupied(currentRookCoordinate.x, i))
                {
                    if (EnemyPresent(new Vector2Int(currentRookCoordinate.x, i)))
                    {
                        possibleRookMoves.Add(new Vector2Int(currentRookCoordinate.x,
                            i)); //Adding this to possibleRookMoves list as player can kill enemy
                    }

                    break; //Break the loop as we can't move further as there is a piece in the way
                }

                possibleRookMoves.Add(new Vector2Int(currentRookCoordinate.x, i));
            }

            // LEFT MOVING 
            for (int i = currentRookCoordinate.y - 1; i >= 0; i--)
            {
                if (IsSquareOccupied(currentRookCoordinate.x, i))
                {
                    if (EnemyPresent(new Vector2Int(currentRookCoordinate.x, i)))
                    {
                        possibleRookMoves.Add(new Vector2Int(currentRookCoordinate.x,
                            i)); //Adding this to possibleRookMoves list as player can kill enemy
                    }

                    break; //Break the loop as we can't move further as there is a piece in the way

                }

                possibleRookMoves.Add(new Vector2Int(currentRookCoordinate.x, i));
            }

            // UP MOVING 
            for (int i = currentRookCoordinate.x + 1; i < 8; i++)
            {
                if (IsSquareOccupied(i, currentRookCoordinate.y))
                {
                    if (EnemyPresent(new Vector2Int(i,
                            currentRookCoordinate.y)))
                    {
                        possibleRookMoves.Add(new Vector2Int(i, currentRookCoordinate.y));
                    }

                    break;

                }

                possibleRookMoves.Add(new Vector2Int(i, currentRookCoordinate.y));
            }

            // DOWN MOVING 
            for (int i = currentRookCoordinate.x - 1; i >= 0; i--)
            {
                if (IsSquareOccupied(i, currentRookCoordinate.y))
                {
                    if (EnemyPresent(new Vector2Int(i,
                            currentRookCoordinate.y)))
                    {
                        possibleRookMoves.Add(new Vector2Int(i, currentRookCoordinate.y));
                    }

                    break;
                }

                possibleRookMoves.Add(new Vector2Int(i, currentRookCoordinate.y));
            }

            return possibleRookMoves;
        }

        public List<Vector2Int> CalculateBishopMoves(Vector2Int currentBishopCoordinate)
        {
            List<Vector2Int> possibleBishopMoves = new List<Vector2Int>();

            // Diagonal moves (up-right)

            for (int i = currentBishopCoordinate.x + 1, j = currentBishopCoordinate.y + 1; i < 8 && j < 8; i++, j++)
            {
                if (IsSquareOccupied(i, j))
                {
                    if (EnemyPresent(new Vector2Int(i, j)))
                    {
                        possibleBishopMoves.Add(new Vector2Int(i, j));
                    }

                    break;

                }

                possibleBishopMoves.Add(new Vector2Int(i, j));
            }

            // Diagonal moves (up-left)
            for (int i = currentBishopCoordinate.x + 1, j = currentBishopCoordinate.y - 1; i < 8 && j >= 0; i++, j--)
            {
                if (IsSquareOccupied(i, j))
                {
                    if (EnemyPresent(new Vector2Int(i, j)))
                    {
                        possibleBishopMoves.Add(new Vector2Int(i, j));
                    }

                    break;
                }

                possibleBishopMoves.Add(new Vector2Int(i, j));
            }

            // Diagonal moves (down-right)

            for (int i = currentBishopCoordinate.x - 1, j = currentBishopCoordinate.y + 1; i >= 0 && j < 8; i--, j++)
            {
                if (IsSquareOccupied(i, j))
                {
                    if (EnemyPresent(new Vector2Int(i, j)))
                    {
                        possibleBishopMoves.Add(new Vector2Int(i, j));
                    }

                    break;
                }

                possibleBishopMoves.Add(new Vector2Int(i, j));
            }

            // Diagonal moves (down-left)

            for (int i = currentBishopCoordinate.x - 1, j = currentBishopCoordinate.y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (IsSquareOccupied(i, j))
                {
                    if (EnemyPresent(new Vector2Int(i, j)))
                    {
                        possibleBishopMoves.Add(new Vector2Int(i, j));
                    }

                    break;

                }

                possibleBishopMoves.Add(new Vector2Int(i, j));
            }


            return possibleBishopMoves;
        }

        public List<Vector2Int> CalculateQueenMoves(Vector2Int currentQueenCoordinate)
        {
            List<Vector2Int> possibleQueenMoves = new List<Vector2Int>();

            // Rook-like moves of the queen
            possibleQueenMoves = CalculateRookMoves(currentQueenCoordinate);

            // Bishop-like moves of the queen
            possibleQueenMoves.AddRange(
                CalculateBishopMoves(
                    currentQueenCoordinate)); //Adding the list of bishop moves to the list of queen moves

            return possibleQueenMoves;
        }

        public List<Vector2Int> CalculateKingMoves(Vector2Int currentKingCoordinate)
        {
            List<Vector2Int> possibleKingMoves = new List<Vector2Int>();

            // Possible moves in 8 directions (horizontally, vertically, and diagonally)
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue; // Skip the current position
                    int newX = currentKingCoordinate.x + i;
                    int newY = currentKingCoordinate.y + j;

                    if (IsWithinBoard(newX, newY) && !IsSquareOccupied(newX, newY))
                    {
                        possibleKingMoves.Add(new Vector2Int(newX, newY));
                    }

                    if (IsWithinBoard(newX, newY) && IsSquareOccupied(newX, newY) &&
                        EnemyPresent(new Vector2Int(newX, newY)))
                    {
                        possibleKingMoves.Add(new Vector2Int(newX, newY));
                    }
                }
            }

            return possibleKingMoves;
        }

        public List<Vector2Int> CalculatePawnMoves(Vector2Int currentPawnCoordinate)
        {
            List<Vector2Int> possiblePawnMoves = new List<Vector2Int>();

            int forwardDirection = 1; 
            int startingRow = 1; 

            // Single forward move
            int singleForwardX = currentPawnCoordinate.x + forwardDirection;
            int singleForwardY = currentPawnCoordinate.y;
            
            if (IsWithinBoard(singleForwardX, singleForwardY) && !IsSquareOccupied(singleForwardX, singleForwardY))
            {
                possiblePawnMoves.Add(new Vector2Int(singleForwardX, singleForwardY));

                // Double forward move from starting row
                int doubleForwardX = currentPawnCoordinate.x + forwardDirection * 2;
                if (currentPawnCoordinate.x == startingRow && !IsSquareOccupied(doubleForwardX, singleForwardY))
                {
                    possiblePawnMoves.Add(new Vector2Int(doubleForwardX, singleForwardY));
                }
            }

            // Diagonal captures
            int diagonalX = currentPawnCoordinate.x + forwardDirection;
            int diagonalY1 = currentPawnCoordinate.y + 1;
            int diagonalY2 = currentPawnCoordinate.y - 1;

            if (IsWithinBoard(diagonalX, diagonalY1) && IsSquareOccupied(diagonalX, diagonalY1) && EnemyPresent(new Vector2Int(diagonalX, diagonalY1)))
            {
                possiblePawnMoves.Add(new Vector2Int(diagonalX, diagonalY1));
            }

            if (IsWithinBoard(diagonalX, diagonalY2) && IsSquareOccupied(diagonalX, diagonalY2) && EnemyPresent(new Vector2Int(diagonalX, diagonalY2)))
            {
                possiblePawnMoves.Add(new Vector2Int(diagonalX, diagonalY2));
            }

            return possiblePawnMoves;
        }

        public List<Vector2Int> CalculateKnightMoves(Vector2Int currentKnightCoordinate)
        {
            List<Vector2Int> possibleKnightMoves = new List<Vector2Int>();

            int[] xOffset = { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[] yOffset = { 2, 1, -1, -2, -2, -1, 1, 2 };

            for (int i = 0; i < 8; i++)
            {
                int targetX = currentKnightCoordinate.x + xOffset[i];
                int targetY = currentKnightCoordinate.y + yOffset[i];

                if (IsWithinBoard(targetX, targetY) && !IsSquareOccupied(targetX, targetY))
                {
                    possibleKnightMoves.Add(new Vector2Int(targetX, targetY));
                }

                if (IsWithinBoard(targetX, targetY) && IsSquareOccupied(targetX, targetY) && 
                    EnemyPresent(new Vector2Int(targetX, targetY)))
                {
                    possibleKnightMoves.Add(new Vector2Int(targetX, targetY));
                }
            }

            return possibleKnightMoves;
        }

        private bool IsWithinBoard(int x, int y)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }
    }
}
