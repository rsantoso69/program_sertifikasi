using Kaizen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonComponent<UIManager>
{
	private UIPanel currentPanel;

	public List<UIPanel> uIPanels;

	public void ShowPanel<T>()
	{
		foreach (var panel in uIPanels)
		{
			if (panel is T)
			{
				if (currentPanel != null)
					currentPanel.Hide();

				var newPanel = Instantiate(panel);
				//Wait a frame in order for UI to be fully instantiated
				StartCoroutine(WaitForUIRefresh(newPanel));
				currentPanel = newPanel;
			}
		}
	}

	private IEnumerator WaitForUIRefresh(UIPanel currentPanel)
	{
		yield return new WaitForEndOfFrame();
		currentPanel.Show();
	}
}
