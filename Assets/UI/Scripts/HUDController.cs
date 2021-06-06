using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    int maxLife = 0;
    int currentLife = 0;
    IEntity playerEntity;

    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        playerEntity = GameObject.FindGameObjectWithTag(k.TagPlayer).GetComponent<IEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEntity != null)
        {
            maxLife = playerEntity.Data.MaxLife;
            currentLife = playerEntity.Data.CurrentLife;

            healthSlider.value = currentLife / maxLife;
        }
    }
}
