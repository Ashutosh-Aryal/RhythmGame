using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterThing : MonoBehaviour
{
    private enum SpawnLocation
    {
        TopLeft = 0, 
        TopMid = 1, 
        TopRight = 2,
        RightMid = 3,
        BottomRight = 4,
        BottomMid = 5,
        BottomLeft = 6,
        LeftMid = 7
    };

    // const vars
    private const KeyCode UP_KEY = KeyCode.W;
    private const KeyCode DOWN_KEY = KeyCode.S;
    private const KeyCode LEFT_KEY = KeyCode.A;
    private const KeyCode RIGHT_KEY = KeyCode.D;
    private const KeyCode STRUM_KEY = KeyCode.M;
    private const KeyCode ALT_STRUM_KEY = KeyCode.Comma;
    private const KeyCode SHOOT_KEY = KeyCode.Space;

    private const int NUM_SPAWNERS = 8;

    private const float PELLET_SPEED = 7.0f;
    private const float LASER_SPEED = 6.0f;
    private const float QUARTER_NOTE_DELAY = 0.517f;
    private const float EIGHTH_NOTE_DELAY = 0.259f;
    private const float TWENTY_FOURTH_NOTE_DELAY = 0.086f;
    private const float TWELTH_NOTE_DELAY = 0.172f;
    private const float DOTTED_EIGHTH_NOTE_DELAY = 0.388f;
    private const float SIXTEENTH_NOTE_DELAY = 0.129f;

    // static vars
    private static readonly float[] s_NoteLength = { 
        // Measure 1
        EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY,
        // Measure 2
        EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, 
        // Measure 3
        TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, DOTTED_EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, 
        // Measure 4
        TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, 
        // Measure 5
        EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, DOTTED_EIGHTH_NOTE_DELAY, DOTTED_EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, 
        // Measure 6
        EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, 
        // Measure 7
        SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY,
        // Measure 8
        TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY,
        // Measure 9
        EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, 
        // Measure 10
        SIXTEENTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, 
        // Measure 11
        TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, DOTTED_EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY,
        // Measure 12
        TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY,
        // Measure 13
        EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, EIGHTH_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, 
        // Measure 14
        EIGHTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, 
        // Measure 15
        SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, SIXTEENTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, 
        // Measure 16
        TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWENTY_FOURTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, TWELTH_NOTE_DELAY, 
        // Measure 17
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY * 2.0f, QUARTER_NOTE_DELAY, 
        // Measure 18
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY,
        // Measure 19
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY * 2.0f, QUARTER_NOTE_DELAY,
        // Measure 20
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY,
        // Measure 21
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY * 2.0f, QUARTER_NOTE_DELAY,
        // Measure 22
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY,
        // Measure 23
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY * 2.0f, QUARTER_NOTE_DELAY,
        // Measure 24
        QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY, QUARTER_NOTE_DELAY
    };

    private static readonly Vector3 UP_LEFT_ROTATION = new Vector3(-45.0f, 90.0f, 90.0f);
    private static readonly Vector3 UP_ROTATION = new Vector3(0.0f, 90.0f, 90.0f);
    private static readonly Vector3 UP_RIGHT_ROTATION = new Vector3(45.0f, 90.0f, 90.0f);
    private static readonly Vector3 RIGHT_ROTATION = new Vector3(90.0f, 90.0f, 90.0f);
    private static readonly Vector3 DOWN_RIGHT_ROTATION = new Vector3(135.0f, 90.0f, 90.0f);
    private static readonly Vector3 DOWN_ROTATION = new Vector3(180.0f, 90.0f, 90.0f);
    private static readonly Vector3 DOWN_LEFT_ROTATION = new Vector3(225.0f, 90.0f, 90.0f);
    private static readonly Vector3 LEFT_ROTATION = new Vector3(270.0f, 90.0f, 90.0f);

    private static readonly Vector3 LASER_ROTATION = new Vector3(-90.0f, 0.0f, -90.0f);
    private static readonly Vector3 LASER_UP_LEFT_ROTATION = new Vector3(0.0f, 0.0f, 43.5f);
    private static readonly Vector3 LASER_UP_ROTATION = new Vector3(0.0f, 0.0f, -1.5f);
    private static readonly Vector3 LASER_UP_RIGHT_ROTATION = new Vector3(0.0f, 0.0f, -46.5f);
    private static readonly Vector3 LASER_RIGHT_ROTATION = new Vector3(0.0f, 0.0f, -91.5f);
    private static readonly Vector3 LASER_DOWN_RIGHT_ROTATION = new Vector3(0.0f, 0.0f, -136.5f);
    private static readonly Vector3 LASER_DOWN_ROTATION = new Vector3(0.0f, 0.0f, -226.5f);
    private static readonly Vector3 LASER_DOWN_LEFT_ROTATION = new Vector3(0.0f, 0.0f, -271.5f);
    private static readonly Vector3 LASER_LEFT_ROTATION = new Vector3(0.0f, 0.0f, 88.5f);

    private static readonly Dictionary<float, HashSet<SpawnLocation>> s_GetAllowedSpawnersFor = new Dictionary<float, HashSet<SpawnLocation>> {
        {
            QUARTER_NOTE_DELAY,
            new HashSet<SpawnLocation> { // all spawners available
                SpawnLocation.TopLeft, 
                SpawnLocation.BottomLeft,
                SpawnLocation.BottomMid,
                SpawnLocation.BottomRight,
                SpawnLocation.LeftMid, 
                SpawnLocation.RightMid, 
                SpawnLocation.TopMid,
                SpawnLocation.TopRight
            }
        },
        {
            QUARTER_NOTE_DELAY * 2.0f,
            new HashSet<SpawnLocation> { // all spawners available
                SpawnLocation.TopLeft
            }
        },
        {
            EIGHTH_NOTE_DELAY,
            new HashSet<SpawnLocation> { // top half (including mids) available
                SpawnLocation.LeftMid, 
                SpawnLocation.TopLeft, 
                SpawnLocation.BottomLeft
            }
        },
        {
            DOTTED_EIGHTH_NOTE_DELAY,
            new HashSet<SpawnLocation> { // only bottom corners available
                SpawnLocation.BottomLeft
            }
        },
        {
            SIXTEENTH_NOTE_DELAY, 
            new HashSet<SpawnLocation> { // only mids
                SpawnLocation.TopMid
            }
        },
        {
            TWELTH_NOTE_DELAY, 
            new HashSet<SpawnLocation> { // left side only (including corners)
                SpawnLocation.TopRight,
                SpawnLocation.RightMid
            }
        },
        {
            TWENTY_FOURTH_NOTE_DELAY, 
            new HashSet<SpawnLocation> { // right side only (including corner)
                SpawnLocation.BottomRight, 
                SpawnLocation.BottomMid
            }
        }
    };

    public static int MAX_POINTS = s_NoteLength.Length;

    // non-static vars
    [SerializeField] GameObject m_LaserPrefab;
    [SerializeField] GameObject m_Spawners;
    [SerializeField] GameObject m_PelletPrefab;
    [SerializeField] AudioSource m_MusicAudio;

    private Vector3 m_CurrentDirection = new Vector3(0.0f, 1.0f);
    private float m_StartMusicTimer = 1.0f;
    private float m_CurrentTimer = 0.0f;

    private int m_SongNoteIndex = 0;

    bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // called once every 
    private void FixedUpdate()
    {  
        CheckPressedKeys();
        CreateSoundPellets();

        if (m_StartMusicTimer > 0.0f)
        {
            m_StartMusicTimer -= Time.deltaTime; return;
        }
        else if (!m_MusicAudio.isPlaying && !hasPlayed)
        {
            m_MusicAudio.PlayOneShot(m_MusicAudio.clip);
            hasPlayed = true;
        } else if(!m_MusicAudio.isPlaying && hasPlayed)
        {
            m_StartMusicTimer = 1.0f;
            m_SongNoteIndex = 0;
            hasPlayed = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    private void CreateSoundPellets()
    {
        if (m_CurrentTimer > 0.0f) {
            m_CurrentTimer -= Time.deltaTime; return;
        } else if(m_SongNoteIndex >= s_NoteLength.Length)
        {
            return;
        }

        m_CurrentTimer = s_NoteLength[m_SongNoteIndex];

        HashSet<SpawnLocation> spawnLocations = s_GetAllowedSpawnersFor[m_CurrentTimer];
        int randomSpawnLocationIndex = Random.Range(0, spawnLocations.Count);

        SpawnLocation sl = SpawnLocation.TopLeft;
        int x = 0; 

        foreach(SpawnLocation s in spawnLocations)
        {
            if(x == randomSpawnLocationIndex)
            {
                sl = s; break;
            }
            x++;
        }
        
        Vector3 spawnPosition = m_Spawners.transform.GetChild((int)(sl)).position;

        GameObject newPellet = Instantiate(m_PelletPrefab, spawnPosition, Quaternion.identity);
        SpriteRenderer pelletSpriteRenderer = newPellet.GetComponent<SpriteRenderer>();
        
        newPellet.tag = "greenPellet";
        pelletSpriteRenderer.material.color = new Color(0.0f, 255.0f, 0.0f);

        Rigidbody2D pelletRigidbody = newPellet.GetComponent<Rigidbody2D>();

        Vector3 directionVector3D = gameObject.transform.position - spawnPosition;
        Vector2 directionVector = new Vector2(directionVector3D.x, directionVector3D.y);
        directionVector.Normalize();

        pelletRigidbody.velocity = PELLET_SPEED * directionVector;

        m_SongNoteIndex++;
    }

    private void CheckPressedKeys()
    {
        Vector3 newRotation = gameObject.transform.eulerAngles;

        Vector3 myPosition = gameObject.transform.position;
        Vector3 directionVector = m_CurrentDirection;

        bool isPressingUpKey = Input.GetKey(UP_KEY);
        bool isPressingLeftKey = Input.GetKey(LEFT_KEY);
        bool isPressingRightKey = Input.GetKey(RIGHT_KEY);
        bool isPressingDownKey = Input.GetKey(DOWN_KEY);

        bool isGoingBottomRightCorner = isPressingDownKey && isPressingRightKey;
        bool isGoingBottomLeftCorner = isPressingDownKey && isPressingLeftKey;
        bool isGoingTopRightCorner = isPressingUpKey && isPressingRightKey;
        bool isGoingTopLeftCorner = isPressingUpKey && isPressingLeftKey;

        if(isGoingBottomLeftCorner)
        {
            newRotation = DOWN_LEFT_ROTATION;
            directionVector = -Vector2.one;
        } else if(isGoingBottomRightCorner)
        {
            newRotation = DOWN_RIGHT_ROTATION;
            directionVector = new Vector3(1.0f, -1.0f);
        } else if(isGoingTopLeftCorner)
        {
            newRotation = UP_LEFT_ROTATION;
            directionVector = new Vector3(-1.0f, 1.0f);
        } else if(isGoingTopRightCorner)
        {
            newRotation = UP_RIGHT_ROTATION;
            directionVector = Vector2.one;
        } else if(isPressingDownKey)
        {
            newRotation = DOWN_ROTATION;
            directionVector = new Vector3(0.0f, -1.0f);
        } else if(isPressingUpKey)
        {
            newRotation = UP_ROTATION;
            directionVector = new Vector3(0.0f, 1.0f);
        }  else if(isPressingLeftKey)
        {
            newRotation = LEFT_ROTATION;
            directionVector = new Vector3(-1.0f, 0.0f);
        } else if(isPressingRightKey)
        {
            newRotation = RIGHT_ROTATION;
            directionVector = new Vector3(1.0f, 0.0f);
        }

        directionVector.Normalize();
        m_CurrentDirection = directionVector;
        gameObject.transform.eulerAngles = newRotation;

        if(Input.GetKeyDown(SHOOT_KEY))
        {
            GameObject laserObject = Instantiate(m_LaserPrefab, Vector3.zero, Quaternion.identity);
            laserObject.transform.parent = gameObject.transform;
            laserObject.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
            laserObject.transform.eulerAngles = new Vector3(-90.0f, 0.0f, -90.0f);

            Rigidbody2D laserRigidbody = laserObject.GetComponent<Rigidbody2D>();
            laserRigidbody.velocity = directionVector * LASER_SPEED;
        }

        /* mouse rotation option
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouseWorldPosition.y - gameObject.transform.position.y, mouseWorldPosition.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
        gameObject.transform.eulerAngles = new Vector3(90.0f - angle, 90.0f, 90.0f); */
    }
}