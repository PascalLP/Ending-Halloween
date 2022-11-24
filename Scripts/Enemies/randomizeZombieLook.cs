using System.Collections;
using UnityEngine;

public class randomizeZombieLook : MonoBehaviour
{

    public Material[] zombieMaterials;

    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer mRenderer = GetComponent<SkinnedMeshRenderer>();
        mRenderer.material = zombieMaterials[Random.Range(0, zombieMaterials.Length)];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
