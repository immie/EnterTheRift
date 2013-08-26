using UnityEngine;
using System.Collections;
using System.Reflection;

public class ManagerFactory : MonoBehaviour
{
	[SerializeField] private InputManager inputManagerPrefab = null;
	public static InputManager InputManager { get; private set; }
	
	[SerializeField] private SixenseInput sixenseInputPrefab = null;
	public static SixenseInput SixenseInput { get; private set; }
	
	void Awake ()
	{
		GameObject.DontDestroyOnLoad(this.gameObject);
		InitializeManagers();
	}
	
	private void InitializeManagers ()
	{
		ManagerFactory.SixenseInput = InitializeManager<SixenseInput>(this.sixenseInputPrefab);
		ManagerFactory.InputManager = InitializeManager<InputManager>(this.inputManagerPrefab);
		ManagerFactory.InputManager.sixenseInputScript = ManagerFactory.SixenseInput;
	}
	
	
	#region Helpers

	private TManager InitializeManager<TManager> (UnityEngine.Object prefab) where TManager : MonoBehaviour
	{
		GameObject instance = UnitySceneUtility.InstantiatePrefabAtTransform(prefab, this.transform);
		GameObject.DontDestroyOnLoad(instance);
		return instance.GetComponent<TManager>();
	}

	#endregion Helpers
}


