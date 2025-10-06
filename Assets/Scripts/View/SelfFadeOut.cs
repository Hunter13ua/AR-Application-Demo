using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SelfFadeOut : MonoBehaviour
{
    private TMP_Text textComponent;
    private float fadeDuration = 1f; // Duration of the fade out in seconds

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeOutAfterDelay());
    }

    private System.Collections.IEnumerator FadeOutAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Fade out over fadeDuration seconds
        float elapsedTime = 0f;
        Color originalColor = textComponent.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure alpha is set to 0
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Disable the gameObject
        gameObject.SetActive(false);
    }
}
