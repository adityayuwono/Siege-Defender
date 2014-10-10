using UnityEngine;

namespace Scripts.Components
{
    public class BaseTexturedController : BaseController
    {
        public UITexture MainTexture;

        protected override void OnSetup()
        {
            base.OnSetup();

            MainTexture.onChange += OnChange;
        }

        protected Rect TextureScreenArea;
        protected virtual void OnChange()
        {
            var screenFactor = Screen.width / (float)Screen.height;
            var screenWidth = 720f * screenFactor;
            var position = MainTexture.transform.localPosition;
            var yFactor = ((360f + position.y) + MainTexture.height) / 720f;
            var xpos = (screenWidth / 2f) + position.x;
            var screenHeightScale = Screen.height / 720f;

            TextureScreenArea = new Rect(xpos * (Screen.width / screenWidth), (1 - yFactor) * Screen.height, MainTexture.width * screenHeightScale, MainTexture.height * screenHeightScale);
        }
    }
}
