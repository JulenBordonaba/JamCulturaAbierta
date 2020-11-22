using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public GameObject itemViewPrefab;

    public List<GameObject> views = new List<GameObject>();

    public float startAngle = 0;
    public float finalAngle = 90;

    public float separationDistance = 100f;

    public GameObject container;
    

    public void Open(List<InventoryItem> items)
    {
        


        float aux = AngleRange / Mathf.Max((float)(items.Count-1),3);


        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = Instantiate(itemViewPrefab, container.transform);
            views.Add(item);
            item.transform.localPosition = Vector3.zero;

            InventoryItemView iv = item.GetComponent<InventoryItemView>();
            

            iv.ShowItem(items[i]);

            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, Mathf.Repeat(startAngle + (aux*i),360)) * Vector2.right);

            StartCoroutine(LerpFromTo(item.transform.position, container.transform.position + new Vector3(dir.x,dir.y,0).normalized * separationDistance, item.transform, 0.5f));
        }

    }

    public void Close()
    {
        foreach (GameObject item in views)
        {
            StartCoroutine(DestroyObjectOnTime(0.1f, item));
        }
        views.Clear();

        container.SetActive(false);
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

    private float AngleRange
    {
        get
        {
            return finalAngle - startAngle;
        }
    }
}
