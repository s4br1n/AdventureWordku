using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Mono.CompilerServices.SymbolWriter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gerak : MonoBehaviour
{
    public int kecepatan;
    public int kekuatanlompat;
    public bool balik;
    public int pindah;
    Rigidbody2D lompat;
    public bool tanah;
    public LayerMask targetlayer;
    public Transform deteksitanah;
    public float jangkauan;
    public int heart;
    public int coin;
    public GameObject lose;
    Vector2 play; //variabel vector untuk posisi start
    public bool play_again;
    [SerializeField] private Text info_heart; // Variabel Heart
    Text info_Coin; // Variabel untuk Koin
    private string[] scene;
    
    Animator anim; 
    private void Awake() {
        
    }
    void Start()
    {
        play=transform.position; //start sebagai object transform posisi
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // info_Coin = GameObject.Find("UI_Coin").GetComponent<Text>();
    }

    public bool ButtonLeft;
    public bool ButtonRight;
    public bool ButtonJump;

    public void buttonDownLeft(){
        ButtonLeft = true;
    }
    public void buttonUpLeft(){
        ButtonLeft = false;
    }
    public void buttonDownRight(){
        ButtonRight = true;
    }
    public void buttonUpRight(){
        ButtonRight = false;
    }
    public void buttonJump(){
        do
        {
            ButtonJump = true;
        } while (tanah == false);
    }

    // Update is called once per frame
    void Update()
    {

        if(play_again == true)
        {
            transform.position = play;
            play_again=false;
        }

        if(tanah == false)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        info_heart.text = "Nyawa : " + heart.ToString(); //Heart yaitu Variabel di Atribut Player
        // info_Coin.text = "Promogem : " + coin.ToString();

        if (Input.GetKey (KeyCode.D) || (ButtonRight == true))
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            pindah = -1;
            if (tanah == true)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
        else if (Input.GetKey (KeyCode.A) || (ButtonLeft == true))
        {
            transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            pindah = 1;
            if (tanah == true)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }    
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (tanah==true && Input.GetKeyDown(KeyCode.Space) || (ButtonJump == true))
        {
            float x = lompat.velocity.x;
            lompat.velocity = new Vector2(x, kekuatanlompat);
            //lompat.AddForce(new Vector2(0, kekuatanlompat));
        }
        if (pindah > 0 && !balik)
        {
            flip();
        }
        else if (pindah < 0 && balik)
        {
            flip();
        }
        if (heart < 1)
        {
            gameObject.SetActive(false);
            lose.SetActive(true);
            Debug.Log("Player Wafat");
        }
    }

    
    

    void flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.tag == "Monster"){
            Debug.Log("Monster Trigger");
            if(Input.GetKeyDown(KeyCode.F)){
                StartCoroutine(loadMiniGames("AnimalWord1"));
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Monster2"){
            Debug.Log("Monster Trigger1");
            StopCoroutine(loadMiniGames("AnimalWord1"));
            if(Input.GetKeyDown(KeyCode.F)){
                StartCoroutine(loadMiniGames("AnimalWord2"));
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Monster3"){
            Debug.Log("Monster Trigger2");
            StopCoroutine(loadMiniGames("AnimalWord2"));
            if(Input.GetKeyDown(KeyCode.F)){
                StartCoroutine(loadMiniGames("AnimalWord3"));
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Monster4"){
            Debug.Log("Monster Trigger3");
            StopCoroutine(loadMiniGames("AnimalWord3"));
            if(Input.GetKeyDown(KeyCode.F)){
                StartCoroutine(loadMiniGames("AnimalWord4"));
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Monster5"){
            Debug.Log("Monster Trigger4");
            StopCoroutine(loadMiniGames("AnimalWord4"));
            if(Input.GetKeyDown(KeyCode.F)){
                StartCoroutine(loadMiniGames("AnimalWord5"));
                Destroy(other.gameObject);
            }
        }
        
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);   
    }
}
