using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnTexture : MonoBehaviour {

	[SerializeField] private Camera cam;
	[SerializeField] private Renderer destinationRenderer;
	[SerializeField] private int TextureSize;
	[SerializeField] private float Radius;

	private Texture2D _texture;

	void Start ()
	{
		_texture = new Texture2D(TextureSize, TextureSize, TextureFormat.RFloat, false, true); 
		
		_texture.Apply();
		destinationRenderer.material.SetTexture("_MouseMap", _texture);
		//destinationRenderer.material.SetFloat("_MaxAge",0);
	}

	

	private void OnMouseDrag ()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
			// younger = redder (higher r)
			// older = blacker
			//Debug.Log("Time: " + Time.timeSinceLevelLoad + "; r: " + r);
			Color color = new Color(Time.timeSinceLevelLoad, 0, 0, 1);
			//Debug.Log("r: " + color.r);
			//Color color = new Color(1, 0, 0, 1);

			int x = (int)(hit.textureCoord.x*_texture.width);
			int y = (int)(hit.textureCoord.y*_texture.height);

			_texture.SetPixel(x, y, color);

			for (int i = 0; i < _texture.height; i++)
			{
				for (int j = 0; j < _texture.width; j++)
				{
					float dist = Vector2.Distance(new Vector2(i,j), new Vector2(x,y));
					if(dist <= Radius)
						_texture.SetPixel(i, j, color);
				}
			}

			_texture.Apply();
			destinationRenderer.material.SetTexture("_MouseMap", _texture);
        }
	}

	public void SetClearAge(float time) {
		destinationRenderer.material.SetFloat("_MaxAge", time);

	}
}