using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject buyButton;
    [SerializeField]
    private GameObject activityBalloon;
    [SerializeField]
    private UIController uIController;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        ShowBuyButton();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        HideBuyButton();
        uIController.CloseScreens();
    }

    void ShowBuyButton()
    {
        buyButton.SetActive(true);
        activityBalloon.SetActive(false);
        UIController.buyButtonShown = true;
    }

    void HideBuyButton()
    {
        buyButton.SetActive(false);
        activityBalloon.SetActive(true);
        UIController.buyButtonShown = false;
    }
}
