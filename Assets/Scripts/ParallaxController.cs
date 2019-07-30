using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour  {

    public Renderer ceu;
    public Renderer floresta;
    public Renderer chao;
    public float vel_ceu = 0.05f;
    public float vel_floresta = 0.1f;
    public float vel_chao = 0.2f;
    private bool isMoving = true;

    
    // Use this for initialization
    void Start () {
		
	}

    public void movimentaFundo()
    {
        Vector2 off = new Vector2(vel_ceu * Time.deltaTime, 0);
        ceu.material.mainTextureOffset += off;

        off = new Vector2(vel_floresta * Time.deltaTime, 0);
        floresta.material.mainTextureOffset += off;

        off = new Vector2(vel_chao * Time.deltaTime, 0);
        chao.material.mainTextureOffset += off;
        IsMoving = true;
    }
    public void paralizaFundo()
    {
        Vector2 off = new Vector2(0 * Time.deltaTime, 0);
        ceu.material.mainTextureOffset += off;
        floresta.material.mainTextureOffset += off;
        chao.material.mainTextureOffset += off;
        IsMoving = false;
    }
    public bool IsMoving
    {
        get
        {
            return this.isMoving;
        }

        set
        {
            this.isMoving = value;
        }
    }


}
