using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBookListController : UIPanel
{
	private List<UIBookItem> listBookItems = new List<UIBookItem>();
	private List<Book> listToBeBorrowed = new List<Book>();

	public UIBookItem bookItemPrefab;
	public Transform content;

	public UIBookGenre[] bookGenres;

	public Action<Book> onGetSaveData;

	public override void Hide()
	{
		Destroy(gameObject);
	}

	public override void Show()
	{
		foreach (var bookGenre in bookGenres)
		{
			bookGenre.onInteracted = ButtonGenre_onClicked;
		}

		ShowAllBooks();
	}

	//Called when the user clicked a save item
	private void ItemBook_onClicked(UIBookItem bookItem)
	{
		if (listToBeBorrowed.Exists(book => book == bookItem.book))
		{
			listToBeBorrowed.Remove(bookItem.book);
			bookItem.SetSelected(false);
		}
		else
		{
			listToBeBorrowed.Add(bookItem.book);
			bookItem.SetSelected(true);
		}
	}

	public void ButtonBorrow_onClicked()
	{
		LibraryManager.Instance.AddBorrower(listToBeBorrowed.ToArray());
	}

	public void ButtonGenre_onClicked(Genre genre)
	{
		foreach (var bookGenre in bookGenres)
		{
			if (bookGenre.genre == genre)
			{
				bookGenre.image.color = Color.white;
				GetBooksByGenre(genre);
			}
			else
			{
				bookGenre.image.color = Color.cyan;
			}
		}
	}

	public void ButtonAll_onClicked()
	{
		ShowAllBooks();
	}

	private void GetBooksByGenre(Genre genre)
	{
		ClearAllBooks();

		Book[] books = LibraryManager.Instance.GetBooksByGenre(genre);
		foreach (var book in books)
		{
			UIBookItem bookItem = Instantiate(bookItemPrefab);
			bookItem.transform.SetParent(content);
			bookItem.Initialize(book);
			bookItem.onClicked = ItemBook_onClicked;
			bookItem.transform.localScale = Vector3.one;

			if (listToBeBorrowed.Exists(currentBook => currentBook == book))
			{
				bookItem.SetSelected(true);
			}

			//Save the reference to every created Save Data UI
			listBookItems.Add(bookItem);
		}
	}

	private void ShowAllBooks()
	{
		ClearAllBooks();

		foreach (var bookGenre in bookGenres)
		{
			bookGenre.image.color = Color.cyan;
		}

		Book[] books = LibraryManager.Instance.GetAllBooks();
		foreach (var book in books)
		{
			UIBookItem bookItem = Instantiate(bookItemPrefab);
			bookItem.transform.SetParent(content);
			bookItem.Initialize(book);
			bookItem.onClicked = ItemBook_onClicked;
			bookItem.transform.localScale = Vector3.one;
			if (listToBeBorrowed.Exists(currentBook => currentBook == book))
			{
				bookItem.SetSelected(true);
			}
			else
			{
				bookItem.SetSelected(false);
			}
			//Save the reference to every created Save Data UI
			listBookItems.Add(bookItem);
		}
	}

	private void ClearAllBooks()
	{
		foreach (var item in listBookItems)
		{
			Destroy(item.gameObject);
		}

		listBookItems.Clear();
	}
}
