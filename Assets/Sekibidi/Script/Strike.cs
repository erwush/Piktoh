using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Strike : MonoBehaviour
{
    public Mancing mancing;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bait")
        {
            mancing.canStrike = true;
        }
    }
   
   void OnTriggerExit2D(Collider2D other)
   {
       if (other.tag == "Bait")
       {
           mancing.canStrike = false;
       }
   }
    
}
