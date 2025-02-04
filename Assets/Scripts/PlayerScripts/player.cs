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
    [SerializeField] private PlayerSound playerSound;
    public Animator animator;
    private float _time = 1;
    private Vector3 _input;
    //private Renderer _renderer;
    private Color _defaultColor;
    private bool _isGrounded = true;
    private Vector3 _basicScale;
    public int shadowExposure = 0;
    private Rigidbody _rb;
    public GameObject armature;
    public float normalScale = 100f; // Normalna wielkość
    public float minScale = 0.1f; // Minimalna wielkość
    public float scalingSpeed = 1f; // Szybkość skalowania


    private Color originalColor = Color.black; 
    private Color targetColor = Color.white;
    private Vector3 targetScale;
    private Gamepad pad;
    private Coroutine stopRumbleAfterTime;
    AudioManager audioManager;
    
    public InputActionReference moveAction;



    public bool isImmortal = false;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isPaused = false;
    public bool isStanding = false;



    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        
        //_renderer = GetComponent<Renderer>();
        originalColor = _renderer.material.color;
        _defaultColor = _renderer.material.color;
        _basicScale = transform.localScale;
        _time = _deathTime;
        Physics.gravity = new Vector3(0, -50.81f, 0);
        targetScale = Vector3.one * 1f; // Ustaw początkową skalę


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
        if (!isImmortal && !isDead)
        {


            if (shadowExposure == 0)
            {
                AkSoundEngine.SetState("Life", "Safe");
                targetScale = Vector3.one * normalScale; // Powrót do normalnej wielkości
            }
            else
            {
                AkSoundEngine.SetState("Life", "Danger");
                // Im większe shadowExposure, tym mniejsza skala
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

            else if (shadowExposure == 5)
            {
                _time -= Time.deltaTime * shadowExposure;
            }


            if (_time <= 0)
            {
                AkSoundEngine.SetState("Life", "Safe");
                StartCoroutine(HandleDeath());
            }

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
            if (!_isGrounded && _rb.linearVelocity.y < 0) // Postać opada
            {
                _rb.linearVelocity += Vector3.up * Physics.gravity.y * 3.0f * Time.fixedDeltaTime;
                // 1.5f to współczynnik "przyspieszonego" opadania, możesz go dostosować
            }
            else if (!_isGrounded && _rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                // Utrzymuj niższy skok po puszczeniu przycisku (efekt "krótkiego skoku")
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
                //Jump();
            }
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
    }

    private void Look()
    {
        // Pobierz kierunek ruchu względem kamery
        Vector3 moveDirection = GetCameraRelativeInput();

        if (moveDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 moveDirection = GetCameraRelativeInput();
        Vector3 velocity = moveDirection.normalized * _input.magnitude * _speed;
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }
    
    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false; // Blokujemy możliwość skoku w powietrzu
    }
    private Vector3 GetCameraRelativeInput()
    {
        // Pobierz kierunek "do przodu" kamery (bez składowej Y)
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Pobierz kierunek "w prawo" kamery (bez składowej Y)
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Oblicz kierunek ruchu względem kamery
        Vector3 moveDirection = (cameraRight * _input.x + cameraForward * _input.z);
        return moveDirection;
    }
    private IEnumerator HandleDeath()
    {
        // Odtwórz dźwięk śmierci
        // audioManager.StopPlaying();


        _rb.linearVelocity = Vector3.zero;
        audioManager.PlaySFX(audioManager.death);
        animator.SetBool("isDead", true);
        //audioManager.PlaySFX(audioManager.death);
        isDead = true;
        _time = _deathTime;
        shadowExposure = 0;
        //PlayerVisibility(false);
        RumblePulse(1f, 1f, 0.2f);
        yield return new WaitForSeconds(.5f);

        playerSound.PlayDeath();
        deathParticle.Play();
        yield return new WaitForSeconds(.2f);
        PlayerVisibility(false);
        // Poczekaj na zakończenie dźwięku (lub ustalony czas
        yield return new WaitForSeconds(audioManager.death.length);

        // Resetuj właściwości postaci
        _renderer.material.color = originalColor;

        // Przeładuj scenę
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerVisibility(bool isVisible)
    {
        armature.SetActive(isVisible);
        _renderer.enabled = isVisible;
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

    private void PlayFootstep()
    {
        playerSound.PlayFootstepSound();
    }


}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}

