using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Camera myCamera;

    public CinemachineVirtualCamera characterCamera;

    public CinemachineVirtualCamera paintingCamera;

    public PerspectiveSwitcher switcher;

    public Transform follow;
    public Transform lookAt;

    public Animator transportEffectAnimator;

    private CinemachineBrain brain;


    private void Start()
    {
        if (myCamera == null)
        {
            myCamera = Camera.main;
        }
        
        brain = myCamera.GetComponent<CinemachineBrain>();
    }

    public void EnterPainting()
    {
        transportEffectAnimator.SetTrigger("Enter");
    }

    public void LookPainting()
    {
        PlayerManager.Instance.Pause();
        characterCamera.Priority = 0;
        paintingCamera.Follow = Cuadro.current.cameraPivot;
        paintingCamera.LookAt = Cuadro.current.cameraTarget;

        StartCoroutine(PaintingReached());
    }

    public void ReturnToPlayer()
    {
        paintingCamera.transform.position = paintingCamera.Follow.position;
        paintingCamera.transform.rotation = paintingCamera.Follow.rotation;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(PlayerReached());
    }

    public IEnumerator PaintingReached()
    {
        while (!brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }
        while (brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }
        switcher.SwitchPerspective(Cuadro.current.orthographicSize,0.5f);
        yield return new WaitForSeconds(0.4f);
        Cuadro.current.inPaintObjects.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator PlayerReached()
    {
        switcher.SwitchPerspective(10f,0.5f);

        yield return new WaitForSeconds(0.4f);

        characterCamera.Priority = 10;

        while (!brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }
        while (brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }
        PlayerManager.Instance.Resume();

        Cuadro.current = null;
    }


}
