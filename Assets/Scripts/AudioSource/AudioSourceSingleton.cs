using UnityEngine;

public class AudioSourceSingleton : MonoBehaviour
{

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
