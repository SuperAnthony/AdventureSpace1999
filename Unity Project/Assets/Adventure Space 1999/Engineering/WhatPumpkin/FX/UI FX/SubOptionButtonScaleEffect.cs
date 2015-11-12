namespace WhatPumpkin.FX
{
    /// <summary>
    /// Script used for implementing sub-option sequential scaling effect.
    /// </summary>
    public class SubOptionButtonScaleEffect : SubOptionButtonUIEffect
    {
        public override void DisplaySubOptions()
        {
            ScaleCharacterButtons();
        }

        private void ScaleCharacterButtons()
        {
            if (_subOptionButtons[0].transform.localScale.x <= 0)
            {
                _openedState.enabled = true;
                _closedState.enabled = false;
                _mainButton.targetGraphic = _openedState;
                foreach (var characterButton in _subOptionButtons)
                {
                    characterButton.GetComponent<ScaleEffect>().ScaleUp();
                }
            }
            else
            {
                _openedState.enabled = false;
                _closedState.enabled = true;
                foreach (var characterButton in _subOptionButtons)
                {
                    characterButton.GetComponent<ScaleEffect>().ScaleDown();
                }
            }
        }
    }
}