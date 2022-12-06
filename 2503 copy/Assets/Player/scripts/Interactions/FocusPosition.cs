using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Input;

public class FocusPosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Transform Xsign;
    public float sightRange;
    public Transform enemy;
    public bool enemyFound;
    public LayerMask whatIsEnemy;
    public  List<Collider> hitColliders;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    { 
        //find the  point im looking at
        Vector2 centerCamera = new Vector2(Screen.width/2f, Screen.height/2f);
        Ray ray = mainCamera.ScreenPointToRay(centerCamera);
        if(Physics.Raycast(ray, out RaycastHit raycastHit)){
            transform.position = raycastHit.point;
        }
        //check if there is any enemies in range
        enemyFound = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        if(enemyFound){
            hitColliders.Clear();
            hitColliders = new List<Collider>(Physics.OverlapSphere(transform.position, sightRange, whatIsEnemy));
            Xsign.position = hitColliders[0].transform.position;
            
        }else{
            Xsign.position = new Vector3(0,0,0);
        }

    }
}
