using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class TurningBlob : MonoBehaviour
{
    private Rigidbody _rb;
    public bool _blob = false;
    private Renderer _renderer;
    private player _player;
    public GameObject hair;
    float upperY, lowerY;
    private Animator animator;
    private bool stopMoving = false;
    private bool stopMovingOne = false;
    public InputActionReference worldLeft;
    public InputActionReference worldRight;
    public DecalProjector projector;
    Vector3 originalSize;
    float time = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _player = GetComponent<player>();
        animator = GetComponent<Animator>();
        originalSize = projector.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovingOne)
        {
            time += Time.deltaTime;
            if (time > 0.6f)
            {
                stopMovingOne = false;
                hair.SetActive(true);
                projector.size = new Vector3(originalSize.x + 0.8f, originalSize.y + 0.8f, originalSize.z);

                time = 0.0f;
            }
        }
        if (stopMoving)
        {
            time += Time.deltaTime;
            if (time > 0.2f)
            {
                stopMoving = false;
                time = 0.0f;
            }
        }
        // Gdy wciskamy Q lub joystick przycisk, aktywujemy tryb blob,
        // blokuj¹c mo¿liwoœæ ruchu gracza (przyjmujemy, ¿e masz flagê canMove w skrypcie player)
        if (worldLeft.action.triggered || worldRight.action.triggered)
        {
            stopMovingOne = true;
            _player.canMove = false;  // blokujemy ruch
            _blob = true;
            _player.isImmortal = true;
            _rb.linearVelocity = Vector3.zero;
            animator.SetBool("isJumpIn", true);
            //stopMoving = true;
        }
    
        // Gdy gracz siê "wyskakuje" – czyli gdy prêdkoœæ nie jest ju¿ zerowa,
        // przywracamy mo¿liwoœæ ruchu.
        if (_blob)
        {
            _rb.linearVelocity = Vector3.zero;
            // U¿ywamy metody Compare (upewnij siê, ¿e metoda Compare porównuje z odpowiednim marginesem)
            if ((_player._input.x != 0.0f || _player._input.z != 0.0f) && !stopMovingOne)
            {
                Debug.Log("Blob");
                animator.SetBool("isJumpIn", false);
                hair.SetActive(false);
                projector.size = new Vector3(originalSize.x, originalSize.y, originalSize.z);
                _blob = false;
                stopMoving = true;

                //_player.isImmortal = false;
                //_player.canMove = true;  // odblokowujemy ruch
            }
        }
        if(!stopMoving && !_blob)
        {
            _player.isImmortal = false;
            _player.canMove = true;  // odblokowujemy ruch
        }
    }
}
