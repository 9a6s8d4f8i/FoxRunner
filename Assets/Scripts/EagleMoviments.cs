using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMoviments : MonoBehaviour {

    public Animator anim;

    public float vel = 6f;
    public Transform EagleTransform;
    private bool podeMovimentar = true;
    public float tempoParado=0;

    public bool PodeMovimentar
    {
        get
        {
            return podeMovimentar;
        }

        set
        {
            podeMovimentar = value;
        }
    }

    // Use this for initialization
    void Start () {

        EagleTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {


        if (!PodeMovimentar)
        {
            tempoParado += Time.deltaTime;
            if (tempoParado > 2.5f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            EagleTransform.Translate(new Vector2(-vel * Time.deltaTime, 0));

        }

    }

    public void explodeEnemy(bool continua)
    {
        anim.SetBool("morto", true);
        if (!continua)
            podeMovimentar = false;
        else
            vel = 4f;

    }
    
}
