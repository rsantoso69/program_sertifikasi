using System;
using TMPro;
using UnityEngine;

public class UIBookItem : MonoBehaviour
{
	public Book book;

	public TextMeshProUGUI textTitle;
	public TextMeshProUGUI textGenre;
	public GameObject imageSelection;

	public Action<UIBookItem> onClicked;

	public void Initialize(Book book)
	{
		this.book = book;
		textTitle.text = book.name;
		textGenre.text = book.genre.ToString();
		SetSelected(false);
	}

	public void OnClicked()
	{
		onClicked.Invoke(this);
	}

	public void SetSelected(bool state)
	{
		imageSelection.SetActive(state);
	}
}
