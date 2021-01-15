using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollField : MonoBehaviour {
    Vector2 offset;
    Material mat;
    MeshRenderer mr;
    public float paralax = 2f;
    // Use this for initialization
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        offset = mat.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        offset.x += Time.deltaTime / transform.localScale.x / paralax;
        mat.mainTextureOffset = offset;
    }
}
