using UnityEngine;
using UnityEngine.Tilemaps;

public class HorrorEventBase : MonoBehaviour, ISetUp
{
    [SerializeField]
    protected float speed;
   
    void Start()
    {
        
    }

    
    protected virtual void Update()
    {
        //子クラスで処理を書く
        Debug.Log("子クラス");
    }

    public virtual void SetUp(Transform target, Tilemap tilemap)
    {
        
    }
}
