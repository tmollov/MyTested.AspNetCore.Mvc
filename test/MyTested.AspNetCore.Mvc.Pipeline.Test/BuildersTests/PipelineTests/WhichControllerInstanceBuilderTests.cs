﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.PipelineTests
{
    using Setups.Routing;
    using Xunit;

    public class WhichControllerInstanceBuilderTests
    {
        [Fact]
        public void WhichShouldResolveCorrectSyncAction()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1))
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(1)));
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncAction()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals("Test")));
        }

        [Fact]
        public void WhichShouldResolveCorrectEmptyAction()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Empty")
                .To<HomeController>(c => c.Empty())
                .Which()
                .ShouldReturnEmpty();
        }

        [Fact]
        public void WhichShouldResolveCorrectEmptyAsyncAction()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/EmptyTask")
                .To<HomeController>(c => c.EmptyTask())
                .Which()
                .ShouldReturnEmpty();
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncActionWithSetup()
        {
            const string testData = "TestData";

            MyRouting
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which()
                .WithSetup(c =>
                {
                    c.Data = testData;
                })
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(testData)));
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncActionWithInnerSetup()
        {
            const string testData = "TestData";

            MyRouting
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which(controller => controller
                    .WithSetup(c => c.Data = testData))
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(testData)));
        }
    }
}
