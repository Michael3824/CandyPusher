// timeOut[ms]–ˆ‚Éˆ—‚ðŽÀs‚·‚é
using UnityEngine;


public class ExampleClass : MonoBehaviour
{


    public float timeOut = 1.0f;
    private float timeElapsed;

    [SerializeField] private GameObject candyPrefab2;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            // Do anything

            timeElapsed = 0.0f;
            GameObject instaniatedCandy = Instantiate(candyPrefab2);
            instaniatedCandy.transform.position = this.transform.position;
        }
    }
}

