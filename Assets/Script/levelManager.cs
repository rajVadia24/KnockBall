using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    private ShapeData shapeData;
    public List<GameObject> level = new List<GameObject>();
    public Transform pos;
    private ShapeData data;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<levelPrefabs.Length;i++)
        {
           GameObject levels = Instantiate(levelPrefabs[i], pos.position, Quaternion.identity, pos);
            levels.SetActive(false);
            level.Add(levels);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
}
