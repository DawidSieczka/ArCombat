using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRotation : MonoBehaviour
{

    //1. obracając postać można obrócić ją o 90 stopni w jakim kolwiek kierunku
    //2. za to w sterowaniu musi byc uwzglednione to ze postac idzie zawsze w kierunku w którym się (klika input/przesuwa joystick)
    //3. tzn. postac zwrócona twarzą do północy porusza się po osi wschód zachód i kamera jest umieszczona na kierunku północnym dla postaci.
    // Teraz, gdy postać porusza się na wschód gdy naciskamy prawy klawisz. Kamera przechodzi na południe i gdy naciskamy prawy klawisz postać porusza się na zachód. 
    // powoduje to że postać porusza się względem przesunięcia joysticka czy też wciśnięcia L/P inputu
    
    PlayerPositionCorrector _posCorrector;
    PlayerAim _playerAim;
    ButtonEvent _buttonEvent;
    bool shouldRotate;
    float startRotationPos;
    float endRotationPos;
    public float rotationSpeed;
    void Start(){
        _posCorrector = GetComponent<PlayerPositionCorrector>();
        _buttonEvent = FindObjectOfType<ButtonEvent>();
        _buttonEvent.OnRotated.AddListener(InvokeRotation);
        _playerAim = FindObjectOfType<PlayerAim>();
    }

    void Update(){
        if(shouldRotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed*Time.deltaTime);
            //Debug.Log($"Y euler: {transform.eulerAngles.y}");

            if (Math.Abs(transform.eulerAngles.y - endRotationPos) < 3f)
            {
                shouldRotate = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotationPos, transform.eulerAngles.z);
            }
        }
    }
    void InvokeRotation()
    {
        if (!shouldRotate) //You can click Invoke only when player is not rotating
        {
            shouldRotate = true;
            startRotationPos = transform.rotation.eulerAngles.y;
            var targetAngle = (float)startRotationPos + 90;
            endRotationPos = RoundValueToCorrectAngle(targetAngle);
            _posCorrector.CorrectPosition();
            _playerAim.ChangeDepthAxis();
        }
    }
    public float RoundValueToCorrectAngle(float inputValue)
    {
        List<float> CorrectValues = new List<float> { 0, 90, 180, 270, 360};
        print(inputValue);
        float roundedAngle = 0;

        if (inputValue >= 360)
            return roundedAngle;

        float theSmallestDifeerence = float.MaxValue;
        foreach (var correctValue in CorrectValues)
        {
            var difference = Math.Abs(inputValue - correctValue);

            bool isTheSmallestDifference = (difference <= theSmallestDifeerence);
            if (isTheSmallestDifference)
            {
                theSmallestDifeerence = difference;
                roundedAngle = correctValue;
            }
        }

        return roundedAngle;
    }
}
