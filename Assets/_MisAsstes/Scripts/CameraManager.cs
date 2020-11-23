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

    public AudioSource travelSound;

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
        Cuadro.current.inPaintObjects.gameObject.SetActive(false);
        if(travelSound!=null)
        {
            if(travelSound.clip!=null)
            {
                travelSound.Play();
            }
        }

        Cuadro.current.travellCamera.Priority = 8;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        switcher.SwitchPerspective(10f, 1f);
        StartCoroutine(Flash());
        StartCoroutine(DestinyReached());
    }

    IEnumerator DestinyReached()
    {
        while (!brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }
        while (brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }
        Cuadro.current.travellCamera.Priority = 3;
        characterCamera.Priority = 10;
        MoveToDentination();
    }

    private void MoveToDentination()
    {
        PlayerManager.Instance.transform.position = Cuadro.current.pair.spawnPosition.position;
        PlayerManager.Instance.transform.forward = Cuadro.current.pair.spawnPosition.forward;
        brain.enabled = false;

        myCamera.transform.position = characterCamera.transform.position;
        myCamera.transform.rotation = characterCamera.transform.rotation;

        PlayerManager.Instance.Resume();

        Cuadro.current = null;

        StartCoroutine(ReactivateBrainOnEndOfFrame());

    }


    IEnumerator ReactivateBrainOnEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        brain.enabled = true;
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(1f);
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
        switcher.SwitchPerspective(Cuadro.current.orthographicSize, 0.5f);
        yield return new WaitForSeconds(0.4f);
        Cuadro.current.inPaintObjects.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator PlayerReached()
    {
        switcher.SwitchPerspective(10f, 0.5f);

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
