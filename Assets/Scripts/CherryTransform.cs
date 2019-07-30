using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryTransform : MonoBehaviour {
    public float vel = 4f;
    public Transform cherryTransform;
    private bool podeMovimentar = true;

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
    void Start()
    {
        cherryTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(podeMovimentar)
        cherryTransform.Translate(new Vector2(-vel * Time.deltaTime, 0));
    }

   


}
