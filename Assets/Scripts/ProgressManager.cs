using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
using System;


public class ProgressManager : MonoBehaviour {

    const string path = "kek.lol";
    /// <summary>
    /// Tries to load a gameProgress class from a file.
    /// It takes two steps. First it decrypts it using an external class and
    /// then it parses it like a JSON file.
    /// </summary>
    /// <returns>If the file is found, returns an instance of gameProgress with the stored values. If the file was not found, returns a new instance with the basic values so the game can start</returns>
    public static GameManager.gameProgress LoadGameProgress()
    {
        GameManager.gameProgress gProgress = new GameManager.gameProgress(0);   
        try
        {
            string actualPath = Application.persistentDataPath + "/"+path;

            // If there is no file we have to create a new one with the basic setup to start
            if (!File.Exists (actualPath))
            {
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
            else
            {
                string encoded = File.ReadAllText(actualPath);
                string json = Cryptography.DecryptString(encoded);
                gProgress = GameManager.gameProgress.FromJson(json);
            }
            
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
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
        string actualPath = Application.persistentDataPath + "/" + path;
        string jsoned = GameManager.gameProgress.ToJson(ref prog);
        string encoded = Cryptography.EncryptString(jsoned);

        File.WriteAllText(actualPath, encoded);

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


