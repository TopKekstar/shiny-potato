using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For managing the tiles 
/// </summary>
public class BoardManager : MonoBehaviour {
    Tile[,] blocks;
    Dictionary<string, GameObject> prefabs;

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
                                if (blocks[i + 1, j] == null)
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

        blocks = new Tile[blockInfo.GetLength(0), 13];

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
                        blocks[i, j].Init(blockInfo[i, j].position.x, blockInfo[i, j].position.y, blockInfo[i, j]._touchs, lm);
                        temp.transform.Translate(j, differ, 0);
                    }
                    catch (System.Exception e)
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
                    if (blocks[i, j].Touch())
                    {
                        
                    }
                }
            }
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
                    if(!blocks[i,j].gameObject.activeSelf)
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
        Debug.Log("nigga you made it");
        return true;

    }

    private void Awake()
    {
        prefabs = new Dictionary<string, GameObject>(20);

    }

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
