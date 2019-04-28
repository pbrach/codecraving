namespace Components.Interface
{
    public interface INewSkillLearned
    {
        string LearnerName { get; }
        ISkillType NewSkillType { get; }
        string Message { get; }
    }
}