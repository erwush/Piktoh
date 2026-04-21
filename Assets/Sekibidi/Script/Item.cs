using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int itemId;
    public string itemName;
    public Sprite itemSprite;
    public int itemCount;
    public bool isFood;

}
