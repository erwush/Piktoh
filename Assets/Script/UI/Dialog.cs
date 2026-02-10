using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    public int dialogCount;
    public int currentDial;
    public string[] nama;
    public string[] text;
    public Sprite[] avatar;
    public float typingSpeed;
}
