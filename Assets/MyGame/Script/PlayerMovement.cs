using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float junpHeight = 3f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 1f;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCheckGround() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && IsCheckGround())
        {
            velocity.y = Mathf.Sqrt(junpHeight * gravity * -2f);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }

    bool IsCheckGround()
    {
        bool isGround = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        return isGround;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
