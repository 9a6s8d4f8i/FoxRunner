using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform[] transformInstEnemy;
    public float velInstantiateEnemy=3f;
    public float velInstantiateCherry=1f;
    public int qtdInstanciadores;
    public GameObject Eagle;
    public GameObject Cherry;
    public float contadorTempoInstEnemy=0;
    public float contadorTempoInstCherry=0;
    public Text textScore;
    public string scoreGame="00001";
   public ParallaxController parallax;
        public Image imagemCherry;
    public float currentFillCherry = 0;
    public Animator animCherry;
    public PlayerMoviments Player;



    // Use this for initializatio

    void Start () {
        
        GameObject[] instanciadoresEnemy = GameObject.FindGameObjectsWithTag("enemyInst");
        imagemCherry.fillAmount = 0;
        qtdInstanciadores = instanciadoresEnemy.Length;
        transformInstEnemy = new Transform[qtdInstanciadores];
        for(int i=0; i< qtdInstanciadores; i++)
        {
            transformInstEnemy[i] = instanciadoresEnemy[i].GetComponent<Transform>();
        }
        textScore.text = scoreGame;




    }
    // Update is called once per frame
    void Update()
    {

        cherryFillIncrement();
        contadorTempoInstEnemy += Time.deltaTime;
        contadorTempoInstCherry += Time.deltaTime;

        if (!Player.growUpAtivado)
        {
            parallax.movimentaFundo();
            verificaSeInstanciaEnemy();
            verificaSeInstanciaCherry();


        }
        else
        {
            paralizaElementosTela();

        }



    }

    private void paralizaElementosTela()
    {
        if (parallax.IsMoving)
        {
            parallax.paralizaFundo();
            defineEstadoItensNatela();
        }
        // paraliza fundo

    }

    private void defineEstadoItensNatela()
    {
        EagleMoviments[] enemys = FindObjectsOfType<EagleMoviments>() as EagleMoviments[];
        CherryTransform[] itens = FindObjectsOfType<CherryTransform>() as CherryTransform[];
        foreach (CherryTransform a in itens)
        {
            a.PodeMovimentar = false;
        }
        foreach (EagleMoviments a in enemys)
        {
            a.explodeEnemy(false);
        }
    }




    private void verificaSeInstanciaCherry()
    {

        if (contadorTempoInstCherry > velInstantiateCherry)
        {
            instanciaCherry();
            contadorTempoInstCherry = 0;
        }


    }

    private void verificaSeInstanciaEnemy()
    {
        if (contadorTempoInstEnemy > velInstantiateEnemy)
        {
            instanciaEnemy();
            contadorTempoInstEnemy = 0;
            incrementScore();
        }
    }


    private void cherryFillIncrement()
    {
        if (imagemCherry.fillAmount < 0.99f)////BUSCAR FORMA MELHOR DE FAZER... >> FILL AMOUNT NÃO CHEGA EM 1.
        {
            currentFillCherry = Player.getCurrentFill();

            imagemCherry.fillAmount = Mathf.Lerp(imagemCherry.fillAmount,currentFillCherry, 1.5f * Time.deltaTime);
        }
        else
        {
            animCherry.SetBool("Carregado", true);
            //cherryCarregado = true;
            currentFillCherry = 0;

        }
    }
    void incrementScore()
    {

        scoreGame = (int.Parse(scoreGame) + 1).ToString();
        textScore.text = scoreGame;
        if (scoreGame.Length == 1) textScore.text = "0000" + scoreGame;
        else if (scoreGame.Length == 2) textScore.text = "000" + scoreGame;
        else if (scoreGame.Length == 3) textScore.text = "00" + scoreGame;
        else if (scoreGame.Length == 4) textScore.text = "0" + scoreGame;


    }
    void instanciaCherry()
    {

        int randonInst = Random.Range(0, 2);
        Instantiate(Cherry, new Vector3(transformInstEnemy[randonInst].position.x, transformInstEnemy[randonInst].position.y, 0), Quaternion.identity);

    }

    void instanciaEnemy()
    {

        int randonInst = Random.Range(0, 2);
        Instantiate(Eagle, new Vector3(transformInstEnemy[randonInst].position.x, transformInstEnemy[randonInst].position.y, 0), Quaternion.identity);

    }
}
