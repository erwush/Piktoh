using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string codeName;
    public string displayName;
    public string desc;
    public Vector3 pos;
    public QuestStatus status;

    public int currentAmount;
    public int targetAmount;

}


public enum QuestStatus
{
    Locked,
    Active,
    Completed,
}