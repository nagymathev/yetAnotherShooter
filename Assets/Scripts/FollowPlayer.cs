using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private Vector2 mousePos;
    private Camera cam;
    [SerializeField] private AnimationCurve intensity;

    public Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] [Range(1, 10)] private float smoothFactor;
    [SerializeField] [Range(1, 10)] private int mouseToCamRange;
    [SerializeField] [Range(0, 1)] private float clampRange;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        mousePos = Input.mousePosition;
        //mousePos.x -= Screen.width / 2;
        //mousePos.y -= Screen.height / 2;
        mousePos = cam.ScreenToViewportPoint(mousePos);
        mousePos -= new Vector2(0.5f, 0.5f);
        mousePos *= 2;
        mousePos.x = Mathf.Clamp(mousePos.x, -clampRange, clampRange);
        mousePos.y = Mathf.Clamp(mousePos.y, -clampRange, clampRange);
        mousePos.x = intensity.Evaluate(Mathf.Abs(mousePos.x)) * Mathf.Sign(mousePos.x);
        mousePos.y = intensity.Evaluate(Mathf.Abs(mousePos.y)) * Mathf.Sign(mousePos.y);
        //Debug.Log(mousePos.x);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset + (new Vector3(mousePos.x, mousePos.y) * mouseToCamRange);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
