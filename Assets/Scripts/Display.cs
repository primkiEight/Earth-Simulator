using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Display : MonoBehaviour {

    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public Camera Camera4;
    public GameObject sunLights;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            sunLights.gameObject.SetActive(false);
            Camera1.gameObject.SetActive(true);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(false);
            Camera4.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            sunLights.gameObject.SetActive(true);
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(true);
            Camera3.gameObject.SetActive(false);
            Camera4.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            sunLights.gameObject.SetActive(true);
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(true);
            Camera4.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            sunLights.gameObject.SetActive(true);
            Camera1.gameObject.SetActive(false);
            Camera2.gameObject.SetActive(false);
            Camera3.gameObject.SetActive(false);
            Camera4.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}