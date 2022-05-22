using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameObject character = null;
    [SerializeField, Range(0, 1000)] float speed = 1;
    [SerializeField, Range(0, 100)] float height = 1;
    [SerializeField] ParticleSystem parSystem;

    [SerializeField] Rigidbody rigi => character.GetComponent<Rigidbody>();
    [SerializeField] Transform tr => character.GetComponent<Transform>();
    [SerializeField] StatusPattern status => StatusPattern.Instance;
    private void FixedUpdate()
    {
        RigibodyMove();
    }

    float hor = 0, ver = 0, jump = 0;
    Vector3 forcce = Vector3.zero;
    [SerializeField] bool isGround = false;
    void RigibodyMove()
    {
        hor = Input.GetAxis("Horizontal");
        //ver = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
        if (hor != 0)
        {
            rigi.AddForce(tr.forward * hor * speed * Time.fixedDeltaTime);
        }
        if (isGround && jump != 0)
        {
            rigi.AddRelativeForce(tr.up * height * jump * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }
    private void Update()
    {
        IsGround();
    }
    
    void IsGround()
    {
        Ray ray = new Ray(tr.position, -tr.up);
        RaycastHit hit;
        if (Physics.Raycast(ray:ray, hitInfo:out hit, float.MaxValue, layerMask:1 << 8))
        {
            if (hit.distance <= 2)
            {
                isGround = true;
                parSystem.Stop();
            }
            else
            {
                isGround = false;
                parSystem.Play();
            } 
        }
        else
        {
            isGround = false;
            parSystem.Play();
        }
    }
}
