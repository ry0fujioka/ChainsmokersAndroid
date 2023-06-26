using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisible : MonoBehaviour
{
	[SerializeField]
	Camera mainCamera;

	Rect cameraRect = new Rect(0, 0, 1, 1);

	[SerializeField]
	float outCameraTime;

	float outCameraTimeCount;
	bool onDeathInvoked = false;

	void Start()
	{

	}
	private void OnEnable()
	{
		LevelManager.Instance.onRespawn += OnRespawn;
	}

	private void OnDisable()
	{
		//if (gameObject.scene.isLoaded)
		//{
		//	return;
		//}
		//if (LevelManager.Instance != null)
		//	LevelManager.Instance.onRespawn -= OnRespawn;
	}

	void Update()
	{
		var viewportPos = mainCamera.WorldToViewportPoint(this.transform.position);

		if (cameraRect.Contains(viewportPos))
		{
			outCameraTimeCount = 0;
		}
		else
		{
			if (outCameraTime >= outCameraTimeCount)
				outCameraTimeCount += Time.deltaTime;
			else if (!onDeathInvoked)
			{
				PlayerHealth.Instance.onDeath?.Invoke();
				onDeathInvoked = true;
			}
		}
	}

	void OnRespawn()
    {
		onDeathInvoked = false;
    }
}
