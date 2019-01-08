using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
using System;


public class ProgressManager : MonoBehaviour {


    /// <summary>
    /// Tries to load a gameProgress class from a file.
    /// It takes two steps. First it decrypts it using an external class and
    /// then it parses it like a JSON file.
    /// </summary>
    /// <returns>If the file is found, returns an instance of gameProgress with the stored values. If the file was not found, returns a new instance with the basic values so the game can start</returns>
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

            for (int i = 0; i < gProgress.NLevels; i++)
            {
                var aux = new GameManager.gameProgress.levelProgress();
                aux.complete = false;
                if (i == 0) aux.unlocked = true;
                else aux.unlocked = false;
                aux.levelNumber = (short)i;
                aux.score = 0;
                aux.stars = 0;

                gProgress.Progresses.Add(aux);
            }
            SaveProgress(gProgress);

        }
        return gProgress;
    }
    /// <summary>
    /// Saves the progress to a file.
    /// Uses JSON as a serializer and then encrypts the output string
    /// </summary>
    /// <param name="prog"> gameProgress instance to be saved into a file</param>
    public static void SaveProgress(GameManager.gameProgress prog)
    {
        string jsoned = "";
        jsoned = GameManager.gameProgress.ToJson(ref prog);

        string encoded = Cryptography.EncryptString(jsoned);

        try
        {
            FileStream progress;
            // We want to try open an existing progress file to overwrite it.
            // If it does not exist, we will create a new one
            progress = new FileStream("kek.lol", FileMode.OpenOrCreate);
            foreach (var cha in encoded)
            {
                progress.WriteByte((byte)cha);
            }
            progress.Close();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
      

    }

    /// <summary>
    /// Class implementing a basic Encrypt-Decrypt algorithm for strings
    /// </summary>
    static class Cryptography
    {
        /// <summary>
        /// Encrypts a string using kekisimo algorithm
        /// </summary>
        /// <param name="input"> String to encrypt</param>
        /// <returns>Encrypted string</returns>
        public static string EncryptString(string input)
        {
            string output = "";
            uint i = 0;
            
            foreach(char ch in input)
            {
                if (i % 2 == 0) output += (char)((ch + i));
                else output += (char)((ch - i));
                i = (i+1) % 15;
            }
            return output;
        }
        /// <summary>
        /// Decrypts a string using kekisimo algorithm
        /// </summary>
        /// <param name="input"> Input string which was encripted by kekisimo algorithm</param>
        /// <returns>Decrypted string</returns> 
        public static string DecryptString(string input)
        {
            string output = "";
            uint i = 0;
            foreach (char ch in input)
            {
                if (i % 2 == 0) output += (char)((ch - i));
                else output += (char)((ch + i));
                i = (i + 1) % 15;
            }

            return output;
        }
    }
}


