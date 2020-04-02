using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    //1. obracając postać można obrócić ją o 90 stopni w jakim kolwiek kierunku
    //2. za to w sterowaniu musi byc uwzglednione to ze postac idzie zawsze w kierunku w którym się (klika input/przesuwa joystick)
    //3. tzn. postac zwrócona twarzą do północy porusza się po osi wschód zachód i kamera jest umieszczona na kierunku północnym dla postaci.
    // Teraz, gdy postać porusza się na wschód gdy naciskamy prawy klawisz. Kamera przechodzi na południe i gdy naciskamy prawy klawisz postać porusza się na zachód. 
    // powoduje to że postać porusza się względem przesunięcia joysticka czy też wciśnięcia L/P inputu

    void Start(){
        
    }

    void Update(){
        
    }
}
