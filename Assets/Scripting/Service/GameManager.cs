using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages user interaction and rendering for different conic sections (Circle, Parabola, Ellipse, Hyperbola).
/// Handles UI events, parameter updates, and delegates rendering to <see cref="RenderingManager"/>.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Enum representing the currently selected conic type.
    /// </summary>
    private enum SelectedConic : int
    {
        /// <summary>Circle conic section.</summary>
        Circle = 0,
        /// <summary>Parabola conic section.</summary>
        Parabola = 1,
        /// <summary>Ellipse conic section.</summary>
        Ellipse = 2,
        /// <summary>Hyperbola conic section.</summary>
        Hyperbola = 3
    }

    [Header("UI References:")]

    /// <summary>
    /// Dropdown for conic selection in the UI.
    /// </summary>
    [SerializeField] private TMP_Dropdown conicSelection;

    // ---- Circle Options ----
    /// <summary>Panel containing circle parameter sliders.</summary>
    [SerializeField] private GameObject circleOptions;
    /// <summary>Slider for the horizontal offset (h) of the circle's center.</summary>
    [SerializeField] private Slider circleHSlider;
    /// <summary>Slider for the vertical offset (k) of the circle's center.</summary>
    [SerializeField] private Slider circleKSlider;
    /// <summary>Slider for the radius (r) of the circle.</summary>
    [SerializeField] private Slider circleRSlider;

    // ---- Parabola Options ----

    /// <summary>Panel containing parabola parameter sliders.</summary>
    [SerializeField] private GameObject parabolaOptions;
    /// <summary>Slider for the horizontal offset (h) of the parabola's vertex.</summary>
    [SerializeField] private Slider parabolaHSlider;
    /// <summary>Slider for the vertical offset (k) of the parabola's vertex.</summary>
    [SerializeField] private Slider parabolaKSlider;
    /// <summary>Slider for the parameter (a) controlling parabola width/orientation.</summary>
    [SerializeField] private Slider parabolaASlider;
    /// <summary>Button to set parabola orientation as vertical.</summary>
    [SerializeField] private Button parabolaVerticalButton;
    /// <summary>Button to set parabola orientation as horizontal.</summary>
    [SerializeField] private Button parabolaHorizontalButton;

    // ---- Ellipse Options ----

    /// <summary>Panel containing ellipse parameter sliders.</summary>
    [SerializeField] private GameObject ellipseOptions;
    /// <summary>Slider for the horizontal offset (h) of the ellipse's center.</summary>
    [SerializeField] private Slider ellipseHSlider;
    /// <summary>Slider for the vertical offset (k) of the ellipse's center.</summary>
    [SerializeField] private Slider ellipseKSlider;
    /// <summary>Slider for the ellipse's major axis length (a).</summary>
    [SerializeField] private Slider ellipseASlider;
    /// <summary>Slider for the ellipse's minor axis length (b).</summary>
    [SerializeField] private Slider ellipseBSlider;

    // ---- Hyperbola Options ----

    /// <summary>Panel containing hyperbola parameter sliders.</summary>
    [SerializeField] private GameObject hyperbolaOptions;
    /// <summary>Slider for the horizontal offset (h) of the hyperbola's center.</summary>
    [SerializeField] private Slider hyperbolaHSlider;
    /// <summary>Slider for the vertical offset (k) of the hyperbola's center.</summary>
    [SerializeField] private Slider hyperbolaKSlider;
    /// <summary>Slider for the hyperbola's a parameter (controls spread/orientation).</summary>
    [SerializeField] private Slider hyperbolaASlider;
    /// <summary>Slider for the hyperbola's b parameter (controls spread/orientation).</summary>
    [SerializeField] private Slider hyperbolaBSlider;
    /// <summary>Button to set hyperbola orientation as horizontal.</summary>
    [SerializeField] private Button hyperbolaHorizontalButton;
    /// <summary>Button to set hyperbola orientation as vertical.</summary>
    [SerializeField] private Button hyperbolaVerticalButton;

    // ---- Offsets ----

    [Header("Offsets:")]

    /// <summary>Offset to apply to the rendered circle.</summary>
    [SerializeField] private Vector2 circleOffset = Vector2.zero;
    /// <summary>Offset to apply to the rendered parabola.</summary>
    [SerializeField] private Vector2 parabolaOffset = Vector2.zero;
    /// <summary>Offset to apply to the rendered ellipse.</summary>
    [SerializeField] private Vector2 ellipseOffset = Vector2.zero;
    /// <summary>Offset to apply to the rendered hyperbola.</summary>
    [SerializeField] private Vector2 hyperbolaOffset = Vector2.zero;

    // ---- Scales ----
    [Header("Scales:")]

    /// <summary>Scaling factor for the parabola rendering.</summary>
    [SerializeField] private float parabolaScale = 1f;
    /// <summary>Scaling factor for the ellipse rendering.</summary>
    [SerializeField] private float ellipseScale = 1f;
    /// <summary>Scaling factor for the hyperbola rendering.</summary>
    [SerializeField] private float hyperbolaScale = 1f;

    // ---- Resolutions ----
    [Header("Resolutions:")]

    /// <summary>Number of points to use when rendering a circle.</summary>
    [SerializeField] private int circleResolution = 30;
    /// <summary>Number of points to use when rendering a parabola.</summary>
    [SerializeField] private int parabolaResolution = 30;
    /// <summary>Number of points to use when rendering an ellipse.</summary>
    [SerializeField] private int ellipseResolution = 30;
    /// <summary>Number of points to use when rendering a hyperbola.</summary>
    [SerializeField] private int hyperbolaResolution = 30;

    // ---- Loop Closing Flags ----

    [Header("Rendering:")]

    /// <summary>
    /// Reference to the <see cref="RenderingManager"/> responsible for drawing the curves.
    /// </summary>
    [SerializeField] private RenderingManager renderingManager;

    /// <summary>Should the circle rendering form a closed loop?</summary>
    [SerializeField] private bool closeLoopCircle = true;
    /// <summary>Should the ellipse rendering form a closed loop?</summary>
    [SerializeField] private bool closeLoopEllipse = false;
    /// <summary>Should the parabola rendering form a closed loop?</summary>
    [SerializeField] private bool closeLoopParabola = false;
    /// <summary>Should the hyperbola rendering form a closed loop?</summary>
    [SerializeField] private bool closeLoopHyperbola = false;

    // ---- State ----

    /// <summary>
    /// Indicates whether the parabola is oriented vertically (true) or horizontally (false).
    /// </summary>
    private bool isParabolaVertical = true;

    /// <summary>
    /// Indicates whether the hyperbola is oriented vertically (true) or horizontally (false).
    /// </summary>
    private bool isHyperbolaVertical = true;

    /// <summary>
    /// Stores the current point maps for rendering (single or dual conic components).
    /// </summary>
    private Vector2[][] currentPointMap = new Vector2[2][];

    /// <summary>
    /// Initializes the UI state at startup.
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    ///  Initialize UI elements and set up event listeners
    /// </summary>
    private void Init()
    {
        circleOptions.SetActive(true);
        parabolaOptions.SetActive(false);
        ellipseOptions.SetActive(false);
        hyperbolaOptions.SetActive(false);

        conicSelection.onValueChanged.AddListener((value) => OnConicSelectionChange());
        conicSelection.value = (int)SelectedConic.Circle;

        circleHSlider.onValueChanged.AddListener((value) => OnCircleValueChange());
        circleKSlider.onValueChanged.AddListener((value) => OnCircleValueChange());
        circleRSlider.onValueChanged.AddListener((value) => OnCircleValueChange());

        parabolaHSlider.onValueChanged.AddListener((value) => OnParabolaValueChange());
        parabolaKSlider.onValueChanged.AddListener((value) => OnParabolaValueChange());
        parabolaASlider.onValueChanged.AddListener((value) => OnParabolaValueChange());
        parabolaVerticalButton.onClick.AddListener(OnParabolaVerticalButtonPress);
        parabolaHorizontalButton.onClick.AddListener(OnParabolaHorizontalButtonPress);

        ellipseHSlider.onValueChanged.AddListener((value) => OnEllipseValueChange());
        ellipseKSlider.onValueChanged.AddListener((value) => OnEllipseValueChange());
        ellipseASlider.onValueChanged.AddListener((value) => OnEllipseValueChange());
        ellipseBSlider.onValueChanged.AddListener((value) => OnEllipseValueChange());

        hyperbolaHSlider.onValueChanged.AddListener((value) => OnHyperbolaValueChange());
        hyperbolaKSlider.onValueChanged.AddListener((value) => OnHyperbolaValueChange());
        hyperbolaASlider.onValueChanged.AddListener((value) => OnHyperbolaValueChange());
        hyperbolaBSlider.onValueChanged.AddListener((value) => OnHyperbolaValueChange());
        hyperbolaHorizontalButton.onClick.AddListener(OnHyperbolaHorizontalButtonPress);
        hyperbolaVerticalButton.onClick.AddListener(OnHyperbolaVerticalButtonPress);

        OnConicSelectionChange(); // Initialize the conic section UI

        OnCircleValueChange(); // Initialize the circle rendering
    }

    /// <summary>
    /// Event handler for when the selected conic section changes.
    /// Updates the UI and renders the corresponding conic section.
    /// </summary>
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

    /// <summary>
    /// Event handler for when a circle parameter value changes.
    /// Regenerates the point map and renders the circle.
    /// </summary>
    public void OnCircleValueChange()
    {
        float h = circleHSlider.value;
        float k = circleKSlider.value;
        float r = circleRSlider.value;

        currentPointMap[0] = BolaMathZ.GenerateCirclePointMap(h, k, r, circleResolution);

        renderingManager.RenderPointMap(currentPointMap[0], circleOffset, closeLoopCircle);
    }

    /// <summary>
    /// Event handler for when a parabola parameter value changes.
    /// Regenerates the point map and renders the parabola.
    /// </summary>
    public void OnParabolaValueChange()
    {
        float h = parabolaHSlider.value;
        float k = parabolaKSlider.value;
        float a = parabolaASlider.value;

        currentPointMap[0] = BolaMathZ.GenerateParabolaPointMap(h, k, a, isParabolaVertical, parabolaResolution, parabolaScale);

        renderingManager.RenderPointMap(currentPointMap[0], parabolaOffset, closeLoopParabola);
    }

    /// <summary>
    /// Event handler for switching the parabola orientation to vertical.
    /// Triggers a parabola redraw.
    /// </summary>
    public void OnParabolaVerticalButtonPress()
    {
        isParabolaVertical = true;
        OnParabolaValueChange();
    }

    /// <summary>
    /// Event handler for switching the parabola orientation to horizontal.
    /// Triggers a parabola redraw.
    /// </summary>
    public void OnParabolaHorizontalButtonPress()
    {
        isParabolaVertical = false;
        OnParabolaValueChange();
    }

    /// <summary>
    /// Event handler for when an ellipse parameter value changes.
    /// Regenerates the point map and renders the ellipse.
    /// </summary>
    public void OnEllipseValueChange()
    {
        float h = ellipseHSlider.value;
        float k = ellipseKSlider.value;
        float a = ellipseASlider.value;
        float b = ellipseBSlider.value;

        currentPointMap[0] = BolaMathZ.GenerateEllipsePointMap(h, k, a, b, ellipseResolution, ellipseScale);

        renderingManager.RenderPointMap(currentPointMap[0], ellipseOffset, closeLoopEllipse);
    }

    /// <summary>
    /// Event handler for when a hyperbola parameter value changes.
    /// Regenerates the point maps for both branches and renders the hyperbola.
    /// </summary>
    public void OnHyperbolaValueChange()
    {
        float h = hyperbolaHSlider.value;
        float k = hyperbolaKSlider.value;
        float a = hyperbolaASlider.value;
        float b = hyperbolaBSlider.value;

        currentPointMap = BolaMathZ.GenerateHyperbolaPointMap(h, k, a, b, isHyperbolaVertical, hyperbolaScale, hyperbolaResolution, 4);

        renderingManager.RenderDualPointMap(currentPointMap[0], currentPointMap[1], hyperbolaOffset, closeLoopHyperbola);
    }

    /// <summary>
    /// Event handler for switching the hyperbola orientation to vertical.
    /// Triggers a hyperbola redraw.
    /// </summary>
    public void OnHyperbolaVerticalButtonPress()
    {
        isHyperbolaVertical = true;
        OnHyperbolaValueChange();
    }

    /// <summary>
    /// Event handler for switching the hyperbola orientation to horizontal.
    /// Triggers a hyperbola redraw.
    /// </summary>
    public void OnHyperbolaHorizontalButtonPress()
    {
        isHyperbolaVertical = false;
        OnHyperbolaValueChange();
    }
}
