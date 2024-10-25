using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GravityGizmo : MonoBehaviour
{
    public GameObject gizmoUI; // Assign in the Inspector (default disabled)
    public Image[] directionButtons; // 6 buttons in the radial menu
    public PlayerController playerScript; // Reference to the player script

    private int hoveredDirectionIndex = -1; // Track which button is being hovered (-1 if none)
    private bool isGizmoActive = false;

    void Start()
    {
        // Ensure the gizmo UI starts disabled
        gizmoUI.SetActive(false);

        // Attach hover listeners to each button
        for (int i = 0; i < directionButtons.Length; i++)
        {
            int index = i; // Store the current index for the lambda
            AddHoverListener(directionButtons[i], index);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleGizmo(true);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            // If a button was hovered, select that gravity direction
            if (hoveredDirectionIndex != -1)
            {
                SetGravity(hoveredDirectionIndex);
            }
            ToggleGizmo(false); // Close the gizmo
        }
    }

    public void ToggleGizmo(bool isActive)
    {
        isGizmoActive = isActive;
        gizmoUI.SetActive(isActive);

        if (isActive)
        {
            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            // Disable player rotation
            playerScript.canRotate = false;
        }
        else
        {
            // Lock the cursor and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Re-enable player rotation
            playerScript.canRotate = true;

            // Reset the hovered index
            hoveredDirectionIndex = -1;
        }
    }

    public void SetGravity(int directionIndex)
    {
        Vector3 selectedDirection = GravityManager.Instance.gravityDirections[directionIndex];
        GravityManager.Instance.SetGravityDirection(selectedDirection);
    }

    private void AddHoverListener(Image button, int index)
    {
        // Add an EventTrigger component if it doesn't already exist
        EventTrigger trigger = button.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

        // Create and configure the pointer enter event
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entry.callback.AddListener((_) => { hoveredDirectionIndex = index; });

        // Add the event to the button's trigger
        trigger.triggers.Add(entry);
    }
}
