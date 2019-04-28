using Components.Interface;

namespace Components.Implementation.SkillTypes
{
    internal class BackwardStrideSkill : ISkillType
    {
        public string Name => "Backward stride";
        public string Description => "Simple backward stride. At higher levels" +
                                     "more stable, faster, more effective and" +
                                     "longer endurance.";
    }
}