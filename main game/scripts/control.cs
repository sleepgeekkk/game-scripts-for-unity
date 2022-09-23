using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class control : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float moveSpeed = 10f; //скорость

    bool isFacingRight = true; //проверка на то что смотрит направо
    Animator anim; // подклбчение анимации

    bool isGrounded = false; // в воздухе или нет

    public Transform groundCheck; // ссылка на прикосновение

    float groundRadius = 0.2f; //радиус соприкосновения

    public LayerMask whatIsGround; //ссылка на слой


    void Start() // функция при запуске
    {
        anim = GetComponent<Animator>(); //берет данные из указанной анимации
        rigidBody = GetComponent<Rigidbody2D>(); //
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("ground", isGrounded); // изм переменной
        anim.SetFloat("vspeed", rigidBody.velocity.y); //скорость взлета и падения
        if (!isGrounded) //выход из прыжка
            return;
    }



    public void Run()
    {

        float move = Input.GetAxis("Horizontal"); //переменная для передвижения
        anim.SetFloat("speed", Mathf.Abs(move)); //функция для ядвижения?
        

        rigidBody.velocity = new Vector2(move * moveSpeed, rigidBody.velocity.y);
        if (move > 0 && !isFacingRight)
            Flip();                            // условия для того чтобы игрок вертелся
        else if (move < 0 && isFacingRight)
            Flip();
    }

    void Flip()  // сама функция верчения
    {

        isFacingRight = !isFacingRight; 

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // если игрок на земеле и пробел
        {
            anim.SetBool("ground", false);
            rigidBody.AddForce(new Vector2(0, 250 )); //сила прыжка
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
