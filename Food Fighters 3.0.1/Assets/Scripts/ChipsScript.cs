using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class ChipsScript : MonoBehaviour
{
    [SerializeField] private float tiempoSiguienteAtaque;
    [SerializeField] private float tiempoSiguienteAtaque2;
    [SerializeField] private float tiempoSiguienteAtaque3;
    [SerializeField] private float tiempoSiguienteAtaqueGun;
    [SerializeField] private float tiempoEntreAtaque;
    [SerializeField] private float tiempoEntreAtaque2;
    [SerializeField] private float tiempoEntreAtaque3;
    [SerializeField] private float tiempoEntreAtaqueGun;

    [SerializeField]
    public float verticalSpeed;

    public float horizontalSpeed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    Vector2 cntrl;
    void Update()
    {

        cntrl = new Vector2(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        if (cntrl.x != 0)
        {
            sr.flipX = cntrl.x < 0;
        }

        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (tiempoSiguienteAtaque2 > 0)
        {
            tiempoSiguienteAtaque2 -= Time.deltaTime;
        }

        if (tiempoSiguienteAtaque3 > 0)
        {
            tiempoSiguienteAtaque3 -= Time.deltaTime;
        }

        if (tiempoSiguienteAtaqueGun > 0)
        {
            tiempoSiguienteAtaqueGun -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.J) && tiempoSiguienteAtaque <= 0)
        {
            anim.SetTrigger("SendPunch1");
            tiempoSiguienteAtaque = tiempoEntreAtaque;
        }

        if (Input.GetKeyDown(KeyCode.K) && tiempoSiguienteAtaque2 <= 0)
        {
            anim.SetTrigger("SendPunch2");
            tiempoSiguienteAtaque2 = tiempoEntreAtaque2;
        }

        if (Input.GetKeyDown(KeyCode.L) && tiempoSiguienteAtaque3 <= 0)
        {
            anim.SetTrigger("SendPunch3");
            tiempoSiguienteAtaque3 = tiempoEntreAtaque3;
        }

        if (Input.GetKeyDown(KeyCode.Q) && tiempoSiguienteAtaqueGun <= 0)
        {
            anim.SetTrigger("IsShooting");
            tiempoSiguienteAtaqueGun = tiempoEntreAtaqueGun;
        }


        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ataque 2 0")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ataque 3 0")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            anim.SetBool("IsWalking", cntrl.magnitude != 0);
            rb.velocity = new Vector2(cntrl.x * horizontalSpeed, cntrl.y * verticalSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }




    }

}

