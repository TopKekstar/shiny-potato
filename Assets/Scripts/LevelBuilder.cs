using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder  {

    /// <summary>
    /// Read the file in order to build the map
    /// </summary>
    /// <param name="file">the file</param>
    /// <returns>a blockInfo array build </returns>
    public static BlockLogic.BlockInfo[,] ReadFile(TextAsset file)
    {
        string maindata = file.text;
        maindata = maindata.Replace("\r\n", "\n");
        string[] data = file.text.Split('\n');
        
        BlockLogic.BlockInfo[,] blockMap= new BlockLogic.BlockInfo[11,11];
        bool readMap = false;
        bool assingLifes = false;
        bool endedRead = false;
        bool flag = true;
        int i=0, j=0;
        int l = -1;
        while (flag)
        {
            l++;
            if (data[l] == "") continue;
            if (assingLifes)
            {
                i = 0;
                string[] vs = data[l].Split(',');
                foreach(string s in vs)
                {
                    string aux = s;
                    if (aux.Contains("."))
                    {
                        aux = aux.Replace(".", "");

                    }
                    int n = System.Int32.Parse(aux);
                    if (blockMap[j, i] != null)
                    {
                        blockMap[j, i].lifes = n;
                    }
                    i++;
                    if (i == 11)
                        break;

                }
                j++;
                if (j == 11)
                {
                    break;
                }

                continue;
            }
            if (readMap&&!endedRead)
            {
                i = 0;
                string[] vs = data[l].Split(',');
                foreach (string s in vs)
                {
                    string aux = s;
                    if (aux.Contains("."))
                    {
                        aux = aux.Replace(".", "");

                    }
                    int n = System.Int32.Parse(aux);
                    if (n != 0)
                    {
                        
                        blockMap[j, i] = new BlockLogic.BlockInfo();
                        blockMap[j, i].position.x = i;
                        blockMap[j, i].position.y = j;
                        blockMap[j, i].type = n;
                    }
                    else
                    {

                        blockMap[j, i] = null;
                    }
                    i++;
                    if (i == 11)
                        break;

                }

                j++;
                if (j == 11)
                {
                    endedRead = true;
                }
                
                continue;
            }
            if (data[l].Contains("data"))
            {
                i = 0; j = 0;
                if (readMap)
                    assingLifes = true;
                else
                    readMap = true;
                continue;
            }
            
        }
        return blockMap;
    }
}
