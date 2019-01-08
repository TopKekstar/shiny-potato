using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    private int loadedLevel;
    private gameProgress gProgress;

    public int LoadedLevel { get => loadedLevel; private set => loadedLevel = value; }

    private void Awake()
    {
        //Singleton setting up
        if (manager == null)
            manager = this;
        if (manager != this)
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);

        LoadGameProgress();
    }
    private void OnDisable()
    {
        SaveProgress();
    }
 
    /// <summary>
    /// Loads a game level 
    /// </summary>
    /// <param name="id"> Game Level to be loaded</param>
    public void loadLevel(int id)
    {
        LoadedLevel = id;
        ChangeScene(2);
    }
    
    /// <summary>
    /// Method to be notified when a level is complete. Changes scene and stores the value of that level's progress
    /// </summary>
    /// <param name="lP"> level progress of the finished level</param>
    public void levelComplete(GameManager.gameProgress.levelProgress lP)
    {
        LoadedLevel = -1;
        modifyLevelProgress(lP);
        updateProgressState();
        SaveProgress();
        ChangeScene(1);
    }

    /// <summary>
    /// Changes the scene to the given index
    /// </summary>
    /// <param name="idx">Index of the scene to be loaded</param>
    void ChangeScene(uint idx)
    {
        try
        {
            SceneManager.LoadScene((int)idx);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// This function updates the unlocked state of every level.
    /// It goes through every complete level until the last, and unlocks the level next to it
    /// </summary>
    void updateProgressState()
    {
        gameProgress.levelProgress lp;
        int i = 0;
        while (gProgress.Progresses[++i].complete)
        {
        }
        Debug.Log(i);

        lp = gProgress.Progresses[i];
        lp.levelNumber = (short)i;
        lp.unlocked = true;
        lp.complete = false;
        lp.stars = 0;
        lp.score = 0;
        modifyLevelProgress(lp);

        Debug.Log(gProgress.nLevels);

    }
  
    /// <summary>
    /// Gets the Game Progress stored in Game Manager. If it is not in memory, it loads the progress.
    /// </summary>
    /// <returns> Curent Game Progress </returns>
    public GameManager.gameProgress GetGameProgress()
    {
        if (gProgress == null) LoadGameProgress();
        return gProgress;
    }

    /// <summary>
    /// Loads the Game progress file into the gameManager attribute gProgress.
    /// </summary>
    public void LoadGameProgress()
    {
        gProgress = ProgressManager.LoadGameProgress();
    }
    /// <summary>
    /// Wrapper function to save the current game Progress
    /// </summary>
    public void SaveProgress ()
    {
        ProgressManager.SaveProgress(gProgress);
    }
    /// <summary>
    /// Accesses a specific level in the gameProgress and changes it
    /// </summary>
    /// <param name="lProgress"> New Progress to store in place</param>
    public void modifyLevelProgress(gameProgress.levelProgress lProgress)
    {
        GetGameProgress().Progresses[lProgress.levelNumber] = new gameProgress.levelProgress (ref lProgress);
    }

    /// <summary>
    /// This class is used to store the Progress of the game 
    /// </summary>
    [System.Serializable]
    public class gameProgress
    {
        [System.Serializable]
        public struct levelProgress
        {
            public bool complete;
            public bool unlocked;
            public int stars;
            public uint score;
            public short levelNumber;
            public levelProgress (ref levelProgress lp)
            {
                this.levelNumber = lp.levelNumber;
                this.unlocked = lp.unlocked;
                this.score = lp.score;
                this.complete = lp.complete;
                this.stars = lp.stars;
            }
        }

        public List<levelProgress> _progresses;
        public int nLevels;
        public int gemas;


        /// <summary>
        /// Serialzes a gameProgress instance
        /// </summary>
        /// <param name="toconvert">object to serialize</param>
        /// <returns>A string containing the Json serialized class</returns>
        public static string ToJson(ref gameProgress toconvert)
        {
            return JsonUtility.ToJson(toconvert);
        }
        /// <summary>
        /// Constructs a gameProgress object from a string formatted in json
        /// </summary>
        /// <param name="json">String containing the json-serialized object</param>
        /// <returns>gameProgress instance with the data in json string</returns>
        public static gameProgress FromJson(string json)
        {
            return JsonUtility.FromJson<gameProgress>(json);
        }
        public gameProgress(int nLevels)
        {
            this.NLevels = nLevels;
        }
 
        public List<levelProgress> Progresses
        {
            get
            {
                return _progresses;
            }

            set
            {
                _progresses = value;
            }
        }

        public int NLevels
        {
            get
            {
                return nLevels;
            }

            set
            {
                nLevels = value;
            }
        }
    }
}
