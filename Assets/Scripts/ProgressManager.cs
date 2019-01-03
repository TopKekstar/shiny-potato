using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ProgressManager : MonoBehaviour {

    public static GameManager.gameProgress LoadGameProgress()
    {
        FileStream progress;
        GameManager.gameProgress gProgress = new GameManager.gameProgress(0);
        
        try
        {
             progress = new FileStream("kek.json", FileMode.Open);

            string input = "";
            int a = 0;
            a = progress.ReadByte();

            while (a != -1) 
            {
                input += (char)a;
                a = progress.ReadByte();
            }


           gProgress =  GameManager.gameProgress.fromJson(input);

        }   
        catch (System.Exception e)//This means that the progress was lost or that we don't have any progress and we need to create a new progress file
        {
            if (e.GetType() != typeof(FileNotFoundException)) Application.Quit();
      
            gProgress = new GameManager.gameProgress(100); //Arbitrary
            gProgress.Progresses = new List<GameManager.gameProgress.levelProgress>();

            SaveProgress(gProgress);

        }
        return gProgress;     
    }
    public static void SaveProgress(GameManager.gameProgress prog)
    {
        FileStream progress = new FileStream("kek.json", FileMode.Create);
        for (int i = 0; i < prog.NLevels; i++)
        {
            var aux = new GameManager.gameProgress.levelProgress();
            aux.complete = false;
            if (i == 0) aux.unlocked = true;
            else aux.unlocked = false;
            aux.levelNumber = (short)i;
            aux.score = 0;
            aux.stars = 0;

            prog.Progresses.Add(aux);
        }

        string jsoned = "";
        jsoned = GameManager.gameProgress.toJson(ref prog);

        foreach (var cha in jsoned)
        {
            progress.WriteByte((byte)cha);
        }


    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
