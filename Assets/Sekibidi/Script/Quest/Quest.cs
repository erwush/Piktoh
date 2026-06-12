using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public int questId;
    public string displayName;
    [TextArea(3,50)]public string desc;
    public Vector3 pos;
    public QuestStatus status;

    public int currentAmount;
    public int targetAmount;

   #if UNITY_EDITOR
private void OnValidate()
{
    string numberPart = name.Replace("Quest", "");

    if (int.TryParse(numberPart, out int result))
    {
            questId = result;
    }
}
#endif

}


public enum QuestStatus
{
    Locked,
    Active,
    Completed,
}