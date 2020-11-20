using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour, IPlayer
{
    public Camera myCam;

    public float hitDistance = 5f;

    public LayerMask hitLayers;
    public LayerMask outPaintingLayers;
    public LayerMask inPaintingLayers;
    

    private bool isPlayerPaused;

    public void Update()
    {
        if (IsPaused) return;

        if(Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    public void Interact()
    {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitDistance, hitLayers))
        {
            IInteractable interactable = null;
            if ((interactable = hit.transform.GetComponent<IInteractable>()) != null)
            {
                interactable.Interact();
            }
        }
    }

    public void EnterPainting()
    {
        hitLayers = inPaintingLayers;
    }

    public void ExitPainting()
    {
        hitLayers = outPaintingLayers;
    }


    public bool IsPaused
    {
        get
        {
            return PauseManager.Instance.onPause;
        }
    }

}
