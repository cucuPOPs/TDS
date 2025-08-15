using System.Collections;
using TMPro;
using UnityEngine;

public class DamageUIItem : MonoBehaviour
{
    [SerializeField] RectTransform rt;
    [SerializeField] TMP_Text text;

    public void SetInfo(Vector3 pos, int damage)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);

        float localX = screenPos.x - (Screen.width / 2f);
        float localY = screenPos.y - (Screen.height / 2f);
        rt.anchoredPosition = new Vector2(localX, localY);
        text.text = damage.ToString();
    }

    public void Show()
    {
        StartCoroutine(ShowCo());
    }

    IEnumerator ShowCo()
    {
        float timer = 0;
        float animTime = 1f;
        Vector3 startPos = rt.anchoredPosition;
        Vector3 endPos = startPos + Vector3.up * 50;
        while (timer < animTime)
        {
            yield return null;
            timer += Time.deltaTime;
            rt.anchoredPosition = Vector3.Lerp(startPos, endPos, timer / animTime);
            Color c = text.color;
            c.a = Mathf.Lerp(1, 0, timer / animTime);
            text.color = c;
        }

        Destroy(gameObject);
    }
}