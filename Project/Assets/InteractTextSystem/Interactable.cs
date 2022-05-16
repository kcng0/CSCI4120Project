using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType {
        Door,
        Item
    }

    public InteractionType interactionType;

    public abstract string GetDescription();
    public abstract void Interact();

}
