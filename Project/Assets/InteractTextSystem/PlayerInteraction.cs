using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;
    public TMPro.TextMeshProUGUI interactionText;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit hit;

        bool sucessfulHit = false;
        if (Physics.Raycast(ray, out hit, interactionDistance)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null) {
                print("spot interactable");
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                sucessfulHit = true;
            }
        }

        if (!sucessfulHit) {
            interactionText.text = "";
        }
    }

    void HandleInteraction(Interactable interactable) {
        KeyCode key = KeyCode.E;
        switch (interactable.interactionType) {
            case Interactable.InteractionType.Door:
                if (Input.GetKeyDown(key)) {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Item:
                if (Input.GetKeyDown(key)) {
                    interactable.Interact();
                }
                break;
            default:
                throw new System.Exception("Unknown interaction type");
        }
    }
}
