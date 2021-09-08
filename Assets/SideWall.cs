using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // Pemain yang akan bertambah skornya jika bola menyentuh dinding
    public PlayerControl player;

    //panggil Game Manager untuk mengakses skor maks
    [SerializeField]
    private GameManager gameManager;

    //method dipanggil ketika objek lain ber-collider (bola) bersentuhan dengan dinding.
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        //jika objek tersebut bernama "Ball"
        if(anotherCollider.name == "Ball")
        {
            // Tambahkan score ke pemain 
            player.IncrementScore();
            
            //Jika score pemain belum mencapai maks
            if(player.Score < gameManager.maxScore)
            {
                // restart game setelah bola mengenai dinding.
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
