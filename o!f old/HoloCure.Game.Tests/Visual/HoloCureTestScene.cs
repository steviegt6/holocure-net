using osu.Framework.Testing;

namespace HoloCure.Game.Tests.Visual
{
    public class HoloCureTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HoloCureTestSceneTestRunner();

        private class HoloCureTestSceneTestRunner : HoloCureGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
