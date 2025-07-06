using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown conicSelection;

    [SerializeField] private GameObject circleOptions;
    [SerializeField] private Slider circleHSlider;
    [SerializeField] private Slider circleKSlider;
    [SerializeField] private Slider circleRSlider;

    [SerializeField] private GameObject parabolaOptions;
    [SerializeField] private Slider parabolaHSlider;
    [SerializeField] private Slider parabolaKSlider;
    [SerializeField] private Slider parabolaPSlider;
    [SerializeField] private Button parabolaVerticalButton;
    [SerializeField] private Button parabolaHorizontalButton;

    [SerializeField] private GameObject ellipseOptions;
    [SerializeField] private Slider ellipseHSlider;
    [SerializeField] private Slider ellipseKSlider;
    [SerializeField] private Slider ellipseASlider;
    [SerializeField] private Slider ellipseBSlider;

    [SerializeField] private GameObject hyperbolaOptions;
    [SerializeField] private Slider hyperbolaHSlider;
    [SerializeField] private Slider hyperbolaKSlider;
    [SerializeField] private Slider hyperbolaASlider;
    [SerializeField] private Slider hyperbolaBSlider;
    [SerializeField] private Slider hyperbolaCSlider;
    [SerializeField] private Button hyperbolaHorizontalButton;
    [SerializeField] private Button hyperbolaVerticalButton;

    private void Start()
    {
        // Initialize UI elements or set up event listeners here if needed
        circleOptions.SetActive(false);
        parabolaOptions.SetActive(false);
        ellipseOptions.SetActive(false);
        hyperbolaOptions.SetActive(false);
    }
}
