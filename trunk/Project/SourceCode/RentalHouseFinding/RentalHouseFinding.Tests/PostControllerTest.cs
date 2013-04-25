using RentalHouseFinding.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using RentalHouseFinding.Caching;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Web;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using Moq;
using RentalHouseFinding.Common;
using System.Collections.Specialized;
namespace RentalHouseFinding.Tests
{
    
    
    /// <summary>
    ///This is a test class for PostControllerTest and is intended
    ///to contain all PostControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PostControllerTest : UnitTestBase
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
        ///A test for AddFavorite
        ///</summary>
        [TestMethod()]
        public void AddFavoriteTest()
        {
            //Arrange
            var mockContext = CreateMockContext("chungnt01726@fpt.edu.vn");
            PostController target = new PostController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            int id = 1;
            JsonResult actual;

            //Act
            actual = target.AddFavorite(id);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Data, true);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void CreateGetTest()
        {
            //Arrange
            var mockContext = CreateMockContext("admin");
            PostController target = new PostController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            ActionResult actual;

            //Act
            actual = target.Create();

            //Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod]
        public void CreatePostTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //Arrange
                var mockContext = CreateMockContext("chungnt01726@fpt.edu.vn");
                PostController target = new PostController();
                target.ControllerContext = new ControllerContext();
                var form = new NameValueCollection();
                form.Add("text", "text");
                mockContext.Http.SetupGet(c => c.Request.Form)
                    .Returns(form);
                target.ControllerContext.HttpContext = mockContext.Http.Object;
                PostViewModel viewModel = new PostViewModel
                {
                    CategoryId = CategoryConstant.PHONG_TRO,
                    RestrictHours = "10",
                    Title = "Test",
                    UserId = 2,
                    Street = "abc",
                    PhoneContact = "01234567890",
                    NumberHouse = "123",
                    ProvinceId = 2,
                    DistrictId = 20,
                    Price = 15,
                    Area = 20,
                    PhoneActive = "0123456789",
                    Lat = 21.0423691,
                    Lon = 105.7879371,
                    IsStayWithOwner = false,
                    IsAllowCooking = false,
                    HasAirConditioner = false,
                    HasBed = true,
                    HasGarage = true,
                    HasInternet = false,
                    HasMotorParking = false,
                    HasSecurity = false,
                    HasToilet = true,
                    HasTVCable = false,
                    HasWaterHeater = true,
                    ElectricityFee = 5000,
                    WaterFee = 7000,
                    Views = 0
                };
                IEnumerable<HttpPostedFileBase> images = new List<HttpPostedFileBase>();
                ActionResult actual;

                //Act
                actual = target.Create(viewModel, images);

                //Assert
                Assert.IsNotNull(actual);
            }
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod]
        public void DeleteTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
                var mockContext = CreateMockContext("chungnt01726@fpt.edu.vn");
                PostController target = new PostController(SetupCacheRepos());
                target.ControllerContext = new ControllerContext();
                target.ControllerContext.HttpContext = mockContext.Http.Object;
                int id = 1;
                ActionResult actual;
                actual = target.Delete(id);
                Assert.IsNotNull(actual);

                var deletedPost = (from p in _db.Posts where p.Id == id select p).FirstOrDefault();
                Assert.IsNotNull(deletedPost);
                Assert.AreEqual(true, deletedPost.IsDeleted);
            }
        }

        /// <summary>
        ///A test for DeleteImage
        ///</summary>
        [TestMethod]
        public void DeleteImageTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
                var mockContext = CreateMockContext("admin");
                PostController target = new PostController(SetupCacheRepos());
                target.ControllerContext = new ControllerContext();
                target.ControllerContext.HttpContext = mockContext.Http.Object;
                int id = 1;
                int postId = 67;
                bool expected = true;
                bool actual;
                actual = target.DeleteImage(id, postId);
                Assert.AreEqual(expected, actual);
                var postImage = (from i in _db.PostImages where i.Id == id select i).FirstOrDefault();
                Assert.AreEqual(true, postImage.IsDeleted);
            }
        }

        /// <summary>
        ///A test for Details
        ///</summary>
        [TestMethod]
        public void DetailsTest()
        {
            PostController target = new PostController();
            int id = 1;
            string name = string.Empty;
            ActionResult actual;
            actual = target.Details(id, name);
            Assert.IsNotNull(actual);
            
        }

        /// <summary>
        ///A test for DetailsBox
        ///</summary>
        [TestMethod]
        public void DetailsBoxTest()
        {
            PostController target = new PostController();
            int id = 1;
            string name = string.Empty;
            ActionResult actual;
            actual = target.DetailsBox(id, name);
            Assert.IsNotNull(actual);
            
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void EditTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                
                RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
                
                var mockContext = CreateMockContext("chungnt01726@fpt.edu.vn");
                PostController target = new PostController(SetupCacheRepos());
                target.ControllerContext = new ControllerContext();
                var form = new NameValueCollection();
                form.Add("text", "text");
                mockContext.Http.SetupGet(c => c.Request.Form)
                    .Returns(form);
                target.ControllerContext.HttpContext = mockContext.Http.Object;
                PostViewModel postViewModel = new PostViewModel
                {
                    Id = 2,
                    CategoryId = CategoryConstant.PHONG_TRO,
                    RestrictHours = "10",
                    Title = "Test",
                    UserId = 2,
                    Street = "abc",
                    PhoneContact = "01234567890",
                    NumberHouse = "123",
                    ProvinceId = 2,
                    DistrictId = 20,
                    Price = 15,
                    Area = 20,
                    PhoneActive = "0123456789",
                    Lat = 21.0423691,
                    Lon = 105.7879371,
                    IsStayWithOwner = false,
                    IsAllowCooking = false,
                    HasAirConditioner = false,
                    HasBed = true,
                    HasGarage = true,
                    HasInternet = false,
                    HasMotorParking = false,
                    HasSecurity = false,
                    HasToilet = true,
                    HasTVCable = false,
                    HasWaterHeater = true,
                    ElectricityFee = 5000,
                    WaterFee = 7000,
                    Views = 0
                };
                IEnumerable<HttpPostedFileBase> images = null;
                ActionResult actual;

                actual = target.Edit(postViewModel, images);
                var temp = _db.Posts.Where(p => p.Id == 2).FirstOrDefault();
                Assert.AreEqual(postViewModel.Title, temp.Title);
            }
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod]
        public void EditTest1()
        {
            var mockContext = CreateMockContext("chungnt01726@fpt.edu.vn");
            PostController target = new PostController();
            target.ControllerContext = new ControllerContext();
            target.ControllerContext.HttpContext = mockContext.Http.Object;
            int id = 2;

            var actual = target.Edit(id) as ViewResult;

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Model);
            var model = (PostViewModel)actual.Model;
            Assert.AreEqual("1 PHÒNG DUY NHẤT DÀNH CHO NỮ VIÊN CHỨC GẦN ĐƯỜNG BƯỞI", model.Title);
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod()]
        public void IndexTest()
        {
            PostController target = new PostController();
            ActionResult actual;
            actual = target.Index();
            Assert.IsNotNull(actual);
            
        }

        /// <summary>
        ///A test for RemoveFavorite
        ///</summary>
        [TestMethod]
        public void RemoveFavoriteTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //Arrange
                var mockContext = CreateMockContext("chungnt01726@fpt.edu.vn");
                PostController target = new PostController();
                target.ControllerContext = new ControllerContext();
                target.ControllerContext.HttpContext = mockContext.Http.Object;
                int id = 2;
                JsonResult actual;

                //Act
                actual = target.RemoveFavorite(id);

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(actual.Data, true);
            }
            
        }

        /// <summary>
        ///A test for ViewImage
        ///</summary>
        [TestMethod]
        public void ViewImageTest()
        {
            PostController target = new PostController();
            int id = 67;
            ActionResult actual;
            actual = target.ViewImage(id);
            Assert.IsNotNull(actual);
            
        }
    }
}
