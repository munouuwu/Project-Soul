using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAimDirection : MonoBehaviour
{
    [SerializeField] 
    Transform head;
    public Transform GetMuzzlePos => head.GetChild(0);
    private Vector2 mousePoint;
    public Vector2 GetMousePoint=>mousePoint;
    public Camera cam;
    float angle;
    Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePoint - new Vector2(head.position.x, head.position.y);
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        head.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
