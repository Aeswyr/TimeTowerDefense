using System.Collections;
using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    [SerializeField] private float fadeDelay;
    [SerializeField] private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        Color c = text.color;
        c.a = 0f;
        text.color = c;
    }

    public void FadeIn() {
        StartCoroutine(c_FadeIn());
    }

    public void FadeOut() {
        StartCoroutine(c_FadeOut());
    }

    private IEnumerator c_FadeIn() {
        Color c = text.color;
        for (float alpha = 0; alpha <= 1f; alpha += 0.1f)
        {
            c.a = alpha;
            text.color = c;
            yield return new WaitForSeconds(fadeDelay / 10f);
        }
        c.a = 1;
        text.color = c;
    }

    private IEnumerator c_FadeOut() {
        Color c = text.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            text.color = c;
            yield return new WaitForSeconds(fadeDelay / 10f);
        }
        c.a = 0;
        text.color = c;
    }
}
