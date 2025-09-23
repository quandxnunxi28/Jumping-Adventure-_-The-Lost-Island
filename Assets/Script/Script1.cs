using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script1 : MonoBehaviour
{
    [SerializeField]float tocdo = 5f;
    [SerializeField] float xoayvong = 20f; 
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float d = Input.GetAxis("Vertical") * tocdo * Time.deltaTime;
        float chuyenhuong = Input.GetAxis("Horizontal") * xoayvong * Time.deltaTime;
        transform.Rotate(0, 0, -chuyenhuong);  // âm để xoay đúng chiều
        transform.Translate(0, d, 0); // di chuyển trong local space
    }
}
