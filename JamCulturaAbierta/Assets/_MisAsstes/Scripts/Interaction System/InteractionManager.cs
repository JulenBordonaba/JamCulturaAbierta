using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour, IPlayer
{
    public Camera myCam;

    public float hitDistance = 5f;

    public LayerMask hitLayers;
    public LayerMask outPaintingLayers;
    public LayerMask inPaintingLayers;


    public Texture2D cursorDefault;
    public Texture2D cursorInteract;

    public Texture2D crosshairDefault;
    public Texture2D crosshairIntercat;

    public Image crosshair;

    private Rect crosshairImageRect;

    public AudioSource overInteractableSound;
    

    private bool isPlayerPaused;


    private bool overInteractable = false;

    private PlayerMinimap minimap;

    private void Start()
    {
        minimap = GetComponent<PlayerMinimap>();
        crosshairImageRect = crosshair.sprite.rect;
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Update()
    {
        if (IsPaused) return;

        CheckImage();

        if(Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (minimap.isMapOpen) return;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitDistance, hitLayers))
        {
            IInteractable[] interactables = null;
            
            if ((interactables = hit.transform.GetComponents<IInteractable>()) != null)
            {
                foreach(IInteractable interactable in interactables)
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void CheckImage()
    {

        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitDistance * 3, hitLayers) && !minimap.isMapOpen)
        {
            if(!overInteractable)
            {
                overInteractable = true;
                overInteractableSound?.Play();
            }

            if (Cursor.visible)
            {
                Cursor.SetCursor(cursorInteract, Vector2.zero, CursorMode.ForceSoftware);
            }
            else
            {
                
                crosshair.sprite = Sprite.Create(crosshairIntercat, new Rect(0.0f, 0.0f, crosshairIntercat.width, crosshairIntercat.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
            
        }
        else
        {
            if(overInteractable)
            {
                overInteractable = false;
            }
            if (Cursor.visible)
            {
                Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
            }
            else 
            {
                crosshair.sprite = Sprite.Create(crosshairDefault, new Rect(0.0f, 0.0f, crosshairIntercat.width, crosshairIntercat.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
        }
    }

    public void EnterPainting()
    {
        hitLayers = inPaintingLayers;
        crosshair.gameObject.SetActive(false);
    }

    public void ExitPainting()
    {
        hitLayers = outPaintingLayers;
        crosshair.gameObject.SetActive(true);
    }


    public bool IsPaused
    {
        get
        {
            return PauseManager.Instance.onPause;
        }
    }

}
