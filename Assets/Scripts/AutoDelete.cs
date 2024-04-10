using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoDelete : MonoBehaviour
{
    private float time = 10f;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        // Your other logic here
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;
        float fadeOutTime = 0f  ;

        while (elapsedTime < time)
        {
            if (elapsedTime > 8)
            {
                float newAlpha = Mathf.Lerp(startAlpha, 0f, fadeOutTime );
                image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
                fadeOutTime+= Time.deltaTime;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        Destroy(this.gameObject);
    }
}
