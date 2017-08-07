using UnityEngine;

public class PressurePlate : MonoBehaviour, IInteractable
{
    public Sprite OnSprite;
    public Door[] Doors;
	
    public void Interact()
    {
        TurnOn();
    }

	void TurnOn()
    {
        GetComponent<SpriteRenderer>().sprite = OnSprite;
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Open();
        }
    }
}
