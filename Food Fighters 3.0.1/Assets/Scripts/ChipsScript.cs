using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class ChipsScript : MonoBehaviour
{
    public static ChipsScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public float playerLife = 100;
    public Image _LifeBar;
    private float vidaMaxima = 100;

    private bool Cooldown;

    public float verticalSpeed;

    public float horizontalSpeed;


    public GameObject _AtaqueN;
    public GameObject _Empuje;
    public GameObject _Especial;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;


    private void Start()
    {
        Cooldown = false;
        _AtaqueN.SetActive(false);
        _Empuje.SetActive(false);
        _Especial.SetActive(false);
    }
    Vector2 cntrl;
    void Update()
    {
        _LifeBar.fillAmount = playerLife / vidaMaxima;

        cntrl = new Vector2(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        if (Cooldown == false)
        {
            if (cntrl.x != 0)
            {
                sr.flipX = cntrl.x < 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && Cooldown == false)
        {
            anim.SetBool("GolpeN", true);
            _AtaqueN.SetActive(true);


        }

        if (Input.GetKeyDown(KeyCode.K) && Cooldown == false)
        {
            anim.SetBool("Empuje", true);
            _Empuje.SetActive(true);


        }

        if (Input.GetKeyDown(KeyCode.L) && Cooldown == false)
        {
            anim.SetBool("Especial", true);
            _Especial.SetActive(true);

        }

        if (anim.GetBool("GolpeN") && anim.GetBool("Empuje") && anim.GetBool("Especial"))
        {
            StartCoroutine(AntiBug());
            _AtaqueN.SetActive(false);
            _Empuje.SetActive(false);
            _Especial.SetActive(false);
        }



        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Chips Empujando")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Chips pegando normal")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Chips Pegando Especial")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            anim.SetBool("Idle", cntrl.magnitude != 0);
            rb.velocity = new Vector2(cntrl.x * horizontalSpeed, cntrl.y * verticalSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if(playerLife <= 0)
        {
            SceneManager.LoadScene("LooseScreen");  
        }




    }
    public void FAttack()
    {
        StartCoroutine(AntiBug());
        _AtaqueN.SetActive(false);
        _Empuje.SetActive(false);
        _Especial.SetActive(false);
    }

    public void IAttack()
    {
        Cooldown = true;
    }
    IEnumerator AntiBug()
    {
        anim.SetBool("GolpeN", false);
        anim.SetBool("Empuje", false);
        anim.SetBool("Especial", false);
        yield return new WaitForSeconds(0.2f);
        Cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AtaqueEnemigo"))
        {
            playerLife -= 10;
        }

    }
}

