using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation
{
    public class User : IUser
    {
        public User(string name)
        {
            Name = "Unknown";
            Skills = new List<IUserSkill>();
            Friends = new List<IUser>();
            Name = name;
        }

        public User(string name, IEnumerable<IUserSkill> skills, IEnumerable<IUser> friends)
        {
            Name = name;
            Skills = skills;
            Friends = friends;
        }

        public string Name { get; }
        public IEnumerable<IUserSkill> Skills { get; }
        public IEnumerable<IUser> Friends { get; }
        
        
        public void LearnSkill(ISkillType skillType)
        {
            
        }
    }
}