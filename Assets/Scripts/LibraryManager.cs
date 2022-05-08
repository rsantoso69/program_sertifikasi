using Kaizen;
using System;
using System.Collections.Generic;

public class LibraryManager : SingletonComponent<LibraryManager>
{
	private const string AdministratorUserName = "admin";
	private const string AdministratorPassword = "admin";

	private const string UserDatabase = "users";
	private const string BorrowDatabase = "borrows";
	private const string Extension = ".dat";

	private string currentUser;

	private List<Member> members = new List<Member>();
	private List<Book> books = new List<Book>();
	private List<BorrowedData> borrowedDatas = new List<BorrowedData>();

	public BookScriptableObject[] bookScriptableObjects;

	public bool IsAdmin { get => currentUser == AdministratorUserName; }

	protected override void Awake()
	{
		base.Awake();

		foreach (var bookScriptableObject in bookScriptableObjects)
		{
			Book book = new Book()
			{
				name = bookScriptableObject.title,
				genre = bookScriptableObject.genre,
			};
			books.Add(book);
		}

		if (FileManager.FileExists(UserDatabase, Extension))
		{
			members = FileManager.ReadJson<List<Member>>(UserDatabase, Extension);
		}
		else
		{
			members = new List<Member>();
			FileManager.WriteJson(UserDatabase, Extension, members);
		}

		if (FileManager.FileExists(BorrowDatabase, Extension))
		{
			borrowedDatas = FileManager.ReadJson<List<BorrowedData>>(BorrowDatabase, Extension);
		}
		else
		{
			borrowedDatas = new List<BorrowedData>();
			FileManager.WriteJson(BorrowDatabase, Extension, borrowedDatas);
		}
	}

	private void Start()
	{
		UIManager.Instance.ShowPanel<UIWelcomeController>();
	}

	public void AddBorrower(Book[] books)
	{
		DateTime today = DateTime.Now;
		DateTime duedate = today.AddDays(7);

		BorrowedData borrowedData = new BorrowedData()
		{
			books = books,
			member = GetCurrentMember(),
			borrowDate = today.ToString("MM/dd/yyyy"),
			returnDate = duedate.ToString("MM/dd/yyyy"),
		};

		borrowedDatas.Add(borrowedData);
		FileManager.WriteJson(BorrowDatabase, Extension, borrowedDatas);
	}

	public BorrowedData[] GetBorrowedDatas()
	{
		return borrowedDatas.ToArray();
	}

	public bool Login(string username, string password)
	{
		if (username == AdministratorUserName && password == AdministratorPassword)
		{
			SetCurrentUser(username);
			return true;
		}

		string savedPassword = members.Find(member => member.username == username).password;
		bool successful = password == savedPassword;

		if (successful)
		{
			SetCurrentUser(username);
		}

		return password == savedPassword;
	}

	private void SetCurrentUser(string username)
	{
		currentUser = username;
	}

	public bool CheckUserExist(string username)
	{
		if (username == AdministratorUserName)
			return true;

		return members.Exists(member => member.username == username);
	}

	public void AddUser(Member member)
	{
		members.Add(member);
		FileManager.WriteJson(UserDatabase, Extension, members);
	}

	public Member GetCurrentMember()
	{
		return members.Find(member => member.username == currentUser);
	}

	public Book[] GetAllBooks()
	{
		return books.ToArray();
	}

	public Book[] GetBooksByGenre(Genre genre)
	{
		return books.FindAll(book => book.genre == genre).ToArray();
	}
}

