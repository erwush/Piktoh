using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    public int dialogCount;
    public int currentDial;
    public string[] nama;
    [TextArea(1, 50)] public string[] text;
    public Sprite[] avatar;
    public float typingSpeed;
    public bool isItem;
    public int itemCount;
    public Item givenItem;
    public System.Action function;
}
