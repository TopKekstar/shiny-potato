using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder  {

    /// <summary>
    /// Read the file in order to build the map
    /// </summary>
    /// <param name="file">the file</param>
    /// <returns>a blockInfo array build </returns>
    public static Tile.TileInfo[,] ReadFile(TextAsset file)
    {
        string maindata = file.text;
        maindata = maindata.Replace("\r\n", "\n");
        string[] data = file.text.Split('\n');
        int height = (data.Length-6)/2;
        

        Tile.TileInfo[,] tileMap = new Tile.TileInfo[height,11];
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
                    if (tileMap[j, i] != null)
                    {
                        tileMap[j, i]._touchs= n;
                    }
                    i++;
                    if (i == 11)
                        break;

                }
                j++;
                if (j == height)
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

                        tileMap[j, i] = new Tile.TileInfo();
                        tileMap[j, i].position.x = i;
                        tileMap[j, i].position.y = j;
                        tileMap[j, i]._type= n;
                    }
                    else
                    {

                        tileMap[j, i] = null;
                    }
                    i++;
                    if (i == 11)
                        break;

                }

                j++;
                if (j == height)
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
        
        return tileMap;
    }
}
