using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For managing the buttons of the menu
/// </summary>
public class MenuButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// go to our GitHub organization
    /// </summary>
    public void GoToGit()
    {
        Application.OpenURL("https://github.com/TopKekstar/");
    }

    /// <summary>
    /// Useless method by Manuel
    /// </summary>
    public void Magic()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");

    }
}
