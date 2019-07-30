using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoviments : MonoBehaviour
{

    public float speed = 2.5f;
    public float force = 5f;
    public bool canJump = true;
    
    public bool face = true;
    public bool alive = true;
    public Animator anim;

   public bool poderAtivo = false;
    public int collectedGems;

    public AudioClip gemsAudio;
    public AudioClip playerDamage;
    public AudioClip jumpAudio;
    public AudioClip getCherryAudio;
    public AudioClip powerAtivo;

    //UI Interface
    public bool cherryCarregado;
    public float currentFill = 0;
    public GameObject player;
    private bool especialAtiv = false;

    public bool growUpAtivado { get; private set; }
    public float tempoGrowUp =  0.1f;



    // Use this for initialization
    void Start()
    {
        //    player = this.gameObject;
        collectedGems = 0;
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {


        if (alive)
        {
            playerJump();
            soltaEspecial();
            if (poderAtivo)
            {
                tempoGrowUp -= Time.deltaTime;
                if (tempoGrowUp < 0)
                {
                    growUpAtivado = true;

                    if (!anim.GetBool("idle"))
                    {
                        anim.SetBool("idle", true);
                        anim.SetBool("run", false);
                    }
                    
                        growUp(5f);
                    
                    

                }

            }
            

        }
       


    }

  
    private void playerJump()
    {
        if (canJump)
        {

                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Input.GetAxis("Jump") * force), ForceMode2D.Impulse);
        }
    }
    
    private void soltaEspecial()
    {
        if (Input.GetKey(KeyCode.LeftControl) && cherryCarregado )
        {

            growUpAtivado = true;
        }
        if(growUpAtivado == true)
        {
            if (player.transform.localScale.x  < 9f)
            {
                if(!anim.GetBool("idle"))
                {
                    AudioManager.inst.PlayAudio(powerAtivo);
                    anim.SetBool("idle", true);
                    anim.SetBool("run", false);
                }else
                growUp(10f);
            }
            else
            {
                poderAtivo = true;
                growUpAtivado = false;
                anim.SetBool("run", true);
                anim.SetBool("idle", false);
            }
        }

    }

   private void growUp(float tamanhoDesejado)
    {
        Vector3 scale = new Vector3(tamanhoDesejado, tamanhoDesejado, 10f);
            player.transform.localScale = Vector3.Lerp(player.transform.localScale, scale, 0.5f * Time.deltaTime);
        


    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            anim.SetBool("jump", true);
            anim.SetBool("run", false);

            AudioManager.inst.PlayAudio(jumpAudio);
            canJump = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            anim.SetBool("jump", false);
            anim.SetBool("run", true);
            canJump = true;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gem"))
        {
            collectedGems++;
            AudioManager.inst.PlayAudio(gemsAudio);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("eagle"))
        {
            AudioManager.inst.PlayAudio(playerDamage);
            GetComponent<Animator>().SetBool("dead", true);
            GetComponent<Animator>().SetBool("run", false);
            GetComponent<Animator>().SetBool("jump", false);
            GetComponent<Animator>().SetBool("idle", false);
            EagleMoviments eagle = collision.gameObject.GetComponent<EagleMoviments>();
            eagle.explodeEnemy(true);
            alive = false;

        }

        else if (collision.gameObject.CompareTag("cherry"))
        {
            if (currentFill < 1)
            {
                AudioManager.inst.PlayAudio(getCherryAudio);
                currentFill += 0.25f;
                Destroy(collision.gameObject);
            }else
            {
                cherryCarregado = true;
            }
        }
    }

    public float getCurrentFill()
    {
        return currentFill;
    }

}




















/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoviments : MonoBehaviour {

    public float speed = 2.5f;
    public float force = 400f;
    public bool canJump = true;
    public AudioClip jumpAudio;
    public bool face= true;
    public bool alive = true;
    public Animator anim;
    
    public int collectedGems;
    public AudioClip gemsAudio;
    public AudioClip playerDamage;

    //UI Interface
    public Image imagemCherry;
    public bool cherryCarregado;
    public float currentFill = 0;
    public Animator animCherry;
        


    // Use this for initialization
    void Start () {
        imagemCherry.fillAmount = 0;
        collectedGems = 0;
        anim = GetComponent<Animator>();
	}
	
    

	// Update is called once per frame
	void Update () {


        if (alive)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("run", true);
                anim.SetBool("idle", false);
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                if (!face)
                    Flip();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("run", true);
                anim.SetBool("idle", false);
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                if (face)
                    Flip();
            }
            else
            {
                if(canJump)
                    anim.SetBool("idle", true);
                anim.SetBool("run", false);
                
            }
            if (canJump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force * Time.deltaTime), ForceMode2D.Impulse);
                    
                }
            }
                
        }
        if(imagemCherry.fillAmount <0.99f)////BUSCAR FORMA MELHOR DE FAZER... >> FILL AMOUNT NÃO CHEGA EM 1.
        {

                imagemCherry.fillAmount = Mathf.Lerp(imagemCherry.fillAmount, currentFill, 1.5f * Time.deltaTime);
        }else{
            animCherry.SetBool("Carregado", true);
                cherryCarregado = true;
            currentFill = 0;

        }
      
        
    }

    void Flip()
    {
        face = !face;
     Vector3 scale = GetComponent<Transform>().localScale;
        scale.x = scale.x * -1;
        GetComponent<Transform>().localScale = scale;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor")){
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);

            AudioManager.inst.PlayAudio(jumpAudio);
            canJump = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor")){
            anim.SetBool("jump", false);
            anim.SetBool("idle", true);
            canJump = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gem"))
        {
            collectedGems++;
            AudioManager.inst.PlayAudio(gemsAudio);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("eagle"))
        {
            AudioManager.inst.PlayAudio(playerDamage);
            GetComponent<Animator>().SetBool("dead", true);
            GetComponent<Animator>().SetBool("run", false);
            GetComponent<Animator>().SetBool("jump", false);
            GetComponent<Animator>().SetBool("idle", false);
            alive = false;

        }else if (collision.gameObject.CompareTag("cherry"))
        {
            if (imagemCherry.fillAmount < 1)
            {
                currentFill += 0.25f;
            }
        }
    }

}
    */

