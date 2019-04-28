using Components.Interface;

namespace Components.Implementation.SkillTypes
{
    internal class ForwardStrideSkill : ISkillType
    {
        public string Name => "Forward Stride";
        public string Description => "Simple forward stride. At higher levels" +
                                     "more stable, faster, more effective and" +
                                     "longer endurance.";
    }
}