using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation
{
    internal class User : IUser
    {
        public User(string name): this()
        {
            Name = name;
        }
        
        public User()
        {
            Name = "Unknown";
            Skills = new List<ISkillType>();
            Groups = new List<ISkillGroup>();
            Friends = new List<IUser>();
        }
        
        public User(string name, IEnumerable<ISkillType> skills, IEnumerable<ISkillGroup> groups, IEnumerable<IUser> friends)
        {
            Name = name;
            Skills = skills;
            Groups = groups;
            Friends = friends;
        }

        public string Name { get; }
        public IEnumerable<ISkillType> Skills { get; }
        public IEnumerable<ISkillGroup> Groups { get; }
        public IEnumerable<IUser> Friends { get; }
        
        
        public void LearnSkill(ISkillType skillType)
        {
            
        }
    }
}