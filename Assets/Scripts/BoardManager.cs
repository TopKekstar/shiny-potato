using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    BlockLogic[,] blocks;
    Dictionary<string, GameObject> prefabs;
    public void BuildMap (BlockLogic.BlockInfo[,] blockInfo)
    {
        for(int i = 0; i < blockInfo.GetLength(0); i++)
        {
            for (int j = 0; j < blockInfo.GetLength(1); j++)
            {
                if(blockInfo[i,j]!= null)
                {
                    string key = blockInfo[i, j].type.ToString();
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

                    temp = GameObject.Instantiate(temp, transform);
                    blocks[i, j] = temp.GetComponent<BlockLogic>();
                    blocks[i, j].SetProperties(blockInfo[i, j].position, blockInfo[i, j].lifes);



                }
                else
                {
                    blocks[i, j] = null;
                }

            }
        }
        

    }

    public bool MapFinished()
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (blocks[i, j] != null)
                {
                    if(blocks[i,j].IsDead()&& blocks[i, j].MustBeDestroyed())
                    {
                        continue;
                    }
                    if(blocks[i, j].MustBeDestroyed())
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
        blocks = new BlockLogic[11, 11];

    }

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
