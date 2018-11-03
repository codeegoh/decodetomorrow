
namespace DigiFrame
{
    using UnityEngine;


    public static class RectTransformEx
    {

        public static RectTransform setFullSize( this RectTransform self ) {

            self.sizeDelta  = new Vector2(0.0f,0.0f);
            self.anchorMin  = new Vector2(0.0f,0.0f);
            self.anchorMax  = new Vector2(1.0f,1.0f);
            self.pivot      = new Vector2(0.5f,0.5f);
            return self;
        }
        

        public static Vector2 getSize( this RectTransform self ) {
            return self.rect.size;
        }


        public static void setSize( this RectTransform self, Vector2 newSize ) {

            var pivot   = self.pivot;
            var dist    = newSize - self.rect.size;
            self.offsetMin = self.offsetMin - new Vector2( dist.x * pivot.x, dist.y * pivot.y );
            self.offsetMax = self.offsetMax + new Vector2( dist.x * (1f - pivot.x), dist.y * (1f - pivot.y) );
        }
       

        public static RectTransform setSizeFromLeft( this RectTransform self, float rate ) {

            self.setFullSize();

            var width = self.rect.width;

            self.anchorMin  = new Vector2(0.0f,0.0f);
            self.anchorMax  = new Vector2(0.0f,1.0f);
            self.pivot      = new Vector2(0.0f,1.0f);
            self.sizeDelta  = new Vector2(width*rate,0.0f);

            return self;
        }

        public static RectTransform setSizeFromRight( this RectTransform self, float rate ) {

            self.setFullSize();

            var width = self.rect.width;

            self.anchorMin  = new Vector2(1.0f,0.0f);
            self.anchorMax  = new Vector2(1.0f,1.0f);
            self.pivot      = new Vector2(1.0f,1.0f);
            self.sizeDelta  = new Vector2(width*rate,0.0f);

            return self;
        }


        public static RectTransform setSizeFromTop( this RectTransform self, float rate ) {

            self.setFullSize();

            var height = self.rect.height;

            self.anchorMin  = new Vector2(0.0f,1.0f);
            self.anchorMax  = new Vector2(1.0f,1.0f);
            self.pivot      = new Vector2(0.0f,1.0f);
            self.sizeDelta  = new Vector2(0.0f,height*rate);

            return self;
        }

        public static RectTransform setSizeFromBottom( this RectTransform self, float rate ) {

            self.setFullSize();

            var height = self.rect.height;

            self.anchorMin  = new Vector2(0.0f,0.0f);
            self.anchorMax  = new Vector2(1.0f,0.0f);
            self.pivot      = new Vector2(0.0f,0.0f);
            self.sizeDelta  = new Vector2(0.0f,height*rate);

            return self;
        }

        public static void setOffset( this RectTransform self, float left, float top, float right, float bottom ) {

            self.offsetMin = new Vector2( left, top );
            self.offsetMax = new Vector2( right, bottom );
        }

        public static bool inScreenRect( this RectTransform self, Vector2 screenPos ) {

            var canvas = self.GetComponentInParent<Canvas>();
            switch( canvas.renderMode )
            {
            case RenderMode.ScreenSpaceCamera:
                {
                    var camera = canvas.worldCamera;
                    if( camera != null )
                    {
                        return RectTransformUtility.RectangleContainsScreenPoint( self, screenPos, camera );
                    }
                }
                break;
            case RenderMode.ScreenSpaceOverlay:
                return RectTransformUtility.RectangleContainsScreenPoint( self, screenPos );
            case RenderMode.WorldSpace:
                return RectTransformUtility.RectangleContainsScreenPoint( self, screenPos );
            }
            return false;
        }
        

        public static bool inScreenRect( this RectTransform self, RectTransform rectTransform ) {

            var rect1 = getScreenRect( self );
            var rect2 = getScreenRect( rectTransform );
            return rect1.Overlaps( rect2 );
        }
        
     
        public static Rect getScreenRect( this RectTransform self ) {

            var rect = new Rect();
            var canvas = self.GetComponentInParent<Canvas>();
            var camera = canvas.worldCamera;
            if( camera != null )
            {
                var corners = new Vector3[4];
                self.GetWorldCorners( corners );
                rect.min = camera.WorldToScreenPoint( corners[0] );
                rect.max = camera.WorldToScreenPoint( corners[2] );
            }
            return rect;
        }
    }
}