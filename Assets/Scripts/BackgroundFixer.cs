using UnityEngine;

[ExecuteAlways] // działa też w edytorze
[RequireComponent(typeof(SpriteRenderer))]
public class FitBackgroundToCamera : MonoBehaviour
{
    public Camera cam;               // zostaw puste -> użyje Camera.main
    [Range(0f, 0.5f)]
    public float extraMargin = 0.02f; // zapas na krawędzie (2%)

    SpriteRenderer sr;

    void OnEnable() {
        sr = GetComponent<SpriteRenderer>();
        if (!cam) cam = Camera.main;
        Fit();
    }

    void LateUpdate() {
        // Jeśli zmienił się rozmiar okna/aspect/orthoSize, dopasuj ponownie
        Fit();
    }

    void Fit() {
        if (!sr || !sr.sprite || !cam || !cam.orthographic) return;

        float worldH = cam.orthographicSize * 2f;
        float worldW = worldH * cam.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size; // w jednostkach świata (uwzględnia Pixels Per Unit)
        float scaleX = worldW  / spriteSize.x;
        float scaleY = worldH  / spriteSize.y;
        float scale  = Mathf.Max(scaleX, scaleY) * (1f + extraMargin);

        transform.localScale = new Vector3(scale, scale, 1f);

        // wycentruj na kamerze (zostaw Z jak masz – byle przed kamerą)
        var z = transform.position.z;
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, z);
    }
}
