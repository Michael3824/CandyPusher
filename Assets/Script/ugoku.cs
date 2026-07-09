using UnityEngine;

public class Move_Sin : MonoBehaviour
{

    private Vector3 pos;

    void Start()

    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(pos.x + Mathf.Sin(Time.time) * 3, pos.y, pos.z);
    }
}