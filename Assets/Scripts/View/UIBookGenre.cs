using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBookGenre : MonoBehaviour
{
	public TextMeshProUGUI textGenre;
	public Image image;

	public Genre genre;

	public Action<Genre> onInteracted;

	protected virtual void Awake()
	{
		textGenre.text = genre.ToString();
	}

	public virtual void ButtonInteract_onClicked()
	{
		onInteracted.Invoke(genre);
	}
}
