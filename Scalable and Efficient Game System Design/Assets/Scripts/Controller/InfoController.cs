using UnityEngine;
using Model;
using View;
using Event;

namespace Controller
{
    public class InfoController : MonoBehaviour
    {
        [SerializeField] private InfoView infoView;
        [SerializeField] private int maxValue = 100;

        private InfoModel infoModel;

        private void Awake()
        {
            infoModel = new InfoModel(maxValue);
        }

        private void OnEnable()
        {
            GameEvents.OnPlayerHealthChanged += UpdateInfo;
        }

        private void OnDisable()
        {
            GameEvents.OnPlayerHealthChanged -= UpdateInfo;
        }

        private void Start()
        {
            UpdateView();
        }

        private void UpdateInfo(int newValue)
        {
            infoModel.UpdateValue(newValue);
            UpdateView();
        }

        private void UpdateView()
        {
            infoView.UpdateUI(infoModel.CurrentValue, infoModel.MaxValue);
        }
    }
}