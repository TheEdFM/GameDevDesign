using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private float horiz;
    private float vert;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        anim.SetFloat("HorizontalSpeed", speed*horiz);
        anim.SetFloat("VerticalSpeed", speed*vert);
    }
    
    void FixedUpdate()
    {
        rb2d.velocity = speed *(new Vector2(horiz, vert)).normalized;
    }
}
