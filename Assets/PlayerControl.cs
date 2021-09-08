using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Tombol untuk menggerakkan keatas
    public KeyCode upButton = KeyCode.W;

    //Tombol Untuk menggerakkan kebawah
    public KeyCode downButton = KeyCode.S;

    //Kecepatan gerak.
    public float speed = 10.0f;

    //Batas atas & bawah game scene (Batas bawah menggunakan minus(-))
    public float yBoundary = 9.0f;

    //Rigidbody 2D raket ini.
    private Rigidbody2D rigidBody2D;

    //Skor pemain.
    private int score;

    //Titik tumbukan terakhir dengan bola, untuk menampilkan variabel" fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Dapatkan kecepatan raket sekarang.
        Vector2 velocity = rigidBody2D.velocity;

        // Jika pemain menekan tombol ke atas, beri kec positif ke komponen y (ke atas).
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        // Jika player menekan tombol ke bawah, berik kec negatif ke komponen y (ke bawah).
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        // Jika player tidak menekan tombol apa" , kec no.
        else
        {
            velocity.y = 0.0f;
        }

        //Masukkan kembali kec ke rigidBody2D.
        rigidBody2D.velocity = velocity;

        //Dapatkan posisi raket sekarang
        Vector3 position = transform.position;

        //Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas default.
        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas bawah default.
        else if(position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        // Masukkan kembali posisinya ke transform
        transform.position = position;
    }

    // Menaikkan skor sebanyak 1 point
    public void IncrementScore()
    {
        score++;
    }

    // Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    //Mendapatkan nilai score.
    public int Score
    {
        get { return score; }
    }

    /* DEBUG INFORMATION SKRIP */

    // Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    //ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }



}
