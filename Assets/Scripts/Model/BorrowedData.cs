using System;

[Serializable]
public class BorrowedData
{
	public Book[] books;
	public Member member;
	public string borrowDate;
	public string returnDate;
}
