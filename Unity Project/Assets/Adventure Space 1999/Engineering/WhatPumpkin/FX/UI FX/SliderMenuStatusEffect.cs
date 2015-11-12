using UnityEngine;
using System.Collections;

public class SliderMenuStatusEffect : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Text _onButtonText;
    [SerializeField] private UnityEngine.UI.Text _offButtonText;
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;
    private UnityEngine.UI.Slider _slider;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
	{
        _slider = GetComponent<UnityEngine.UI.Slider>();
        SliderStatusState();
        _slider.onValueChanged.AddListener(delegate {SliderStatusState();});
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void SliderStatusState()
    {
        if (_slider.value == 0)
        {
            _onButtonText.color = _enabledColor;
            _offButtonText.color = _disabledColor;
        }
        if (_slider.value == 1)
        {
            _onButtonText.color = _disabledColor;
            _offButtonText.color = _enabledColor;
        }
    }

    public void OnButtonClick()
    {
        _slider.value = 0;
        SliderStatusState();
    }

    public void OffButtonClick()
    {
        _slider.value = 1;
        SliderStatusState();
    }

}
