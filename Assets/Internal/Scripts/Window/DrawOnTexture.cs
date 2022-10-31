using Audio;
using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
	public class DrawOnTexture : MonoBehaviour
	{
		///////////////////////////////
		//  INSPECTOR VARIABLES      //
		///////////////////////////////
		[SerializeField] private Renderer _destinationRenderer;
		[SerializeField] private int _textureSize;
		[SerializeField] private float _radius;

		///////////////////////////////
		//  PRIVATE VARIABLES         //
		///////////////////////////////
		private Texture2D _texture;
		private Camera cam;
		private AudioManager _audioManager { get { return AudioManager.Instance; } }
		private GameState _currentState;

		///////////////////////////////
		//  PRIVATE METHODS           //
		///////////////////////////////
		private void Awake()
		{
			_currentState = Resources.Load<GameState>("CurrentState");
			_currentState.Window = this;
			_texture = new Texture2D(_textureSize, _textureSize, TextureFormat.RFloat, false, true);
			cam = Camera.main;
			_texture.Apply();
			_destinationRenderer.material.SetTexture("_MouseMap", _texture);
			_destinationRenderer.material.SetFloat("_MaxAge", 0);

		}
		


		private void OnMouseUp()
		{
			if (_currentState.State == GameStateType.Playing)
			{
				_audioManager.PlayWipeAudio();
			}
		}

		private void OnMouseDrag()
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				
				Color color = new Color(Time.timeSinceLevelLoad, 0, 0, 1);
			

				int x = (int)(hit.textureCoord.x * _texture.width);
				int y = (int)(hit.textureCoord.y * _texture.height);

				_texture.SetPixel(x, y, color);

				for (int i = 0; i < _texture.height; i++)
				{
					for (int j = 0; j < _texture.width; j++)
					{
						float dist = Vector2.Distance(new Vector2(i, j), new Vector2(x, y));
						if (dist <= _radius)
							_texture.SetPixel(i, j, color);
					}
				}

				_texture.Apply();
				_destinationRenderer.material.SetTexture("_MouseMap", _texture);
			}
		}

		///////////////////////////////
		//  PUBLIC API               //
		///////////////////////////////

		public void SetClearAge(float time)
		{
			_destinationRenderer.material.SetFloat("_MaxAge", time);

		}

		public void SetRadius(float radius)
		{
			_radius = radius;
		}

	}
}