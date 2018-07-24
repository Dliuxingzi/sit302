using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public GameObject PlayerCamera;
    CharacterController PlayerCon;

    public float PlayerSpeed = 2;
    public float CameraSpeed = 2;
    public float Gravity = 50f;

    public float speed = 2.0f;
    public float toggleAngle = 20.0f;
    public bool moveForward;

    float MoveFrontAndBack = 0;
    float MoveLeftAndRight = 0;

    float CamerRotateX = 0;
    float CamerRotateY = 0;

    
    float FallSpeed = 1;

    private Animator ThisAnimatorCon;

    private const string IDLE_ANIME_BOOL = "idle";
    private const string WALK_ANIME_BOOL = "walking";
    private const string SIT_ANIME_BOOL = "sittingdown";
    private const string TALK_ANIME_BOOL = "talking";

    public float VRSpeeed;
    float ThisVRSpeeed;



    // Use this for initialization
    void Start () {

        PlayerCon = GetComponent<CharacterController>();
        ThisAnimatorCon = GetComponent<Animator>();

        

    }
	
	// Update is called once per frame
	void Update () {

          if (Camera.main.transform.localEulerAngles.x >= toggleAngle && Camera.main.transform.localEulerAngles.x < 90.0f)
        {
            moveForward = true;
        }
        else { moveForward = false;
        }

        if (moveForward)
        {

            ThisVRSpeeed = VRSpeeed;
            MoveFrontAndBack = ThisVRSpeeed * PlayerSpeed;
        }
        else {

            ThisVRSpeeed = 0f;
        }

        if (PlayerCon.isGrounded) {
            FallSpeed = 0;
        }
        else {

            FallSpeed = 1;


        }

        MoveFrontAndBack = ThisVRSpeeed * PlayerSpeed;

        Vector3 Movement = new Vector3(0, 0, MoveFrontAndBack);
        Movement = transform.rotation * Movement;
        Movement.y -= Gravity * Time.deltaTime * FallSpeed;
        PlayerCon.Move(Movement * Time.deltaTime);

        CamerRotateX = Input.GetAxis("Mouse X") * CameraSpeed;
        CamerRotateY = Input.GetAxis("Mouse Y") * CameraSpeed;

        PlayerCon.transform.Rotate(0, CamerRotateX, 0);
        PlayerCamera.transform.Rotate(-CamerRotateY , 0 ,0);


        Vector3 horizontalVelocity = PlayerCon.velocity;
        horizontalVelocity = new Vector3(PlayerCon.velocity.x, 0, PlayerCon.velocity.z);
        float overallSpeed = PlayerCon.velocity.magnitude;

        if (overallSpeed == 0)
        {

            AnimationToPlay(IDLE_ANIME_BOOL);
            
        }
        else {

            AnimationToPlay(WALK_ANIME_BOOL);

        }

    }

    private void AnimationToPlay(string BoolName) {

        DisableOtherAnime(ThisAnimatorCon, BoolName);

        ThisAnimatorCon.SetBool(BoolName , true);



    }

    private void DisableOtherAnime(Animator ThisAnimatorCon, string AnimeString) {

        foreach (AnimatorControllerParameter parame in ThisAnimatorCon.parameters) {

            if (parame.name != AnimeString) {

                ThisAnimatorCon.SetBool(parame.name , false);
            }

        }

    }
}
