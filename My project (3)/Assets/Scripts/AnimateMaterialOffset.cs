using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMaterialOffset:MonoBehaviour{


    public Material material;
    public float speed=1;
    private Vector2 offset=new Vector2();
    void Update()
    {
        offset.x+=speed*Time.deltaTime;
        this.material.SetTextureOffset("_MainTex",offset );
    }

}