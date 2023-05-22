using UnityEngine;

namespace AudioSource
{
	public class AudioSourceSingleton : MonoBehaviour
	{

		private void Awake() => DontDestroyOnLoad(gameObject);
	}
}
