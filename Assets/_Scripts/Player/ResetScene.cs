using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    private ButtonEvent _buttonEvent;
    private Rigidbody _rb;
    public GameObject ObjectOfZeroPosition;

    private void Start()
    {
        _buttonEvent = FindObjectOfType<ButtonEvent>();
        _rb = GetComponent<Rigidbody>();
        _buttonEvent.OnRestarted.AddListener(ResetPosition);
    }

    private void ResetPosition()
    {
        transform.localPosition = ObjectOfZeroPosition.transform.position;
        _rb.velocity = Vector3.zero;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}