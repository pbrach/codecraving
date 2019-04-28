namespace Components.Interface
{
    public interface IUserSkill
    {
        ISkillType Skill { get; }
        Level Level { get; }
    }
}