using UnityEngine;

public class AnimUtil : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SetInactive() => gameObject.SetActive(false);
    public void SetSpeed(float spd) => GetComponent<Animator>().speed = spd;
}
