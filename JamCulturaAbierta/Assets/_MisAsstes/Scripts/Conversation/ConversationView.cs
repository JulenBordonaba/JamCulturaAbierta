using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextWritter))]
public class ConversationView : MonoBehaviour, IInteractable
{
    public static ConversationView currentConversation;

    public GameObject conversationObject;

    public bool inMenu = false;

    [Header("State Data")]
    public ConversationState[] states;

    public int currentState = 0;

    [Header("Message")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI emisorText;
    public GameObject messageObject;
    [Header("Options")]
    //public GameObject optionsObject;
    public ConversationOptionsGroup[] optionGroups;

    [Header("Inventory")]
    public GameObject itemViewPrefab;
    public float startAngle = 90;
    public float finalAngle = 180;
    public float separationDistance = 200;
    public GameObject container;
    public GameObject inventoryObject;
    [HideInInspector]
    public List<GameObject> views = new List<GameObject>();

    [HideInInspector]
    public ConversationOptionsGroup currentOptionGroup = null;
    [HideInInspector]
    public TextWritter textWritter;
    [HideInInspector]
    public List<string> triggers = new List<string>();
    [HideInInspector]
    public string requieredItemId;
    [HideInInspector]
    public string correctItemTrigger;

    private bool textFinished = false;

    private PauseSettings pauseSettings;

    private void OnEnable()
    {
        ConversationManager.Instance.OnTriggerSet += SetTrigger;
        if (textWritter == null)
        {
            textWritter = GetComponent<TextWritter>();
        }

    }


    public void OpenConversation()
    {
        if (currentConversation != null && currentConversation != this)
        {
            currentConversation.CloseConversation();
        }

        conversationObject.SetActive(true);
        currentConversation = this;
        CurrentState.Open();
    }

    public void CloseConversation()
    {
        conversationObject.SetActive(false);
    }

    public void CloseInventory()
    {
        foreach (GameObject item in views)
        {
            StartCoroutine(DestroyObjectOnTime(0.1f, item));
        }
        views.Clear();

        inventoryObject.SetActive(false);
    }

    public bool CheckInventoryItem(string itemId)
    {
        if(itemId==requieredItemId)
        {
            ConversationManager.Instance.SetTrigger(correctItemTrigger);
            return true;
        }
        return false;
    }

    public void WriteMessage(string message)
    {
        if (pauseSettings == null)
        {
            pauseSettings = new PauseSettings();
        }


        pauseSettings.SaveSettings();

        messageText.text = "";
        messageObject.SetActive(true);

        textWritter.myText = messageText;
        textWritter.WriteText(message);
        if(!inMenu)
        PlayerManager.Instance.Pause();

        StartCoroutine(WaitUntilWrittingEnd());
    }

    IEnumerator WaitUntilWrittingEnd()
    {
        yield return new WaitUntil(() => !TextWritter.isWritting);
        OnWrittingEnd();
    }

    private void OnWrittingEnd()
    {
        textFinished = true;
    }

    public void CloseCurrent()
    {
        CurrentState.CurrentEvent.Close();
    }

    public void OpenCurrent()
    {
        CurrentState.Open();
    }

    public void OpenInventory()
    {
        inventoryObject.SetActive(true);

        List<InventoryItem> items = PlayerInventory.Instance.items;

        float aux = AngleRange / Mathf.Max((float)(items.Count - 1), 3);


        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = Instantiate(itemViewPrefab, container.transform);
            views.Add(item);
            item.transform.localPosition = Vector3.zero;

            ConversationInventoryItemView iv = item.GetComponent<ConversationInventoryItemView>();


            iv.ShowItem(items[i]);

            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, Mathf.Repeat(startAngle + (aux * i), 360)) * Vector2.right);

            StartCoroutine(LerpFromTo(item.transform.position, container.transform.position + new Vector3(dir.x, dir.y, 0).normalized * separationDistance, item.transform, 0.5f));
        }
    }

    private float AngleRange
    {
        get
        {
            return finalAngle - startAngle;
        }
    }

    public void OpenOptionGroup(string id)
    {
        ConversationOptionsGroup og = GetOptionGroupById(id);

        if (og != null)
        {
            og.Open();
            currentOptionGroup = og;
        }
    }

    private ConversationOptionsGroup GetOptionGroupById(string id)
    {
        foreach (ConversationOptionsGroup og in optionGroups)
        {
            if (og.id == id)
            {
                return og;
            }
        }

        return null;
    }

    public void CloseMessage()
    {
        messageObject.SetActive(false);
    }

    public void Next()
    {
        CurrentState.Next();
    }

    public void CloseOptions()
    {
        print("F");
        currentOptionGroup.Close();
    }

    private void OnDisable()
    {
        ConversationManager.Instance.OnTriggerSet -= SetTrigger;
    }

    private void Update()
    {
        CurrentState.Update();
    }

    public void SetTrigger(string trigger)
    {
        if (trigger == "") return;

        if (!triggers.Contains(trigger))
        {
            triggers.Add(trigger);
        }
    }

    public void RemoveTrigger(string trigger)
    {
        if (triggers.Contains(trigger))
        {
            triggers.Remove(trigger);
        }
    }

    IEnumerator DestroyObjectOnTime(float t, GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(t);
        Destroy(objectToDestroy);

    }

    public Vector3 VectorLerp(Vector3 from, Vector3 to, float time)
    {
        Vector3 ret = new Vector3();

        ret = Vector3.Lerp(from, to, time);

        return ret;
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    private IEnumerator LerpFromTo(Vector3 src, Vector3 dest, Transform myTransform, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            myTransform.position = VectorLerp(src, dest, (Time.time - startTime) / duration);
            yield return 1;
        }
        myTransform.position = dest;
    }

    public void FinishState()
    {
        CurrentState.End();
        currentState += 1;
        if (currentState >= states.Length)
        {
            currentState = states.Length - 1;
            CloseConversation();
            return;
        }
        //CurrentState.Open();
    }

    public void Interact()
    {
        OpenConversation();
    }

    public ConversationState CurrentState
    {
        get
        {
            if (states.Length <= 0) return null;
            if (currentState >= states.Length) return states[states.Length - 1];
            return states[currentState];
        }
    }


}
