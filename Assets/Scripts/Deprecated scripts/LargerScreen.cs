using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargerScreen : MonoBehaviour
{
    public Renderer screenReference;
    private Renderer screenRenderer;
    // Start is called before the first frame update
    void Start()
    {
        screenRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Material matReference = screenReference.material;
        screenRenderer.material = matReference;
    }
}
