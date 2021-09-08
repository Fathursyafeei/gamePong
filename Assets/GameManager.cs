using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player 1
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;

    //Player 2
    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;

    //Bola
    public BallControl ball;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    //Skor maksimal
    public int maxScore;

    // set debug window ditampilkan false
    private bool isDebugWindowShown = false;

    // Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;

    // Inisialisasi rigbody & collider
    private void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // untuk menampilkan GUI
    void OnGUI()
    {
        // Tampilkan score player 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        // Tombol Restart untuk memulai game dari awal
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            // Ketika tombol restart di tekan, reset skor kedua pemain..
            player1.ResetScore();
            player2.ResetScore();

            // ..dan Restart game.
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        // jika Player 1 Menang ( skor Maks)
        if(player1.Score == maxScore)
        {
            // .. tampilkan teks "Player One Wins" di bagian kiri layar..
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            //.. DAN kembalikan bola ketengah.
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        // Sebaliknya, jika player 2 menang( mencapai skor maks)..
        else if(player2.Score == maxScore)
        {
            //.. Tampilkan teks "PLAYER TWO WINS" di bagian kanan layar..
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            //.. dan kembalikan bola ke tengah.
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }


        // Menampilkan text area untuk debug window, Jika isDebugWindowShown == true.
        if (isDebugWindowShown)
        {
            // simpan Warna Lama
            Color oldColor = GUI.backgroundColor;

            // set Warna baRU
            GUI.backgroundColor = Color.red;

            // Simpan variabel-variabel fisika yang akan ditampilkan.
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.normalImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.normalImpulse;

            // Tentukan debug text-nya
            string debugText =
                    "Ball mass = " + ballMass + "\n" +
                    "Ball velocity = " + ballVelocity + "\n" +
                    "Ball speed = " + ballSpeed + "\n" +
                    "Ball momentum = " + ballMomentum + "\n" +
                    "Ball friction = " + ballFriction + "\n" +
                    "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                    "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            // Tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            // Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;
            
        }
        // Button toggle nilai debug window
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\n DEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
