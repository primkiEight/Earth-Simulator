using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    private Rigidbody _myRigidBody;
    
    private float _moveX;
    private float _moveY;
    private float _moveZL;
    private float _moveZR;
    private float _moveForward;
    private float _moveUp;
    private float _moveRight;
    private float _brake;
    private float _speedUp;
    private float _speedDown;

    public GameObject ForwardTarget;
    public GameObject UpTarget;
    public GameObject RightTarget;

    private Vector3 _playerPosition;
    private Vector3 _forwardPosition;
    private Vector3 _forwardDirection;
    private Vector3 _upPosition;
    private Vector3 _upDirection;
    private Vector3 _rightPosition;
    private Vector3 _rightDirection;

    public float SpeedUpMax;
    public float ForwardSpeed;
    public float SpeedFactor;
    public float BrakeFactor;
    public float ImpulseFactor;
    
    private float x;
    private float y;
    private float z;
    public float RotationSpeed;

    public Text xText;
    public Text yText;
    public Text zText;
    public Text Speed;

    private ParticleSystem _backEngineParticle;
    
    // Use this for initialization
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();

        _backEngineParticle = GetComponentInChildren<ParticleSystem>();
        _backEngineParticle.Stop();
    }

	// Update is called once per frame
	void Update () {
        _moveY = Input.GetAxisRaw("Horizontal");
        _moveX = Input.GetAxisRaw("Vertical");
        _moveZL = Input.GetAxisRaw("RotateL");
        _moveZR = Input.GetAxisRaw("RotateR");
        _moveForward = Input.GetAxisRaw("MoveForward");
        _moveUp = Input.GetAxisRaw("MoveUp");
        _moveRight = Input.GetAxisRaw("MoveRight");
        _brake = Input.GetAxisRaw("FullBreak");
        _speedUp = Input.GetAxisRaw("SpeedUp");
        _speedDown = Input.GetAxisRaw("SpeedDown");
    }

    private void FixedUpdate()
    {

        transform.rotation = Quaternion.Euler(x, y, z);

        if (_moveY < 0)
        {
            y -= Time.deltaTime * RotationSpeed;
        } else if (_moveY > 0) {
            y += Time.deltaTime * RotationSpeed;
        } 

        if (_moveX > 0)
        {
            x -= Time.deltaTime * RotationSpeed;
        }
        else if (_moveX < 0)
        {
            x += Time.deltaTime * RotationSpeed;
        }

        if (_moveZL > 0)
        {
            z -= Time.deltaTime * RotationSpeed;
        }
        else if (_moveZR > 0)
        {
            z += Time.deltaTime * RotationSpeed;
        }

        if (_speedUp > 0) {
            if (ForwardSpeed < SpeedUpMax)
            {
                ForwardSpeed += ForwardSpeed * Time.deltaTime;
            } else if (ForwardSpeed >= SpeedUpMax)
            {
                ForwardSpeed = SpeedUpMax;
            }
        }

        if (_speedDown > 0) { 
            if (ForwardSpeed > 0.5f)
            {
                ForwardSpeed -= ForwardSpeed * Time.deltaTime;
                if (ForwardSpeed <= 0.5f)
                {
                    ForwardSpeed = 0.4f;
                }
            }
        }

        _playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _forwardPosition = new Vector3(ForwardTarget.transform.position.x, ForwardTarget.transform.position.y, ForwardTarget.transform.position.z);
        _upPosition = new Vector3(UpTarget.transform.position.x, UpTarget.transform.position.y, UpTarget.transform.position.z);
        _rightPosition = new Vector3(RightTarget.transform.position.x, RightTarget.transform.position.y, RightTarget.transform.position.z);
        _forwardDirection = _forwardPosition - _playerPosition;
        _upDirection = _upPosition - _playerPosition;
        _rightDirection = _rightPosition - _playerPosition;

        if (_moveForward < 0)
        {
            _backEngineParticle.Play();
            _myRigidBody.AddForce(_forwardDirection * ForwardSpeed * SpeedFactor/100, ForceMode.Acceleration);            
        } else if (_moveForward > 0)
        {
            _backEngineParticle.Stop();
            _myRigidBody.AddForce(-_forwardDirection * ForwardSpeed * BrakeFactor/100, ForceMode.Acceleration);
        } else
        {
            _backEngineParticle.Stop();
        }

        if (_moveUp < 0)
        {
            _backEngineParticle.Play();
            _myRigidBody.AddForce(_upDirection * ImpulseFactor/100, ForceMode.Impulse);
        }
        else if (_moveUp > 0)
        {
            _backEngineParticle.Stop();
            _myRigidBody.AddForce(-_upDirection * ImpulseFactor/100, ForceMode.Impulse);
        }
        
        if (_moveRight < 0)
        {
            _backEngineParticle.Play();
            _myRigidBody.AddForce(_rightDirection * ImpulseFactor/100, ForceMode.Impulse);
         
        }
        else if (_moveRight > 0)
        {
            _backEngineParticle.Stop();
            _myRigidBody.AddForce(-_rightDirection * ImpulseFactor/100, ForceMode.Impulse);
        }
        
        if (_brake > 0)
        {
            _backEngineParticle.Stop();
            _myRigidBody.Sleep();
        }
    }

    private void LateUpdate()
    {
        xText.text = "X = " + System.Math.Round(-transform.localRotation.x * 10, 2);
        yText.text = "Y = " + System.Math.Round(transform.localRotation.y * 10, 2);
        zText.text = "Z = " + System.Math.Round(transform.localRotation.z * 10, 2);
        Speed.text = "S = " + System.Math.Round(ForwardSpeed, 1);
    }
}
