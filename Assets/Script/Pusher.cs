using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float speed = 1f;
    public float movepower = 5f;
        private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()  
    {
        startPosition = this.transform.position;
        Debug.Log("ÉQÅ[ÉÄÇ™äJénÇµÇΩÇÊ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
