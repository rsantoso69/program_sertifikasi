using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book", order = 1)]
public class BookScriptableObject : ScriptableObject
{
	public string title;
	public Genre genre;
}

public enum Genre
{
	SciFi,
	Romance,
	Horror,
}