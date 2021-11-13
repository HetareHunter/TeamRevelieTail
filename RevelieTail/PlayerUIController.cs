using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReverieTale;
using UniRx;

namespace ReverieTale
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] Slider playerHPSlider;
        [SerializeField] Text playerHPText;
        [SerializeField] Text attackPoint;
        [SerializeField] Text defensePoint;
        [SerializeField] Text skillPoint;
        [SerializeField] Image attributeImage;
        [SerializeField] AttributeIconDatas attributeIconDatas;
        PlayerController playerController;
        PlayerStatus playerStatus;

        void Start()
        {
            playerController = PlayerController.Instance;
            playerStatus = PlayerStatus.Instance;

            this.ObserveEveryValueChanged(x => GameManager.Instance.CurrentGameState)
                .Subscribe(x =>
                {
                    if(x == GameState.Battle)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }).AddTo(this);

            this.ObserveEveryValueChanged(x => playerStatus.Hp)
                .Subscribe(x =>
                {
                    playerHPSlider.maxValue = playerStatus.Hp;
                    playerHPText.text = playerHPSlider.value.ToString() + "/" + playerHPSlider.maxValue.ToString();
                }).AddTo(this);  //AddToを入れないとこのスクリプトがアタッチされてるオブジェクトが破棄されてもこのスクリプトは実行され続けるっぽい？

            this.ObserveEveryValueChanged(x => playerController.currentHp)
                .Subscribe(x =>
                {
                    playerHPSlider.value = playerController.currentHp;
                    playerHPText.text = playerHPSlider.value.ToString() + "/" + playerHPSlider.maxValue.ToString();
                }).AddTo(this);

            this.ObserveEveryValueChanged(x => playerController.currentAttack)
                .Subscribe(x =>
                {
                    attackPoint.text = playerController.currentAttack.ToString();
                }).AddTo(this);

            this.ObserveEveryValueChanged(x => playerController.currentDefence)
                .Subscribe(x =>
                {
                    defensePoint.text = playerController.currentDefence.ToString();
                }).AddTo(this);

            this.ObserveEveryValueChanged(x => playerController.currentSkillPoint)
                .Subscribe(x =>
                {
                    skillPoint.text = playerController.currentSkillPoint.ToString();
                }).AddTo(this);

            this.ObserveEveryValueChanged(x => playerController.currentAttribute)
                .Subscribe(x =>
                {
                    attributeImage.sprite = attributeIconDatas.GetAttributeIconObj(playerController.currentAttribute).GetComponent<Image>().sprite;
                }).AddTo(this);
        }
    }
}