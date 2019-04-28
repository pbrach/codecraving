using Components.Interface;

namespace Components.Implementation
{
    internal class NewSkillLearned : INewSkillLearned
    {
        public NewSkillLearned(string learnerName, ISkillType newSkillType, string message)
        {
            LearnerName = learnerName;
            NewSkillType = newSkillType;
            Message = message;
        }

        public string LearnerName { get; }
        public ISkillType NewSkillType { get; }
        public string Message { get; }
    }
}