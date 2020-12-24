using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class ChangeText : MonoBehaviour
{


    public TextMeshProUGUI textField;

    public TextMeshProUGUI deathField;

    public TextMeshProUGUI achievementField;


    public StatController statController;


    // Update is called once per frame
    void Update()
    {


        float time = (float) Math.Round(statController.getTotalTime(), 2);

        textField.text = Convert.ToString(time);


        deathField.text = "Deaths: " + Convert.ToString(statController.getNumberOfDeaths());

        achievementField.text = "Achievements: " + Convert.ToString(statController.getAchievementsUnlocked());
        
    }
}
