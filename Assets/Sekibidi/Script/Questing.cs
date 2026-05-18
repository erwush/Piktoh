using System.Collections;
using TMPro;
using UnityEngine;

public class Questing : MonoBehaviour
{
    public QuestUI UI;
    public Quest activeQuest;
    public GameObject questNotif;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator QuestChange()
    {
        Animator anim = questNotif.GetComponent<Animator>();
        anim.speed = 1f;
        anim.Play("Turun");
        yield return new WaitForSeconds(0.5f);
        anim.speed = 0f;
    }

    
}
