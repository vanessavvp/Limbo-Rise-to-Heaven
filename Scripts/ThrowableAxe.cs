using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowableAxe : MonoBehaviour
{
    public PickUpObject PickUpObject;
    public GameObject hand;
    public GameObject axe;
    private Rigidbody rb;
    public float throwForce = 50;
    public Transform target, curve_point;
    private bool isThrown = false;
    private bool isReturning = false;
    private Vector3 oldPos;
    public Transform player;
    public float axeRange = 10.0f;
    private float time = 0.0f;

    void Start() {
        rb = axe.GetComponent<Rigidbody>();
    }

    void Update() {
        if (Gamepad.all.Count != 0) {
            if (Gamepad.current.rightTrigger.isPressed && PickUpObject.PickedObject != null) {
                int counter = target.transform.childCount;
                if (!isThrown && counter > 0) {
                    ThrowAxe();
                } else if (!isReturning){
                    ReturnAxe();
                }
            }
            float distance = (axe.transform.position - player.position).magnitude;
            if (distance > axeRange && PickUpObject.PickedObject != null) {
                ReturnAxe();
            }

            if (isReturning) {
                rb.position = getBQCPoint(time, oldPos, curve_point.position, target.position);
                if (time < 1.0f) {
                    rb.rotation = Quaternion.Slerp(rb.transform.rotation, target.rotation, 50 * Time.deltaTime);
                    time += Time.deltaTime;
                } else {
                    ResetAxe();
                }
            }
        }
    }

    void ThrowAxe() {
        hand.SetActive(false);
        time = 0.0f;
        isReturning = false;
        rb.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        rb.AddTorque(rb.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
        axe.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        isThrown = true;
    }

    void ReturnAxe() {
        axe.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        isThrown = false;
        oldPos = rb.position;
        isReturning = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }

    // Reset axe
    void ResetAxe() {
        rb.transform.parent = target.transform;
        axe.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        isReturning = false;
        isThrown = false;
        rb.position = target.position;
        rb.rotation = target.rotation;
        hand.SetActive(true);
    }

    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t *p1) + (tt * p2);
        return p; 
    }
}
