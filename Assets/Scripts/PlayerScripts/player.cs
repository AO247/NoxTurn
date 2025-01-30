using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _deathTime = 15f;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private ParticleSystem deathParticle;
    public Animator animator;
    private float _time = 1;
    private Vector3 _input;
    //private Renderer _renderer;
    private Color _defaultColor;
    private bool _isGrounded = true;
    private Vector3 _basicScale;
    bool isDead = false;
    public int shadowExposure = 0;
    private Rigidbody _rb;
    public GameObject armature;

    public float normalScale = 100f; // Normalna wielko��
    public float minScale = 0.1f; // Minimalna wielko��
    public float scalingSpeed = 1f; // Szybko�� skalowania


    private Color originalColor = Color.black; 
    private Color targetColor = Color.white;
    private Vector3 targetScale;
    public bool _isImmortal = false;
    private Gamepad pad;
    private Coroutine stopRumbleAfterTime;
    AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        armature.SetActive(true);
        _renderer.enabled = true;
    }
    
    private void Start()
    {
        
        //_renderer = GetComponent<Renderer>();
        originalColor = _renderer.material.color;
        _defaultColor = _renderer.material.color;
        _basicScale = transform.localScale;
        _time = _deathTime;
        Physics.gravity = new Vector3(0, -50.81f, 0);
        targetScale = Vector3.one * 1f; // Ustaw pocz�tkow� skal�


    }
    private void Update()
    {
        GatherInput();
        Look();
        isInShadows();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void isInShadows()
    {
        if (!_isImmortal && !isDead)
        {


            if (shadowExposure == 0)
            {
                targetScale = Vector3.one * normalScale; // Powr�t do normalnej wielko�ci
            }
            else
            {
                // Im wi�ksze shadowExposure, tym mniejsza skala
                float scaleFactor = Mathf.Lerp(normalScale, minScale, shadowExposure / 8f);
                targetScale = Vector3.one * scaleFactor;
            }

            // Stopniowa zmiana skali
            armature.transform.localScale = Vector3.Lerp(armature.transform.localScale, targetScale, Time.deltaTime * scalingSpeed);


            if (shadowExposure == 0)
            {
                RumblePulse2(0f, 0f);
                if (_time < _deathTime)
                {
                    _time += Time.deltaTime * 2;

                }
                //audioManager.StopPlaying();
            }
            else if (shadowExposure == 1)
            {
            }
            else if (shadowExposure == 2)
            {
                RumblePulse2(0.07f, 0.07f);
            }
            else if (shadowExposure == 3)
            {
                RumblePulse2(0.2f, 0.2f);
            }
            else if (shadowExposure == 4)
            {
                RumblePulse2(0.4f, 0.4f);
            }
            //else if (shadowExposure > 0 && shadowExposure < 5)
            //{
            //    RumblePulse2(0.5f, 0.5f);
            //    //audioManager.PlaySFXInLoop(audioManager.scorching);
            //    //float scaleFactor = (float)(_time / _deathTime);
            //    //armature.transform.localScale = Vector3.Lerp(armature.transform.localScale, _basicScale * scaleFactor, Time.deltaTime * 5);
            //}
            else if (shadowExposure == 5)
            {
                _time -= Time.deltaTime * shadowExposure;
            }

            //_time = Mathf.Clamp(_time, 0, _deathTime);
            //float t = _time / _deathTime; // Przeskaluj _time na przedzia� [0, 1]
            //Color currentColor = Color.Lerp(targetColor, originalColor, t);

            // Ustaw kolor materia�u obiektu
           //_renderer.material.color = currentColor;
            if (_time <= 0)
            {
                //transform.localScale += new Vector3(0, 0, 0);
                StartCoroutine(HandleDeath());                
                //audioManager.PlaySFX(audioManager.death);
                //_time = _deathTime;
                //shadowExposure = 0;
                //_renderer.material.color = originalColor;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }



            //if (_time <= _deathTime)
            //{
            //    _time -= Time.deltaTime;

            //}
            //if (_time < _deathTime - 0.05)
            //{
            //    float scaleFactor = (float)(_time / _deathTime);
            //    transform.localScale = Vector3.Lerp(transform.localScale, _basicScale * scaleFactor, Time.deltaTime * 5);

            //}
            //else
            //{
            //    if (transform.localScale.x < _basicScale.x)
            //    {
            //        transform.localScale = Vector3.Lerp(transform.localScale, _basicScale, Time.deltaTime * 5);
            //    }
            //    else
            //    {
            //        transform.localScale = _basicScale;
            //    }
            //    _renderer.material.color = _defaultColor;
            //}
        }
        else
        {
            RumblePulse2(0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (_rb.linearVelocity.magnitude > 0.1) { 
                animator.SetBool("isWalking", true);
                audioManager.PlaySFXInLoop(audioManager.footsteps);
            }
            else
            {
                animator.SetBool("isWalking", false);
                audioManager.StopPlaying();
            }

            Move();
            if (!_isGrounded && _rb.linearVelocity.y < 0) // Posta� opada
            {
                _rb.linearVelocity += Vector3.up * Physics.gravity.y * 3.0f * Time.fixedDeltaTime;
                // 1.5f to wsp�czynnik "przyspieszonego" opadania, mo�esz go dostosowa�
            }
            else if (!_isGrounded && _rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                // Utrzymuj ni�szy skok po puszczeniu przycisku (efekt "kr�tkiego skoku")
                _rb.linearVelocity += Vector3.up * Physics.gravity.y * 0.8f * Time.fixedDeltaTime;
            }
        }

    }

    private void GatherInput()
    {
        if (!isDead)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && _isGrounded)
            {
                Jump();
            }
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 velocity = transform.forward * _input.normalized.magnitude * _speed;
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }
    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false; // Blokujemy mo�liwo�� skoku w powietrzu
    }

    private IEnumerator HandleDeath()
    {
        // Odtw�rz d�wi�k �mierci
        // audioManager.StopPlaying();
        _rb.linearVelocity = Vector3.zero;
        audioManager.PlaySFX(audioManager.death);
        isDead = true;
        _time = _deathTime;
        shadowExposure = 0;
        armature.SetActive(false);
        RumblePulse(1f, 1f, 0.2f);
        _renderer.enabled = false;
        deathParticle.Play();
        // Poczekaj na zako�czenie d�wi�ku (lub ustalony czas
        yield return new WaitForSeconds(audioManager.death.length);

        // Resetuj w�a�ciwo�ci postaci
        _renderer.material.color = originalColor;

        // Prze�aduj scen�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void RumblePulse2(float lowFrequency, float highFrequency)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }
    private void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            stopRumbleAfterTime = StartCoroutine(StopRumble(duration, pad));

        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0f, 0f);
    }


}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}

