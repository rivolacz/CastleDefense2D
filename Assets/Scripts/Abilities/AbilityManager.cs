using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace Project.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField]
        private float MaxMana = 300;
        [SerializeField]
        ProgressBar manaBar;
        [SerializeField]
        private TMP_Text manaText;
        [SerializeField]
        private Image castImage;
        [SerializeField]
        private Sprite blackTick;
        [SerializeField]
        private Sprite redTick;
        [SerializeField]
        private TMP_Text costText;

        private float currentMana = 0;
        [SerializeField]
        private RangeVisualizer rangeVisualizer;
        [SerializeField]
        private GameObject castingConfirmation;
        [SerializeField]
        private FireballAbility fireballAbility;
        [SerializeField]
        private TMP_Text fireballCostText;
        [SerializeField]
        private CashBonusAbility cashBonusAbility;
        [SerializeField]
        private TMP_Text cashBonusCostText;
        [SerializeField]
        private PoisonAbility poisonAbility;
        [SerializeField]
        private TMP_Text poisonCostText;
        [SerializeField]
        private TimeWarpAbility timeWarpAbility;
        [SerializeField]
        private TMP_Text timeWarpCostText;

        private float CurrentMana { 
            get
            {
                return currentMana;
            }
            set
            {
                currentMana = value;
                if (manaBar != null) {
                    manaBar.FillProgressBar(currentMana / MaxMana);
                    manaText.text = $"{currentMana.ToString("0")}/{MaxMana}";
                }
            }
        }
        private float ManaPerSecond = 20;
        Ability abilityToCast = null;
        private PlayerInput input;
        private Vector2 castingPosition = Vector2.zero;

        private void Awake()
        {
            fireballCostText.text = fireballAbility.GetManaCost().ToString();
            cashBonusCostText.text = cashBonusAbility.GetManaCost().ToString();
            poisonCostText.text = poisonAbility.GetManaCost().ToString();
            timeWarpCostText.text = timeWarpAbility.GetManaCost().ToString();
            castingConfirmation.SetActive(false);
            rangeVisualizer.gameObject.SetActive(false);
        }

        public void Start()
        {
            input = new PlayerInput();
            input.Enable();
            StartCoroutine(RegenerateMana());
            castingConfirmation.SetActive(false);
        }

        void Update()
        {
            if (!Settings.PlayerIsCasting || abilityToCast == null) return;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 tapPosition = input.Player.FirstTouchPosition.ReadValue<Vector2>();
                castingPosition = Camera.main.ScreenToWorldPoint(tapPosition);
                rangeVisualizer.transform.position = castingPosition;
                abilityToCast.SetTarget(castingPosition);
            }
        }

        public void UpdateGraphics()
        {
            if (currentMana > abilityToCast.GetManaCost())
            {
                castImage.sprite = blackTick;
            }
            else
            {
                castImage.sprite = redTick;
            }
        }


        IEnumerator RegenerateMana()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                AddMana(ManaPerSecond / 10);
            }
        }


        private void AddMana(float mana)
        {
            CurrentMana += mana;
            CurrentMana = Math.Clamp(CurrentMana, 0, MaxMana);
        }

        public void CastFireball()
        {
            if (Settings.PlayerIsCasting || currentMana < fireballAbility.GetManaCost()) return;
            ConfirmationPopUp();
            abilityToCast = fireballAbility;
            rangeVisualizer.SetRange(fireballAbility.Range);
            costText.text = abilityToCast.GetManaCost().ToString();
        }

        public void CastCashBonus()
        {
            if (!cashBonusAbility.CanCast() || currentMana < cashBonusAbility.GetManaCost())return;
            currentMana -= cashBonusAbility.GetManaCost();
            cashBonusAbility.Cast();
            StartCoroutine(ProgressCashBonus());
        }

        public void CastPoison()
        {
            if (Settings.PlayerIsCasting || currentMana < poisonAbility.GetManaCost()) return;
            ConfirmationPopUp();
            rangeVisualizer.SetRange(poisonAbility.GetRange());
            abilityToCast = poisonAbility;
            costText.text = abilityToCast.GetManaCost().ToString();
        }

        public void CastTimeWarp()
        {
            if (Settings.PlayerIsCasting || currentMana < timeWarpAbility.GetManaCost()) return;
            ConfirmationPopUp();
            abilityToCast = poisonAbility;
            costText.text = abilityToCast.GetManaCost().ToString();
        }

        public void ConfirmCast()
        {
            if (currentMana < abilityToCast.GetManaCost()) return;
            abilityToCast.Cast();
            currentMana -= abilityToCast.GetManaCost();
            rangeVisualizer.gameObject.SetActive(false);
            castingConfirmation.SetActive(false);
            Settings.PlayerIsCasting = false;
        }

        public void CancelCast()
        {
            abilityToCast = null;
            castingConfirmation.SetActive(false);
            rangeVisualizer.gameObject.SetActive(false);
            Settings.PlayerIsCasting = false;
        }

        private IEnumerator ProgressCashBonus()
        {
            float time = 0;
            float maxTime = cashBonusAbility.GetEffectLength();
            while (maxTime > time)
            {
                yield return null;
                cashBonusAbility.UpdateEffectProgress(time);
                time += Time.deltaTime;
            }
            cashBonusAbility.CancelEffect();
        }

        private void ConfirmationPopUp()
        {
            castingConfirmation.SetActive(true);
            rangeVisualizer.gameObject.SetActive(true);
            Settings.PlayerIsCasting = true;
        }
    }
}
