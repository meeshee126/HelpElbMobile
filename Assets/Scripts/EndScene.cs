using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
   public CameraManager cameraManager;
   public Panda panda;

   public GameObject uiEndScore;
   public GameObject score;

    public MenuManager menuManager;


    void Start()
    {
        uiEndScore = GameObject.Find("End");
        score = GameObject.Find("Score");
        panda = GameObject.Find("Panda").GetComponent<Panda>();
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();

        //uiEndScore.SetActive(false); 
    }

    void Update()
    {
    /*    if (uiEndScore == null) uiEndScore = GameObject.FindGameObjectWithTag("UIEND"); 
        if (score == null) score = GameObject.FindGameObjectWithTag("UI");*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Panda")
        {
            cameraManager.lerpSpeed = 0.1f;
            cameraManager.xOffset = 8;
            cameraManager.yOffset = 30;

            other.gameObject.GetComponent<Panda>().enabled = false;
        }
    }
}
