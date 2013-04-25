using RentalHouseFinding.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using RentalHouseFinding.Models;
using System.Web.Mvc;
using Moq;
using System.Linq;
using System.Transactions;
namespace RentalHouseFinding.Tests
{

    /// <summary>
    ///This is a test class for UserControllerTest and is intended
    ///to contain all UserControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserControllerTest : UnitTestBase
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void EditPostTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //Arrange
                var mockContext = CreateMockContext("admin");
                RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
                UserController target = new UserController();
                target.ControllerContext = new ControllerContext();
                target.ControllerContext.HttpContext = mockContext.Http.Object;
                UserViewModel userViewModel = new UserViewModel
                {
                    UserId = 1,
                    PhoneNumber = "0987654321"
                };
                ActionResult actual;
                
                //Act
                actual = target.Edit(userViewModel);

                //Assert
                Assert.IsNotNull(actual);
                var temp = _db.Users.Where(u => u.Id == 1).FirstOrDefault();
                Assert.AreEqual(userViewModel.PhoneNumber, temp.PhoneNumber);
            }
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void EditGetTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            UserController target = new UserController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            ActionResult actual;

            //Act
            actual = target.Edit();

            //Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Favorites
        ///</summary>
        [TestMethod]
        public void FavoritesTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            UserController target = new UserController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            Nullable<int> page = new Nullable<int>();
            string sort = string.Empty;
            string sortdir = string.Empty;
            ActionResult actual;

            //Act
            actual = target.Favorites(page, sort, sortdir);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ActionResult));
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod]
        public void IndexTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            UserController target = new UserController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            ActionResult actual;

            //Act
            actual = target.Index();

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ActionResult));
        }

        /// <summary>
        ///A test for Info
        ///</summary>
        [TestMethod]
        public void InfoTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            UserController target = new UserController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            ActionResult actual;

            //Act
            actual = target.Info();

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ActionResult));
        }

        /// <summary>
        ///A test for Payments
        ///</summary>
        [TestMethod]
        public void PaymentsTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            UserController target = new UserController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            Nullable<int> page = new Nullable<int>();
            string sort = string.Empty;
            string sortdir = string.Empty;
            ActionResult actual;

            //Act
            actual = target.Payments(page, sort, sortdir);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ActionResult));
        }

        /// <summary>
        ///A test for Posts
        ///</summary>
        [TestMethod]
        public void PostsTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            UserController target = new UserController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            Nullable<int> page = new Nullable<int>();
            string sort = string.Empty;
            string sortdir = string.Empty;
            ActionResult actual;

            //Act
            actual = target.Posts(page, sort, sortdir);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ActionResult));
        }
    }
}
