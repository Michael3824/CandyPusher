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
        Debug.Log("ゲームが開始したよ");
    }
    public Rigidbody rb; 
    // Update is called once per frame
    void Update()
    {
        float z = Mathf.Sin(Time.time * speed) * movepower;
        rb.linearVelocity = new Vector3(0, 0, z);
    }
}
