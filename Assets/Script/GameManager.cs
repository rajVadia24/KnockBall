using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    public Transform PointToShoot;
    public float ballForce;
    public Transform targetDir;
    public bool ReadyToShoot;
    Plane plane = new Plane(Vector3.forward, 0f);
    
    public int currentLevel=0;
    public int TotalBall;
    public ShapeData[] AllLevel;
    public Transform levelPos;
    private GameObject level;
    public List<GameObject> levels = new List<GameObject>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    private void Start()
    {
        ReadyToShoot = true;
        for(int i=0;i<AllLevel.Length;i++)
        {
            level = Instantiate(AllLevel[i].prefab, levelPos.position, Quaternion.identity, levelPos);
            level.SetActive(false);
            levels.Add(level);
        }
        levels[currentLevel].SetActive(true);
    }
    void Update()
    {
      
        Vector3 dir = targetDir.position - ball.transform.position;

        if (Input.GetMouseButtonDown(0) && ReadyToShoot == true)
        {
             Instantiate(ball.GetComponent<Rigidbody>(),PointToShoot.position,Quaternion.identity,PointToShoot).GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);

            // ball.GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);
            ReadyToShoot = false;
            TotalBall--;
            //if(TotalBall<=0)
            //{
            //    //Game Over
            //    Debug.Log("Game Over");
            //}
        }

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out dist))
        {
          
            Vector3 point = ray.GetPoint(dist);
            targetDir.position = new Vector3(point.x, point.y, 0f);
        }
    }



    public void CheckAllObjectIsGrounded()
    {
      if(AllGrounded())
        {
            //load next level
            print("Level Complete");
            loadLevel();
        }
    }
    public bool AllGrounded()
    {
        Transform CurrentLevel = levels[currentLevel].transform;
        foreach(Transform t in CurrentLevel)
        {
            if (t.GetComponent<Shape>().hasFallan == false)
            {
                return false;
            }
        }
        return true;
    }

    public void loadLevel()
    {
        StartCoroutine(loadNextLevelRoutine());
    }

    IEnumerator loadNextLevelRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        ReadyToShoot = false;
        levels[currentLevel].SetActive(false);
        currentLevel++;
        TotalBall = AllLevel[currentLevel].balls;

        if(currentLevel > levels.Count)
        {
            currentLevel = 0;
        }
        yield return new WaitForSeconds(0.5f);
        levels[currentLevel].SetActive(true);
        ReadyToShoot = true;

    }
}
