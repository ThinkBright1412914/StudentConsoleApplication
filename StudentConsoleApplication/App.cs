namespace StudentConsoleApplication
{
	public class App
	{
		private readonly Repository _repo;

		public App(Repository repo)
		{
			_repo = repo;
		}

		private static void DisplayStudentList(List<Student> students)
		{
			int idWidth = 5;
			int nameWidth = 20;
			int classWidth = 25;
			int cityWidth = 15;

			Console.WriteLine($"{"Id".PadRight(idWidth)}{"Name".PadRight(nameWidth)}{"Class".PadRight(classWidth)}{"Address".PadRight(cityWidth)}");
			Console.WriteLine(new string('-', idWidth + nameWidth + classWidth + cityWidth));

			foreach (var student in students)
			{
				Console.WriteLine($"{student.Id.ToString().PadRight(idWidth)}" +
					$"{student.Name.PadRight(nameWidth)}" +
					$"{student.Class.ToString().PadRight(classWidth)}" +
					$"{student.Address.PadRight(cityWidth)}");
			}
		}

		private static void DisplayStudentInfo(Student student)
		{
			Console.WriteLine($"Student Info" +
							$" Id : {student.Id}\n" +
							$"Name : {student.Name}\n" +
							$"Class : {student.Class}\n" +
							$"Address : {student.Address}");
		}

		public void Main()
		{
			bool isRunning = true;

			while (isRunning)
			{
				Console.Clear();
				Console.WriteLine("Choose option to perform operation");
				Console.WriteLine(" 1 : Add Student ");
				Console.WriteLine(" 2 : Get All Student");
				Console.WriteLine(" 3 : Get Student By Id ");
				Console.WriteLine(" 4 : Update Student ");
				Console.WriteLine(" 5 : Delete Student ");
				Console.WriteLine(" 6 : Exit ");
				Console.WriteLine("_________________");
				Console.WriteLine("Enter your choice here");

				if (int.TryParse(Console.ReadLine(), out int option))
				{
					switch (option)
					{
						case 1:
							Console.WriteLine("Enter Student Id, Class, Name, Address");
							int id = int.Parse(Console.ReadLine());
							int studentClass = int.Parse(Console.ReadLine());
							string name = Console.ReadLine();
							string address = Console.ReadLine();

							Student model = new Student()
							{
								Id = id,
								Name = name,
								Address = address,
								Class = studentClass
							};

							var response = _repo.Add(model);
							Console.WriteLine(response.Message);
							Console.ReadLine();
							break;

						case 2:
							var students = _repo.GetAll();
							if (students.Any())
							{
								DisplayStudentList(students.ToList());
							}
							else
							{
								Console.WriteLine("There are no students.");
							}

							Console.ReadLine();
							break;

						case 3:
							Console.WriteLine("Enter Student Id");
							int studentId = int.Parse(Console.ReadLine());
							var studentById = _repo.GetById(studentId);
							if (studentById != null)
							{
								Console.WriteLine($"Id: {studentById.Id}, Name: {studentById.Name}, Class: {studentById.Class}, Address: {studentById.Address}");
							}
							else
							{
								Console.WriteLine("Student not found.");
							}
							Console.ReadLine();
							break;

						case 4:
							Console.WriteLine("Enter Student Id");
							int updateStudentId = int.Parse(Console.ReadLine());
							var updateStudentObj = _repo.GetById(updateStudentId);

							if (updateStudentObj.Name != null)
							{
								DisplayStudentInfo(updateStudentObj);

								Console.WriteLine("\n\n\nEnter Student Class, Name, Address");
								string upClass = Console.ReadLine();
								string upName = Console.ReadLine();
								string upAddress = Console.ReadLine();

								if (!string.IsNullOrEmpty(upName))
								{
									updateStudentObj.Name = upName;
								}

								if (!string.IsNullOrEmpty(upAddress))
								{
									updateStudentObj.Address = upAddress;
								}

								if (!string.IsNullOrEmpty(upClass))
								{
									updateStudentObj.Class = int.Parse(upClass);
								}

								var udpateResponse = _repo.Update(updateStudentObj);
								Console.WriteLine(udpateResponse.Message);
							}
							else
							{
								Console.WriteLine(updateStudentObj.Message);
							}

							Console.ReadLine();
							break;

						case 5:
							Console.WriteLine("Enter Student Id");
							int delId = int.Parse(Console.ReadLine());
							var deleteResponse = _repo.DeleteById(delId);
							Console.WriteLine(deleteResponse.Message);
							Console.ReadLine();
							break;

						case 6:
							isRunning = false;
							break;

						default:
							Console.WriteLine("Invalid option, please try again.");
							break;
					}
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a number.");
				}
			}

			Console.WriteLine("Exiting application. Press Enter to close...");
			Console.ReadLine();
		}
	}
}
