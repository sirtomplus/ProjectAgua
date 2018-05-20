using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    const float INTERP_TIME = 0.2f;
    const float MAX_VELOCITY = 2f;

    Rigidbody m_Rigidbody;
    
    float m_Distance = 0f;
    float m_Speed = 0f;

    bool m_IsAccelerating = false;
    bool m_IsDragging;


    // Use this for initialization
    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_IsAccelerating)
        {
            m_Speed = Mathf.Lerp(m_Speed, MAX_VELOCITY, INTERP_TIME);
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }
        else
        {
            m_Speed = Mathf.Lerp(m_Speed, 0, INTERP_TIME);
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }
    }

    void OnMouseDown()
    {
#if UNITY_ANDROID
        var touch = Input.GetTouch(0);
        var ray = Camera.main.ScreenPointToRay(touch.position);
#else
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
        m_Distance = Vector3.Distance(ray.origin, transform.position);
        m_IsAccelerating = true;
    }

    void OnMouseDrag()
    {
#if UNITY_ANDROID
        var touch = Input.GetTouch(0);
        var ray = Camera.main.ScreenPointToRay(touch.position);
#else
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
        var pos = ray.origin + ray.direction * m_Distance;
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, pos, INTERP_TIME);
    }

    void OnMouseUp()
    {
        m_IsAccelerating = false;
    }
}
