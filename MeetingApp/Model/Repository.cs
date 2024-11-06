namespace MeetingApp.Model
{
    public static class Repository
    {   
        public static List<UserInfo> _users = new();

        static Repository()
        {
      
                _users.Add(new UserInfo {Id=1, Name = "Ali", Email = "a" , Phone = "123-456-7890", WillAttend = true});
                _users.Add(new UserInfo { Id=2,Name = "Veli", Email = "a" , Phone = "123-456-7890", WillAttend = false});
                _users.Add(new UserInfo { Id=3,Name = "Ahmet", Email = "a" , Phone = "123-456-7890", WillAttend = true});

            
        }

        public static List<UserInfo> Users 
        {
            get { return _users; }

        }

        public static void AddUser(UserInfo user)
        {   user.Id = Users.Count + 1;
            _users.Add(user);
        }

        public static UserInfo GeyById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}