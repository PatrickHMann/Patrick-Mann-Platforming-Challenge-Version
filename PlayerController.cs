using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public int count;
    public int lives;
    public Text countText;
    public Text winText;
    public Text livesText;
    public AudioClip MusicClip;
    public AudioClip Victory;
    public AudioSource MusicSource;
    private bool facingRight;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
        lives = 3;
        SetLivesText();
        MusicSource.clip = MusicClip;
        MusicSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("left"))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp("left"))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown("right"))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp("right"))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKey("up"))
        {
            anim.SetInteger("State", 2);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                anim.SetInteger("State", 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }

    }
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count == 4) {
            lives = 3;
            SetLivesText();
            rb2d.transform.position = new Vector2(45.78f, 0.73f);
        }
        if (count == 8)
        {
            winText.text = "You win!";
            MusicSource.clip = Victory;
            MusicSource.Play();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0) { 
        winText.text = "You lose!";
        Destroy(rb2d); }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
