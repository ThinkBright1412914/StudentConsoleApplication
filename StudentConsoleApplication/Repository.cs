namespace StudentConsoleApplication
{
	public interface Repository
	{
		IEnumerable<Student> GetAll();
		Student GetById(int id);
		Student DeleteById(int id);
		Student Add(Student model);
		Student Update(Student model);
	}

	public class Services : Repository
	{
		List<Student> _store = new List<Student>();

		public Services()
		{
			_store = new List<Student>();
		}

		public Student Add(Student model)
		{
			Student response = new Student();
			_store.Add(model);
			response.Message = "Added SuccessFully";
			return response;
		}

		public Student DeleteById(int id)
		{
			Student response = new Student();
			var result = _store.Find(x => x.Id == id);
			if (result != null)
			{
				_store.Remove(result);
				response.Message = "Deleted SuccessFully";
			}
			else
			{
				response.Message = "Not Found";
			}
			return response;
		}

		public IEnumerable<Student> GetAll()
		{
			return _store.ToList();
		}

		public Student GetById(int id)
		{
			var result = _store.Find(x => x.Id == id);
			Student response = new Student();
			if (result == null)
			{
				response.Message = "Students Not Found.";
			}
			else
			{
				response.Id = result.Id;
				response.Name = result.Name;
				response.Message = result.Message;
				response.Class = result.Class;
				response.Address = result.Address;
			}
			return response;
		}

		public Student Update(Student model)
		{
			Student response = new Student();
			var result = _store.Find(x => x.Id == model.Id);
			if (result != null)
			{
				result.Class = model.Class;
				result.Address = model.Address;
				result.Name = model.Name;
				response.Message = "Updated Successfully.";
			}
			else
			{
				response.Message = "Not Found.";
			}
			return response;
		}
	}
}
