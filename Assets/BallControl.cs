using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D Bola.
    private Rigidbody2D rigidBody2D;

    // Besar gaya awal yang di berikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    // Method Reset Ball
    void ResetBall()
    {
        //Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjai (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }
    
    // Method Push ball
    void PushBall()
    {
        

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (ekslusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya dibawah 1, bola bergerak ke kiri. 
        //jika tidak bola bergerak ke kanan.
        if(randomDirection < 1.0f)
        {
            // Menggunakan gaya untuk menggerakkan bola
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
        }
    }

    //Method Restart Game
    void RestartGame()
    {
        //Kembalikan bola ke posisi semula.
        ResetBall();

        //Setelah 2 detik, berikan gaya ke bola.
        Invoke("PushBall", 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Mulai Game 
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
