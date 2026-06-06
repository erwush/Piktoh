using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string codeName;
    public string displayName;
    public string desc;
    public Vector3 pos;
    public QuestStatus status;

}


public enum QuestStatus
{
    Locked,
    Active,
    Completed,
}