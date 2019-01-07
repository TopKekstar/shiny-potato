using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
using System;


public class ProgressManager : MonoBehaviour {

    public static GameManager.gameProgress LoadGameProgress()
    {
        FileStream progress;
        GameManager.gameProgress gProgress = new GameManager.gameProgress(0);

        try
        {
            progress = new FileStream("kek.lol", FileMode.Open);

            string encodedinput = "";
            int a = 0;
            a = progress.ReadByte();

            while (a != -1)
            {
                encodedinput += (char)a;
                a = progress.ReadByte();
            }
            progress.Close();

            string jsonInput = Cryptography.DecryptString(encodedinput);
            gProgress = GameManager.gameProgress.FromJson(jsonInput);

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
        
        for (int i = 0; i < prog.NLevels; i++)
        {
            var aux = new GameManager.gameProgress.levelProgress();
            aux.complete = false;
            if (i == 0) aux.unlocked = true;
            else aux.unlocked = false;
            aux.levelNumber = (short)i;
            aux.score = 0;
            aux.stars = 3;

            prog.Progresses.Add(aux);
        }

        string jsoned = "";
        jsoned = GameManager.gameProgress.ToJson(ref prog);

        string encoded = Cryptography.EncryptString(jsoned);


        FileStream progress = new FileStream("kek.lol", FileMode.Create);
            foreach (var cha in encoded)
        {
            progress.WriteByte((byte)cha);
        }
        progress.Close();

    }
    static class Cryptography
    {
        
        public static string EncryptString(string plainText)
        {
            byte[] save =  System.Text.Encoding.UTF8.GetBytes(plainText);
            string output = "";
            foreach(var b in save)
            {
                output += (char)b+3;
            }
            return output;
        }
        //Decrypt
        public static string DecryptString(string input)
        {
            string output = "";
            foreach(var ch in input)
            {
                output += (ch - 3);  
            }
            return output;
        }
    }
}


