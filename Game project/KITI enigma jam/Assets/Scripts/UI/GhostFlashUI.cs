using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GhostFlashUI : MonoBehaviour {

    private Image _image;

    [field: SerializeField]
    public float flashTime { get; private set; } = 1.5f;

    public static GhostFlashUI Instance { get; set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        _image = GetComponent<Image>();
    }

    public void Flash(float time) {
        _image.enabled = true;

        var imgColor = _image.color;
        imgColor.a = 1f;
        _image.color = imgColor;

        StartCoroutine(FadeFlash(time));
    }

    private IEnumerator FadeFlash(float time) {
        var tmpColor = _image.color;
        var tmpAlpha = tmpColor.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(tmpAlpha, 0f, t));
            _image.color = newColor;
            yield return null;
        }
        _image.enabled = false;
    }

    public void Flash() {
        Flash(flashTime);
    }
}