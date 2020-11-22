using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MySlider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
{
    public SliderMode mode = SliderMode.filled;

    public Image fillImage;


    public float minValue = 0;
    public float maxValue = 1;

    [Range(0f, 1f)]
    public float value;


    public UnityEvent OnPointerEnterEvent = new UnityEvent();
    //public UnityEvent OnValueChanged = new UnityEvent();

    private Coroutine checkValue;

    private float previousValue;

    protected override void OnEnable()
    {
        checkValue = StartCoroutine(CheckValue());
    }

    protected override void OnDisable()
    {
        StopCoroutine(checkValue);
        checkValue = null;
    }

    protected void Update()
    {

        switch (mode)
        {
            case SliderMode.filled:
                FilledUpdate();
                break;
            case SliderMode.scaled:
                ScaledUpdate();
                break;
            default:
                break;
        }
    }


    private IEnumerator CheckValue()
    {
        while(true)
        {
            previousValue = value;
            yield return new WaitUntil(() => previousValue != value);
            //OnValueChanged.Invoke();
        }
    }

    


    private void ScaledUpdate()
    {

    }

    private void FilledUpdate()
    {
        fillImage.type = Image.Type.Filled;
        fillImage.fillAmount = value / maxValue;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        value = SetValue(pointerEventData);
    }



    public void OnInitializePotentialDrag(PointerEventData pointerEventData)
    {

    }

    public void Rebuild(CanvasUpdate canvasUpdate)
    {
        
    }

    public void LayoutComplete()
    {
        
    }

    public void GraphicUpdateComplete()
    {
        
    }

    private float SetValue(PointerEventData data)
    {
        float x = data.position.x;
        


        float _value = ((x - (Left + (Screen.width / 2))) / Width) * maxValue;

        return Mathf.Clamp(_value, minValue, maxValue);

    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OnPointerEnterEvent?.Invoke();
    }

    private float Width
    {
        get
        {
            return Right - Left;
        }
    }

    private float Left
    {
        get
        {
            return fillImage.rectTransform.rect.xMin;
        }
    }

    private float Right
    {
        get
        {
            return fillImage.rectTransform.rect.xMax;
        }
    }
}
