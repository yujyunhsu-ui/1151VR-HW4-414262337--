using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class AirplaneController : MonoBehaviour
{
    [Header("UI設定")]
    public GameObject winUI; // 用來存放過關文字的物件

    [Header("飛行移動設定")]
    public float forwardSpeed = 25f;
    public float pitchSpeed = 60f;
    public float rollSpeed = 80f;
    public float yawSpeed = 40f;

    [Header("音效設定")]
    public AudioSource audioSource;
    public AudioClip flySound;
    public AudioClip hitSound;
    public AudioClip passSound;

    private Rigidbody rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        if (audioSource != null && flySound != null)
        {
            audioSource.clip = flySound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        if (isDead || rb == null) return;

        var keyboard = Keyboard.current;
        float pitchInput = 0f, rollInput = 0f, yawInput = 0f;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) pitchInput = 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) pitchInput = -1f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) rollInput = 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) rollInput = -1f;
            if (keyboard.qKey.isPressed) yawInput = -1f;
            if (keyboard.eKey.isPressed) yawInput = 1f;
        }

        // 物理速度推進 (注意: linearVelocity 是 Unity 6 的新語法，若報錯請改回 velocity)
        rb.linearVelocity = transform.forward * forwardSpeed;

        // 物理旋轉 (已修復原本缺少的乘號)
        Quaternion deltaRotation = Quaternion.Euler(
            pitchInput * pitchSpeed * Time.fixedDeltaTime,
            yawInput * yawSpeed * Time.fixedDeltaTime,
            rollInput * rollSpeed * Time.fixedDeltaTime
        );
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    // 關鍵修改：將 OnTriggerEnter 改為 OnCollisionEnter，這樣才會有實體碰撞，不會穿模
    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        // 判斷撞到的物件標籤是否為 "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            Debug.Log("碰觸到障礙物！準備重新開始...");

            // 關鍵修改：強制凍結飛機，消除所有彈飛與旋轉的反作用力
            rb.isKinematic = true;

            if (audioSource != null && hitSound != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(hitSound);
            }

            Invoke("RestartGame", 1f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        // 判斷穿過的物件標籤是不是 "Finish"
        if (other.gameObject.CompareTag("Finish"))
        {
            isDead = true;
            Debug.Log("抵達終點！恭喜過關！");

            // 讓飛機停下來
            rb.isKinematic = true;

            if (audioSource != null && passSound != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(passSound);
            }

            // 關鍵修改：顯示過關 UI
            if (winUI != null)
            {
                winUI.SetActive(true);
            }

            // 過關後，延遲 2 秒重新開始遊戲
            Invoke("RestartGame", 2f);
        }
    }
    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}