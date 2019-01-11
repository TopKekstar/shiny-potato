using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For managing the tiles 
/// </summary>
public class BoardManager : MonoBehaviour {
    /// <summary>
    /// all the tiles of the game
    /// </summary>
    private Tile[,] blocks;
    
    /// <summary>
    /// for managing the prefabs of the tiles
    /// </summary>
    private Dictionary<string, GameObject> prefabs;


    /// <summary>
    /// Notify to all blocks the end of the round, by calling the method end of round
    /// </summary>
    /// <returns>True if you lost, return false every other case</returns>
    public bool EndOfRound()
    {
        bool flag = false;
        for(int i = blocks.GetLength(0)-1; i>=0 ; i--)
        {
            for (int j = blocks.GetLength(1)-1; j >= 0; j--)
            {
                if (blocks[i, j] != null)
                {
                    if(!blocks[i, j].Dead)
                    {
                        blocks[i, j].EndOfRound();
                        if (blocks[i, j].CanFall())
                        {
                            try
                            {
                                if (blocks[i + 1, j] == null || blocks[i + 1, j].Dead)
                                {
                                    blocks[i, j].transform.Translate(0, -1, 0);
                                    blocks[i, j].GridPosition.y++;
                                    blocks[i + 1, j] = blocks[i, j];
                                    blocks[i, j] = null;
                                }

                            }
                            catch(System.IndexOutOfRangeException e)
                            {
                                flag = true;
                                

                            }
                        }
                    }
                    else
                    {
                        blocks[i, j].gameObject.SetActive(false);
                        blocks[i, j] = null; 
                    }
                }

            }

        }

        return flag;

    }

    /// <summary>
    /// This method build the map with the build info provided before
    /// </summary>
    /// <param name="blockInfo">an bidimensional array with all the info about the map</param>
    public void BuildMap (Tile.TileInfo[,] blockInfo, LevelManager lm)
    {
        Camera c = Camera.current;
        int differ = 2;

        prefabs.Add("20",Resources.Load<GameObject>("Blocks/20"));
        blocks = new Tile[blockInfo.GetLength(0)+2, 11];

        for (int i = blockInfo.GetLength(0)-3; i >= 0; i--,differ++)
        {
            for (int j = blockInfo.GetLength(1)-1; j >= 0; j--)
            {
                if(blockInfo[i,j]!= null)
                {
                    string key = blockInfo[i, j]._type.ToString();
                    GameObject temp;
                    try
                    {
                        temp = prefabs[key];
                    }
                    catch (System.Exception)
                    {
                        temp = Resources.Load<GameObject>("Blocks/" + key);
                        
                        prefabs.Add(key, temp);
                    }

                    try
                    {
                        
                        temp = GameObject.Instantiate(temp, transform);
                        blocks[i, j] = temp.GetComponent<Tile>();

                        System.Action<Tile> whenDie = lm.onTileDestroyed;
                        System.Action<Tile> whenHit = null;

                        int type = System.Int32.Parse(key);
                        switch (type)
                        {
                            case 7:
                                whenHit = HitRowOfTheTile;
                                break;
                            case 8:
                                whenHit = HitColumnOfTheTile;
                                break;
                        }
                        blocks[i, j].Init(blockInfo[i, j].position.x, blockInfo[i, j].position.y, blockInfo[i, j]._touchs,whenDie,whenHit);
                        temp.transform.Translate(j, differ, 0);

                    }
                    catch (System.Exception )
                    {
                        Debug.Log("Block number:" + key + " does not exist");
                        blocks[i, j] = null;
                        
                    }


                }
                else
                {
                    blocks[i, j] = null;
                }

            }
        }


    }

    public void Earthquake()
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (blocks[i, j] != null)
                {
                    //if(!blocks[i,j].Dead)
                        blocks[i, j].Hit();
                }
            }
        }

    }

    public void EraseLastRow()
    {
        int rowToErase = -1;
        for (int i = blocks.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = blocks.GetLength(1) - 1; j >= 0; j--)
            {
                if (blocks[i, j] != null)
                {
                    rowToErase = i;
                    break;
                }

            }
            if (rowToErase != -1) break;
        }

        for (int j = blocks.GetLength(1) - 1; j >= 0; j--)
        {
            if (blocks[rowToErase, j] != null)
            {
                blocks[rowToErase, j].Kill();
                
            }

        }




    }

    /// <summary>
    /// Put an steel barrier in the last row
    /// </summary>
    public void SteelBarrier()
    {
        int lastRow = blocks.GetLength(0) - 1;

        for (int j = 3; j < 8; j++)
        {
            GameObject temp = Instantiate(prefabs["20"], transform);
            blocks[lastRow, j] = temp.GetComponent<Tile>();
            blocks[lastRow, j].Init(j, lastRow,1);
            temp.transform.Translate(j, -1,0);
        }

    }

    /// <summary>
    /// Check if every block that must destroyed is destroyed
    /// </summary>
    /// <returns>return true if every block that must be destroyed is destroyed, any other case return false</returns>
    public bool MapFinished()
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (blocks[i, j] != null)
                {
                    if(blocks[i,j].Dead)
                    {
                        continue;
                    }
                    if(blocks[i, j].CanBeDestroyed())
                    {
                        return false;
                    }
                }
                else
                {
                    continue;
                }

            }
        }
        return true;

    }

    public void HitRowOfTheTile(Tile tile)
    {
        int row = tile.GridPosition.y;
        for (int j = 0; j < blocks.GetLength(1); j++)
        {
            if (blocks[row, j] != null && (blocks[row,j].type != 7 && blocks[row, j].type != 8))
            {
                blocks[row, j].Hit();
            }

        }
    }
    public void HitColumnOfTheTile(Tile tile)
    {
        int column = tile.GridPosition.x;
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            if (blocks[i, column] != null && (blocks[i, column].type != 7 && blocks[i, column].type != 8))
            {
                blocks[i, column].Hit();
            }

        }
    }

    private void Awake()
    {
        prefabs = new Dictionary<string, GameObject>(20);

    }
}
