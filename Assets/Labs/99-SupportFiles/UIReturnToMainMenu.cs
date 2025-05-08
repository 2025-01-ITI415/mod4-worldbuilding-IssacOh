using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIReturnToMainMenu : MonoBehaviour
{
    //todo: dtermine if I want to set up different return points from this prefab
    private GameObject returnMenu;
    //public GameObject fpsController;


    public void Awake()
    {
       
        //returnMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    // Update is called once per frame
    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            GameObject.Find("ReturnToMenu-Equals").SetActive(false);
            

        }

        if ((Input.GetKeyDown(KeyCode.Equals)))
        {
            ReturnToMainMenu();
        }
    }
}
