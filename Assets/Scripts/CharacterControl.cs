using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float fMovespeed = 20;
    public float fGravity = -20;
    public float fJumpSpeed = 20;

    private CharacterController m_Controller;
    public Vector3 v3MoveDirection;
    private Animator m_Anim;

    public float fDrag = 0.085f;

    private Transform tCamera;

    public float fTopSpeed = 20;
    public Vector3 v3MoveDirectionFlattened;

    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Anim = GetComponentInChildren<Animator>();
        tCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 v3UserInput = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            v3UserInput += tCamera.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            v3UserInput += tCamera.right * -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            v3UserInput += tCamera.forward * -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            v3UserInput += tCamera.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_Controller.isGrounded)
            {
                v3MoveDirection.y = fJumpSpeed;
            }
        }


        // V



        v3MoveDirectionFlattened = v3MoveDirection;
        v3MoveDirectionFlattened.y = 0;
        v3UserInput.y = 0;
        if ((v3MoveDirectionFlattened + v3UserInput).magnitude > fTopSpeed)
        {
            Vector3 v3Inverse = v3MoveDirectionFlattened.normalized * -1;

            Vector3 v3AdjustedInput = (Vector3.Dot(v3UserInput, v3MoveDirectionFlattened.normalized) * v3Inverse) + v3UserInput;
            Debug.DrawLine(this.transform.position, this.transform.position + v3AdjustedInput, Color.red, 2);
            v3UserInput = v3MoveDirection + (v3AdjustedInput * fMovespeed * Time.deltaTime);
            v3MoveDirection = v3UserInput;
        }
        else
        {
            v3MoveDirection += v3UserInput.normalized * fMovespeed * Time.deltaTime;
        }





        //Gravity
        v3MoveDirection.y += fGravity * Time.deltaTime;

        //Friction
        v3MoveDirection.x *= Mathf.Pow(fDrag, Time.deltaTime);
        v3MoveDirection.z *= Mathf.Pow(fDrag, Time.deltaTime);

        m_Controller.Move(v3MoveDirection * Time.deltaTime);

        // ^


        Vector3 v3LookDir = v3MoveDirection;
        v3LookDir.y = 0;

        transform.LookAt(this.transform.position + v3LookDir);

        if (m_Anim)
        {
            m_Anim.SetFloat("Forward", Vector3.Dot(v3MoveDirection, transform.forward));
            m_Anim.SetFloat("Right", Vector3.Dot(v3MoveDirection, transform.right));
        }
    }
}
