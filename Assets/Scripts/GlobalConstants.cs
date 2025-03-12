
    using UnityEngine;

    public static class GlobalConstants
    {
        public static class AnimatorParameters
        {
            public static readonly int runX = Animator.StringToHash(nameof(runX));
            public static readonly int runY = Animator.StringToHash(nameof(runY));
            public static readonly int runXY = Animator.StringToHash(nameof(runXY));
            public static readonly int follow = Animator.StringToHash(nameof(follow));
            public static readonly int walkX = Animator.StringToHash(nameof(walkX));
            public static readonly int walkY = Animator.StringToHash(nameof(walkY));
            public static readonly int isDirX = Animator.StringToHash(nameof(isDirX));
        }

        public static class Tags
        {
            
        }

        public static class InputData
        {
            
        }
    }
