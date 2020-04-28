using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100F;
    [SerializeField] float mainThrust = 100F;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // todo somewhere stop sound on death
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotate();
        }
    }

    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            print("Play thrust");
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotate()
    {
        rigidBody.freezeRotation = false; // take manual control of rotation

        // print("deltaTime : " + Time.deltaTime);

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = true; // resume physics control of rotation
    }

    void OnCollisionEnter(Collision collision)
    {
        print( "state " + state );
        if ( state != State.Alive ) { return; } // ignore collisions when dead

        switch (collision.gameObject.tag)
        {
            case "Start":
                print("OK");
                break;
            case "Finish":
                print("1");
                SoundToSuccess();
                break;
            default:
                SoundToDeath();
                break;
        }
    }

    void SoundToSuccess()
    {
        state = State.Transcending;
        print("state after " + state);
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void SoundToDeath()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}