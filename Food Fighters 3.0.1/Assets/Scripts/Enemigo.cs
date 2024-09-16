using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private GameObject _player;
    public bool _Attacked;

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
                animator.Play("Idle");
            }
            else
            {
                Ataque();
            }

            if (_Lifes < 0)
            {
                ChipsScript.Instance.playerLife += 5;
                GameManager.Instance._TakeDowns -= 1;
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
        
        

     if (collision.gameObject.CompareTag("AtaqueN"))
       {
       DañoVerdadero();
       GotHit();
       _Lifes -= 2;
       animator.Play("Idle");
            
        }
         if (collision.gameObject.CompareTag("Empuje"))
         {
         Vector2 newPosition;
              _Lifes -= 3;
             if (_player.GetComponent<SpriteRenderer>().flipX == false)
             {
                 newPosition = new Vector2(transform.position.x + 5, transform.localPosition.y);
             }
             else
              {
                 newPosition = new Vector2(transform.position.x - 5, transform.localPosition.y);

             }
             transform.position = newPosition;
            }
            if (collision.gameObject.CompareTag("Especial"))
            {
                DañoVerdadero();
                GotHit();
            
            animator.Play("Idle");
            _Lifes -= 5;
            }
        
    }

    public void DañoVerdadero()
    {
   
        animator.Play("Idle");
        
    }

    public void GotHit()
    {
        animator.Play("Idle");
        
    }

    public void End()
    {
        
    }

}
