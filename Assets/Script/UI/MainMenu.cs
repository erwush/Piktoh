using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string[] sceneNames;
    public GameObject abc;
    public Interactable1 scr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartDialog()
    {
        scr.Dialog();
        scr.inArea = true;
        abc.SetActive(false);
    }
}
