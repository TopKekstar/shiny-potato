using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class Monetizer : MonoBehaviour
{

    // Variables for unityAds
    static string gameID = "2999979";
    static bool testMode = true;
    string placementId = "rewardedVideo";

    //GUI Variables
    public GameObject monetizePanel;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Starting monetization");
        try
        {
           Monetization.Initialize(gameID, testMode);
           
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void ShowAD()
    {
        StartCoroutine(ShowAdWhenReady());
    }
    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }
    void AdFinished (ShowResult res)
    {
        if (res == ShowResult.Finished) //reward player;
            monetizePanel.SetActive(false);
    }

    //Shows the panel with the 'watch ad' button
    public void ShowMonetizePanel()
    {
        monetizePanel.SetActive(true);
    }
    //Closes the panel with the 'watch ad' button
    public void CloseMonetizePanel()
    {
        monetizePanel.SetActive(false);
    }

}
