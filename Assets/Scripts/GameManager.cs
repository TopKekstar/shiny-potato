using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    private int _loadedLevel;
    private gameProgress gProgress;

    // Use this for initialization
    private void OnEnable()
    {
        //Singleton setting up
        if(manager == null)
            manager = this;
        if (manager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);


        SceneManager.activeSceneChanged += onSceneChanged;

        SceneManager.LoadSceneAsync(1);
    }
    private void OnDisable()
    {
        ProgressManager.SaveProgress(gProgress);
        SceneManager.activeSceneChanged -= onSceneChanged;
    }
    void Start () {
    }
    public void loadLevel(int id)
    {
        ProgressManager.SaveProgress(gProgress);
        _loadedLevel = id;
        ChangeScene(1);
    }
    
    /// <summary>
    /// Method to be notified when a level is complete. Changes scene and stores the value of that level's progress
    /// </summary>
    /// <param name="lP"> level progress of the finished level</param>
    public void levelComplete(GameManager.gameProgress.levelProgress lP)
    {
        modifyLevelProgress(lP);
        SaveProgress();
        ChangeScene(0);
    }


    void ChangeScene(uint idx)
    {
        Scene scene = SceneManager.GetSceneByBuildIndex((int)idx);
        if (!scene.isLoaded)
        {
            SceneManager.LoadScene((int)idx);
            return;
        }
        SceneManager.SetActiveScene(scene);
    }


    /// <summary>
    /// Callback used when a scene is loaded.
    /// If The scene loaded is the main scene, we Load the game Progress, to be used by the list.
    /// If the scene loaded is the gamescene, we initiate the level building routine.
    /// </summary>
    /// <param name="scene"> Scene that is loaded</param>
    /// <param name="mode"> Mode in which the Scene was loaded.</param>
    void onSceneChanged(Scene scene, Scene s)
    {
        if (scene.buildIndex == 0)
        {
            Debug.Log("Main Scene Loaded");
            _loadedLevel = -1;
            LoadGameProgress();

        }
        else if (scene.buildIndex == 1) // Esto significa que hemos cargado la escena con indice 1, que es la de juego
        {
            Debug.Log("Game Scene Loaded");
            GameObject lvlMgr = GameObject.Find("LevelManager");
            lvlMgr.GetComponent<LevelManager>().buildLevel(_loadedLevel);
           
        }  
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
