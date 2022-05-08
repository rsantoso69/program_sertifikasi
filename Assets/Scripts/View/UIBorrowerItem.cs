using TMPro;
using UnityEngine;

public class UIBorrowerItem : MonoBehaviour
{
	public TextMeshProUGUI textBorrowerName;
	public TextMeshProUGUI textBorrowedBooks;
	public TextMeshProUGUI textBorrowDate;
	public TextMeshProUGUI textReturnDate;

	public void Initialize(BorrowedData borrowedData)
	{
		textBorrowerName.text = borrowedData.member.name;
		string borrowedBookText = "";

		foreach (var book in borrowedData.books)
		{
			borrowedBookText += book.name + ", ";
		}

		textBorrowedBooks.text = borrowedBookText;

		textBorrowDate.text = borrowedData.borrowDate;
		textReturnDate.text = borrowedData.returnDate;
	}
}