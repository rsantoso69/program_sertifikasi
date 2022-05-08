using System.Collections.Generic;
using UnityEngine;

public class UIBorrowerList : UIPanel
{
	private List<UIBorrowerItem> listItemBorrowed = new List<UIBorrowerItem>();

	public UIBorrowerItem borrowerItemPrefab;
	public Transform content;

	public override void Show()
	{
		foreach (var sortResult in LibraryManager.Instance.GetBorrowedDatas())
		{
			UIBorrowerItem item = Instantiate(borrowerItemPrefab);
			item.transform.SetParent(content);
			item.transform.localScale = Vector3.one;
			item.Initialize(sortResult);

			//Save the reference to every created Save Data UI
			listItemBorrowed.Add(item);
		}
	}

	public override void Hide()
	{
		Destroy(gameObject);
	}
}
