using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    enum SelectedConic : int
    {
        Circle = 0,
        Parabola = 1,
        Ellipse = 2,
        Hyperbola = 3
    }

    [SerializeField] private TMP_Dropdown conicSelection;

    [SerializeField] private GameObject circleOptions;
    [SerializeField] private Slider circleHSlider;
    [SerializeField] private Slider circleKSlider;
    [SerializeField] private Slider circleRSlider;

    [SerializeField] private GameObject parabolaOptions;
    [SerializeField] private Slider parabolaHSlider;
    [SerializeField] private Slider parabolaKSlider;
    [SerializeField] private Slider parabolaASlider;
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

    [SerializeField] private Vector2 circleOffset = new Vector2(0, 0);
    [SerializeField] private Vector2 parabolaOffset = new Vector2(0, 0);
    [SerializeField] private Vector2 ellipseOffset = new Vector2(0, 0);
    [SerializeField] private Vector2 hyperbolaOffset = new Vector2(0, 0);

    [SerializeField] private float parabolaScale = 1f;
    [SerializeField] private float ellipseScale = 1f;
    [SerializeField] private float hyperbolaScale = 1f;

    [SerializeField] private int circleResolution = 30;
    [SerializeField] private int parabolaResolution = 30;
    [SerializeField] private int ellipseResolution = 30;
    [SerializeField] private int hyperbolaResolution = 30;

    [SerializeField] private bool closeLoopCircle = true;
    [SerializeField] private bool closeLoopEllipse = false;
    [SerializeField] private bool closeLoopParabola = false;
    [SerializeField] private bool closeLoopHyperbola = false;

    private bool isParabolaVertical = true;
    private bool isHyperbolaVertical = true;

    private Vector2[] currentPointMap = null;

    [SerializeField] private RenderingManager renderingManager;

    private void Start()
    {
        // Initialize UI elements or set up event listeners here if needed
        circleOptions.SetActive(true);
        parabolaOptions.SetActive(false);
        ellipseOptions.SetActive(false);
        hyperbolaOptions.SetActive(false);
    }

    public void OnConicSelectionChange()
    {
        circleOptions.SetActive(false);
        parabolaOptions.SetActive(false);
        ellipseOptions.SetActive(false);
        hyperbolaOptions.SetActive(false);

        switch (conicSelection.value)
        {
            case (int)SelectedConic.Circle:

                circleOptions.SetActive(true);

                OnCircleValueChange();

                break;

            case (int)SelectedConic.Parabola:

                parabolaOptions.SetActive(true);

                OnParabolaValueChange();

                break;

            case (int)SelectedConic.Ellipse:

                ellipseOptions.SetActive(true);

                OnEllipseValueChange();

                break;

            case (int)SelectedConic.Hyperbola:

                hyperbolaOptions.SetActive(true);
                
                OnHyperbolaValueChange();

                break;
        }   
    }

    public void OnCircleValueChange()
    {
        float h = circleHSlider.value;
        float k = circleKSlider.value;
        float r = circleRSlider.value;
        
        currentPointMap = BolaMathZ.GenerateCirclePointMap(h, k, r, circleResolution);

        renderingManager.RenderPointMap(currentPointMap, circleOffset, closeLoopCircle);
    }

    public void OnParabolaValueChange()
    {
        float h = parabolaHSlider.value;
        float k = parabolaKSlider.value;
        float a = parabolaASlider.value;

        currentPointMap = BolaMathZ.GenerateParabolaPointMap(h, k, a, isParabolaVertical, parabolaResolution, parabolaScale);

        renderingManager.RenderPointMap(currentPointMap, parabolaOffset, closeLoopParabola);
    }

    public void OnParabolaVerticalButtonPress()
    {
        isParabolaVertical = true;

        OnParabolaValueChange();
    }

    public void OnParabolaHorizontalButtonPress()
    {
        isParabolaVertical = false;

        OnParabolaValueChange();
    }

    public void OnEllipseValueChange()
    {
        float h = ellipseHSlider.value;
        float k = ellipseKSlider.value;
        float a = ellipseASlider.value;
        float b = ellipseBSlider.value;

        currentPointMap = BolaMathZ.GenerateEllipsePointMap(h, k, a, b, ellipseResolution, ellipseScale);

        renderingManager.RenderPointMap(currentPointMap, ellipseOffset, closeLoopEllipse);
    }

    public void OnHyperbolaValueChange()
    {
        float h = circleHSlider.value;
        float k = circleKSlider.value;
        float a = circleRSlider.value;
        float b = circleRSlider.value;
        float c = circleRSlider.value;

        //currentPointMap = BolaMathZ.GenerateHyperbolaPointmap(h, k, r, 30);

        renderingManager.RenderPointMap(currentPointMap, hyperbolaOffset, closeLoopHyperbola);
    }
}
