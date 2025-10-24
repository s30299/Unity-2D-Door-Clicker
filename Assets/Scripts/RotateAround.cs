using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed = 3f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
    }
}
