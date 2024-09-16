using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private GameObject _player;

    public float _Lifes;

    public Transform player;
    private bool isFacingRight = true;

    private bool Verdadero = false;

    [SerializeField] private float DistanciaMinima;
    [SerializeField] private float speed;
    //no olvides agregar un valor a la distancia
    [SerializeField] private float RangoVista;
    private Animator animator;

    public GameObject HitboxAttack;
    private void Start()
    {
        _Lifes = 10;
        animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Rutina
        if (Vector2.Distance(transform.position, _player.transform.position) > RangoVista)
        {
            animator.SetBool("Atacando", false);
        
        }
        else
        {
            //Persecución del jugador
            if (Vector2.Distance(transform.position, _player.transform.position) > DistanciaMinima && Verdadero == false)
            {
         
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
                animator.SetBool("Atacando", false);
            }
            else
            {
                Ataque();
            }

            if (_Lifes < 0)
            {
                Destroy(this.gameObject);
            }

        }


        bool isPlayerRight = transform.position.x > _player.transform.position.x;
        Flip(isPlayerRight);

        if (Vector2.Distance(transform.position, _player.transform.position) > DistanciaMinima + 1.5f)
        {
            Verdadero = false;
            HitboxAttack.SetActive(false);
        }
    }

    private void Flip(bool isPlayerRight)
    {
        if ((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Ataque()
    {
        animator.SetBool("Atacando", true);
        Verdadero = true;
    }

    private void AtaqueEncendido()
    {
        HitboxAttack.SetActive(true);
    }
    private void AtaqueApagado()
    {
        HitboxAttack.SetActive(false);
        animator.SetBool("Atacando", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBoxPirate1"))
        {
            DañoVerdadero();
            GotHit();
            _Lifes -= 2;
        }
        if (collision.gameObject.CompareTag("HitBoxPirate2"))
        {
            DañoVerdadero();
            GotHit();
            _Lifes -= 4;
        }
        if (collision.gameObject.CompareTag("HitBoxPirate3"))
        {
            DañoVerdadero();
            GotHit();
            _Lifes -= 8;
        }
    }

    public void DañoVerdadero()
    {
        speed = 0;
        animator.SetBool("DañoRecibido", true);
    }

    public void GotHit()
    {
        animator.SetBool("Atacando", false);
    }

    public void PosHit()
    {
        speed = 3;
        animator.SetBool("DañoRecibido", false);
    }
}
