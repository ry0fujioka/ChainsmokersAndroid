using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGenerator : Singleton<SmokeGenerator>
{
    public Material material;
    public GameObject area;
    //public Dictionary<GameObject, Vector3> meshPositions = new Dictionary<GameObject, Vector3>();
    public List<KeyValuePair<GameObject, Vector3>> meshPositions = new List<KeyValuePair<GameObject, Vector3>>();

    float moveTotal;
    Vector2 lastPos;

    private List<GameObject> objectPool;
    [SerializeField]
    private GameObject smoke;
    public float smokeRemainTime = 3;
    [SerializeField]
    int maxCount = 200;

    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
        GetComponent<TrailRenderer>().time = smokeRemainTime;
    }
    private void OnEnable()
    {
        PlayerHealth.Instance.onDeath += Reset;
    }

    private void OnDisable()
    {
        //if (gameObject.scene.isLoaded)
        //{
        //    return;
        //}
        //if (PlayerHealth.Instance != null)
        //    PlayerHealth.Instance.onDeath -= Reset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diffPos = new Vector2(this.transform.position.x, this.transform.position.y) - lastPos;
        moveTotal += diffPos.magnitude;
        GenerateSmoke();
        lastPos = this.transform.position;
    }

    void CreatePool()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = Instantiate(smoke, this.transform);
            newObj.name = i.ToString();
            objectPool.Add(newObj);
        }
    }

    public void GenerateSmoke()
    {
        if (moveTotal > 0.8f)
        {
            moveTotal = 0;
            foreach(var smoke in objectPool)
            {
                if(smoke.GetComponent<Smoke>().isVisible == false)
                {
                    smoke.GetComponent<Smoke>().Initialize(smokeRemainTime);
                    smoke.transform.position = this.transform.position;
                    smoke.transform.rotation = this.transform.rotation;
                    KeyValuePair<GameObject, Vector3> checkPair = new KeyValuePair<GameObject, Vector3>(smoke, smoke.transform.position);
                    if(!meshPositions.Contains(checkPair))
                    {
                        meshPositions.Insert(0, checkPair);
                    }
                    return;
                }
            }
            var newObj = Instantiate(smoke, this.transform);
            objectPool.Add(newObj);
            newObj.GetComponent<Smoke>().Initialize(smokeRemainTime);
            newObj.transform.position = this.transform.position;
            newObj.transform.rotation = this.transform.rotation;
            KeyValuePair<GameObject, Vector3> newPair = new KeyValuePair<GameObject, Vector3>(newObj, newObj.transform.position);
            meshPositions.Insert(0, newPair);
        }
    }

    public void OnTouch(GameObject hitObject)
    {
        Mesh mesh = new Mesh();
        KeyValuePair<GameObject, Vector3> hitObjectPair = new KeyValuePair<GameObject, Vector3>(hitObject, hitObject.transform.position);
        int size = 0;
        foreach(var obj in meshPositions)
        {
            if (obj.Key == hitObject)
            {
                break;
            }
            size++;
        }
        if (size >= 3)
        {
            Vector3[] vertices = new Vector3[size];
            int verticesIndex = 0;
            foreach (var obj in meshPositions)
            {
                if (obj.Key == hitObject)
                {
                    break;
                }
                vertices[verticesIndex] = obj.Value;
                verticesIndex++;
            }
            int[] triangles = new int[(size - 2) * 3];
            int vertice = 1;
            for (int i = 2; i <= (size - 2) * 3; i += 3)
            {
                triangles[i - 2] = 0;
                triangles[i - 1] = vertice;
                triangles[i] = vertice + 1;
                vertice++;
            }
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            area.GetComponent<MeshFilter>().mesh = mesh;
            area.GetComponent<MeshRenderer>().sharedMaterial = material;
            area.GetComponent<MeshCollider>().sharedMesh = mesh;
            area.GetComponent<MeshCollider>().enabled = true;
            StartCoroutine("DisableArea");
        }
    }

    IEnumerator DisableArea()
    {
        yield return new WaitForSeconds(0.1f);
        Reset();
    }

    private void Reset()
    {
        area.GetComponent<MeshFilter>().mesh = null;
        area.GetComponent<MeshRenderer>().sharedMaterial = null;
        area.GetComponent<MeshCollider>().sharedMesh = null;
        area.GetComponent<MeshCollider>().enabled = false;
    }
}
